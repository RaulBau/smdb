using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace SMBD.Sentencias
{
    class Sentencias
    {
        public Regex selectAllRegEx;
        public Regex selectAtributosRegEx;
        public Regex selectWhereRegEx;
        public bool sentenciaCorrecta;
        public List<string> atributos;
        public string tabla1, tabla2;

        public string nomAtrib = "";
        public string condicion = "";
        public string valor = "";

        public Sentencias()
        {
            selectAllRegEx = new Regex(@"(SELECT|select)\s+\*\s+(FROM|from)\s+(\w+)\s*;?\s*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            selectAtributosRegEx = new Regex(@"(SELECT|select)(\s+\w+,?)+\s+(FROM|from)\s+(\w+)\s*;?\s*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            selectWhereRegEx = new Regex(@"(SELECT|select)((\s+\w+,?)+)\s+(FROM|from)\s+(\w+)\s*(WHERE|where)\s+(\w+)\s+(=|>|<|>=|<=|<>)\s+(\d+)\s*;?", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            sentenciaCorrecta = false;
            atributos = new List<string>();
        }

        public void inicializaDatos()
        {
            atributos.Clear();
            tabla1 = "";
            tabla2 = "";
            nomAtrib = "";
            condicion = "";
            valor = "";
        }

        public bool selectAll(string entrada)
        {
            MatchCollection matchCol = selectAllRegEx.Matches(entrada);
            GroupCollection groupCol = selectAllRegEx.Match(entrada).Groups;
            sentenciaCorrecta = false;

            if (matchCol.Count == 1 && matchCol[0].Length == entrada.Length)
            {
                inicializaDatos();
                tabla1 = limpia_cadena(groupCol[3].Value)[0];
                sentenciaCorrecta = true;
            }

            return sentenciaCorrecta;
        }

        public bool selectAtributos(string entrada)
        {
            MatchCollection matchCol = selectAtributosRegEx.Matches(entrada);
            GroupCollection groupCol = selectAtributosRegEx.Match(entrada).Groups;
            sentenciaCorrecta = false;

            if (matchCol.Count == 1 && matchCol[0].Length == entrada.Length)
            {
                inicializaDatos();
                tabla1 = groupCol[4].Value;
                atributos = limpia_cadena(groupCol[2].Value);
                sentenciaCorrecta = true;
            }

            return sentenciaCorrecta;
        }

        public bool selectWhere(string entrada)
        {
            MatchCollection matchCol = selectWhereRegEx.Matches(entrada);
            GroupCollection groupCol = selectWhereRegEx.Match(entrada).Groups;
            sentenciaCorrecta = false;

            if (matchCol.Count == 1 && matchCol[0].Length == entrada.Length)
            {
                inicializaDatos();
                tabla1 = groupCol[5].Value;
                atributos = limpia_cadena(groupCol[2].Value);
                nomAtrib = groupCol[7].Value;
                condicion = groupCol[8].Value;
                valor = groupCol[9].Value;
                sentenciaCorrecta = true;
            }

            return sentenciaCorrecta;
        }

        private List<string> limpia_cadena(string cadena)
        {
            List<string> lista_atrs = new List<string>();
            Regex r = new Regex(@"\s+|,");
            lista_atrs = r.Replace(cadena, " ").Split(' ').ToList();
            lista_atrs.RemoveAll(s => s == "");
            return lista_atrs;
        }

        public string obtenAtributos()
        {
            string s = "";
            foreach (string str in atributos)
                s += str + " ";
            return s;
        }
    }


    class Select : Sentencias
    {
        public string resultado;
        public string[] atributos_tablaA, atributos_tablaB;
        public List<List<string>> datos;
        public List<int> atr_no_selA, atr_no_selB, orden_atrA, orden_atrB;
        public bool resuelve_ambiguedad;
        public List<Tabla> tablas;
        public Select(List<Tabla> t)
        {
            resuelve_ambiguedad = false;
            resultado = "";
            tablas = t;
            datos = new List<List<string>>();
            atr_no_selA = new List<int>();
            atr_no_selB = new List<int>();
            atributos_tablaA = new string[1];
            atributos_tablaB = new string[1];
        }

        #region ejecuciones 
        public bool ejecutaSelectAll()
        {
            foreach (Tabla t in tablas)
            {
                if (t.nombre.Replace(".t", "") == tabla1)
                {
                    inicializaVariables(t, null);
                    foreach (Atributo a in t.atributos)
                    {
                        resultado += a.nombre + "\t";
                        this.atributos.Add(a.nombre);
                    }
                    datos = obtenRegistros(t, false);
                    resultado = "Ejecución terminada correctamente.";
                    return true;
                }
            }
            resultado = "La tabla no existe!.";
            return false;
        }

        public bool ejecutaSelectAtributos(bool where)
        {
            foreach (Tabla t in tablas)
            {
                if (t.nombre.Replace(".t", "") == tabla1)
                {
                    inicializaVariables(t, null);

                    if (!verifica_atributos(t, where))
                    {
                        resultado = "Algun atributo no existe!";
                        return false;
                    }

                    reorganiza_atributos(t, null);
                    atr_no_selA = separaAtributosMostrar(t);
                    datos = obtenRegistros(t, where);
                    resultado = "Ejecución terminada correctamente.";
                    return true;
                }
            }
            resultado = "La tabla no existe!.";
            return false;
        }
        #endregion

        #region verificaciones
        public bool cumpleCondicion(Tabla t, string[] tupla)
        {
            int indice_atributo_condicional = t.obtenNombreAtributos().IndexOf(nomAtrib);

            if (indice_atributo_condicional >= 0)
            {
                float valor_f = float.Parse(this.valor, System.Globalization.CultureInfo.InvariantCulture);
                float valor_tupla = float.Parse(tupla[indice_atributo_condicional], System.Globalization.CultureInfo.InvariantCulture);
                switch (condicion)
                {
                    case "=":
                        return valor_tupla == valor_f;
                    case ">":
                        return valor_tupla > valor_f;
                    case "<":
                        return valor_tupla < valor_f;
                    case ">=":
                        return valor_tupla >= valor_f;
                    case "<=":
                        return valor_tupla <= valor_f;
                    case "<>":
                        return valor_tupla != valor_f;
                    default:
                        return false;
                }
            }
            return false;
        }

        public bool hay_tablas()
        {
            if (tablas == null)
                return false;
            if (tablas.Count == 0)
            {
                resultado = "No hay tablas";
                return false;
            }
            return true;
        }

        private bool verifica_atributos(Tabla t, bool where)
        {
            if (!verifica_atributos(t, this.atributos.ToArray()))
                return false;

            if (where)
                return verifica_where(t);

            return true;
        }

        private bool verifica_atributos(Tabla t, string[] atributos)
        {
            List<string> atrs = t.obtenNombreAtributos();
            foreach (string s in atributos)
                if (!atrs.Contains(s))
                    return false;
            return true;
        }

        private bool verifica_where(Tabla t)
        {
            List<string> nombreAtributos = t.obtenNombreAtributos();

            if (nomAtrib != "" && !nombreAtributos.Contains(nomAtrib))
                return false;

            var indice = nombreAtributos.ToList().IndexOf(nomAtrib);

            if (indice >= 0)
            {
                if (t.atributos[indice].tipoDato.ToString() == "Cadena")
                    return false;
            }
            else
            {
                if (nomAtrib != "")
                    return false;
            }
            return true;
        }
        #endregion

        #region utilidades
        private List<string> clona_lista(List<string> lista)
        {
            List<string> clona = new List<string>();
            foreach (string s in lista)
                clona.Add(s);
            return clona;
        }

        private string obtenDatosAtributo(Dictionary<string, object> tupla, Tabla t)
        {
            string s = "";
            var atributos = t.obtenNombreAtributos();

            for (int i = 0; i < atributos.Count; i++)
            {
                s += tupla[atributos[i]];
                if (i < atributos.Count - 1) s += ",";
            }
            return s;
        }

        private List<int> separaAtributosMostrar(Tabla t)
        {
            List<int> res = new List<int>();
            // separamos los atributos que nos interesan de los que no
            for (int i = 0; i < t.atributos.Count; i++)
            {
                if (!this.atributos.Contains(t.atributos[i].nombre))
                    res.Add(i);
            }

            return res;
        }

        private List<List<string>> obtenRegistros(Tabla t, bool where)
        {
            List<List<string>> registros = new List<List<string>>();
            // por cada tupla
            for (int i = 0; i < t.registros.Count; i++)
            {
                registros.Add(new List<string>());

                // separamos la tupla por cada uno de sus metadatos
                string[] datos_split = obtenDatosAtributo(t.registros[i], t).Split(',');
                // por cada metadato
                for (int j = 0; j < datos_split.Length; j++)
                {
                    if (where)
                    {
                        // comprobamos que cumpla la condicion
                        if (cumpleCondicion(t, datos_split))
                        {
                            // se cumple, añadimos el registro
                            registros.Last().Add(datos_split[j]);
                        }
                    }
                    else
                    {
                        // simplemente añadimos el registro
                        registros.Last().Add(datos_split[j]);
                    }
                }
            }
            if (where)
                // limpiamos las tuplas que quedaron vacias por no cumplir la condicional
                registros.RemoveAll(l => l.Count == 0);
            return registros;
        }

        private void inicializaVariables(Tabla ta, Tabla tb)
        {
            if (ta != null)
                atributos_tablaA = ta.obtenNombreAtributos().ToArray();
            if (tb != null)
                atributos_tablaB = tb.obtenNombreAtributos().ToArray();
            orden_atrA = new List<int>();
            orden_atrB = new List<int>();
            atr_no_selA = new List<int>();
            atr_no_selB = new List<int>();
            datos.Clear();
        }

        private void reorganiza_atributos(Tabla ta, Tabla tb)
        {
            for (int i = 0; i < this.atributos.Count; i++)
            {
                if (ta != null)
                    orden_atrA.Add(ta.obtenNombreAtributos().IndexOf(this.atributos[i]));
                if (tb != null)
                    orden_atrB.Add(tb.obtenNombreAtributos().IndexOf(this.atributos[i]));
            }
        }
        #endregion
    }
}
