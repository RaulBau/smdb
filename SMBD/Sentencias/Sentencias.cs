﻿using System;
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
        public Regex innerJoinRegEx;
        public bool sentenciaCorrecta;
        public List<string> atributos;
        public string tabla1, tabla2;
        public string atributo;

        public string nomAtrib = "";
        public string condicion = "";
        public string valor = "";

        public Sentencias()
        {
            selectAllRegEx = new Regex(@"\s*\n*\t*(SELECT|select)\s+\*\s+(FROM|from)\s+(\w+)\s*;?\s*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            selectAtributosRegEx = new Regex(@"\s*\n*\t*(SELECT|select)((\s+\w+,?)+)\s+(FROM|from)\s+(\w+)\s*;?\s*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            selectWhereRegEx = new Regex(@"\s*\n*\t*(SELECT|select)((\s+\w+,?)+)\s+(FROM|from)\s+(\w+)\s*(WHERE|where)\s+(\w+)\s+(=|>|<|>=|<=|<>)\s+(\d+)\s*;?", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            innerJoinRegEx = new Regex(@"\s*\n*\t*(SELECT|select)((\s+(\w+\.)?\w+,?)+)\s+(FROM|from)\s+(\w+)\s+(inner join|INNER JOIN)\s+(\w+)\s+(ON|on)\s+(\w+)\.(\w+)\s+=\s+(\w+)\.(\w+)\s*;?\s*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            atributos = new List<string>();
            sentenciaCorrecta = false;
            atributo = "";
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
                tabla1 = limpiaCadena(groupCol[3].Value)[0];
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
                tabla1 = groupCol[5].Value;
                atributos = limpiaCadena(groupCol[2].Value);
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
                atributos = limpiaCadena(groupCol[2].Value);
                nomAtrib = groupCol[7].Value;
                condicion = groupCol[8].Value;
                valor = groupCol[9].Value;
                sentenciaCorrecta = true;
            }

            return sentenciaCorrecta;
        }

        public bool innerJoin(string entrada)
        {
            string atrA = "", atrB = "";
            MatchCollection matchCol = innerJoinRegEx.Matches(entrada);
            GroupCollection groupCol = innerJoinRegEx.Match(entrada).Groups;

            sentenciaCorrecta = false;

            if (matchCol.Count == 1 && matchCol[0].Length == entrada.Length)
            {
                inicializaDatos();
                atributos = limpiaCadena(groupCol[2].Value);
                tabla1 = groupCol[6].Value;
                tabla2 = groupCol[8].Value;

                if (tabla1 != groupCol[10].Value || tabla2 != groupCol[12].Value)
                    return sentenciaCorrecta;

                atrA = groupCol[11].Value; atrB = groupCol[13].Value;

                if (atrA != atrB)
                    return sentenciaCorrecta;

                atributo = atrA;

                sentenciaCorrecta = true;
                return sentenciaCorrecta;
            }

            return sentenciaCorrecta;
        }

        public string obtenAtributos()
        {
            string s = "";
            //foreach (string str in atributos)
            //    s += str + " ";

            atributos.ForEach(str => s += str + " ");

            return s;
        }

        private List<string> limpiaCadena(string cadena)
        {
            Regex r = new Regex(@"\s+|,");
            List<string> listaAtrib = new List<string>();
            listaAtrib = r.Replace(cadena, " ").Split(' ').ToList();
            listaAtrib.RemoveAll(s => s == "");
            return listaAtrib;
        }
    }

    class Select : Sentencias
    {
        public string resultado;
        public List<string> atributosTabla1, atributosTabla2;
        public List<List<string>> datos;
        public List<string> ansA, ansB;
        public List<int> atr_no_selA, atr_no_selB, ordenAtrib, ordenAtrib2;
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
            atributosTabla1 = new List<string>();
            atributosTabla2 = new List<string>();
            ansA = new List<string>();
            ansB = new List<string>();
        }

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
            resultado = "La tabla no existe.";
            return false;
        }

        public bool ejecutaSelectAtributos(bool where)
        {
            foreach (Tabla t in tablas)
            {
                if (t.nombre.Replace(".t", "") == tabla1)
                {
                    inicializaVariables(t, null);

                    if (!existenAtributos(t, where))
                    {
                        resultado = "Un atributo no existe.";
                        return false;
                    }

                    ansA = separaAtributos(t, 1);
                    datos = obtenRegistros(t, where);
                    resultado = "Ejecución terminada correctamente.";
                    return true;
                }
            }

            resultado = "La tabla no existe.";
            return false;
        }

        private List<int> separaAtributos(Tabla t)
        {
            List<int> res = new List<int>();

            for (int i = 0; i < t.atributos.Count; i++)
            {
                if (!this.atributos.Contains(t.atributos[i].nombre))
                    res.Add(i);
            }

            return res;
        }

        private List<string> separaAtributos(Tabla t, int n)
        {
            List<string> res = new List<string>();

            List<string> atributosSeleccionados = seleccionaAtributos(null);

            for (int i = 0; i < t.atributos.Count; i++)
            {
                if (!atributosSeleccionados.Contains(t.atributos[i].nombre))
                    res.Add(t.atributos[i].nombre);
            }

            return res;
        }

        public List<string> seleccionaAtributos(List<string> lista_atributos)
        {
            List<string> atributosSeleccionados = new List<string>();
            string t = "";

            List<string> atributos_usar;

            if (lista_atributos != null)
                atributos_usar = lista_atributos;
            else
                atributos_usar = this.atributos;

            foreach (string atr in atributos_usar)
            {
                if (atr.Contains('.'))
                {
                    t = atr.Substring((atr.IndexOf('.') + 1), (atr.Length - atr.IndexOf('.') - 1));
                    atributosSeleccionados.Add(t);
                }
                else
                    atributosSeleccionados.Add(atr);
            }

            return atributosSeleccionados;
        }

        public bool ejecutaSelectInnerJoin()
        {
            Tabla t1 = tablas.Find(t => t.nombre == tabla1 + ".t");
            Tabla t2 = tablas.Find(t => t.nombre == tabla2 + ".t");

            if (t1 != null && t2 != null)
            {
                if (!verifica_tablas(t1, t2, atributos))
                {
                    resultado = "No existe alguna tabla referenciada.";
                    return false;
                }

                // comprobamos que los atributos seleccionados existan al menos en una de las dos tablas
                foreach (string a in atributos)
                {
                    if (atributos_repetidos())
                    {
                        resultado = "Existen atributos repetidos.";
                        return false;
                    }

                    if (verifica_atributos(t1, new string[] { a }) && verifica_atributos(t2, new string[] { a }))
                    {
                        // verificamos que los atributos de cada tabla existan
                        if (!(verifica_atributos(t1) && verifica_atributos(t2)))
                        {
                            resultado = "Un atributo no existe.";
                            return false;
                        }
                    }
                    if (!verifica_atributos(t1, new string[] { a }) && !verifica_atributos(t2, new string[] { a }))
                    {
                        resultado = "Un atrtibuto no existe.";
                        return false;
                    }
                }

                inicializa_variables(t1, t2);

                //reorganiza_atributos(ta, tb);

                ansA = separaAtributos(t1, 1);
                ansB = separaAtributos(t2, 1);

                int ind_atr_tA = t1.obtenNombreAtributos().IndexOf(atributo);
                int ind_atr_tB = t2.obtenNombreAtributos().IndexOf(atributo);

                // obtenemos todos las tuplas que cumplen la condicion
                datos = obten_tuplas_inner_join(t1, t2, ind_atr_tA, ind_atr_tB);
                resultado = "Ejecución terminada correctamente!.";
                return true;
            }
            resultado = "La tabla no existe!.";
            return false;
        }

        private List<List<string>> obten_tuplas_inner_join(Tabla a, Tabla b, int ia, int ib)
        {
            List<List<string>> ra = obtenRegistros(a, false);
            List<List<string>> rb = obtenRegistros(b, false);
            List<List<string>> resultado = new List<List<string>>();

            int num_atrA = ra[0].Count;
            int num_atrB = rb[0].Count;

            string id_ta;

            for (int i = 0; i < ra.Count; i++)
            {
                id_ta = ra[i][ia];
                foreach (List<string> list in rb.FindAll(tupla => tupla[ib] == id_ta))
                {
                    resultado.Add(copiaLista(ra[i]));
                    foreach (string s in list)
                    {
                        resultado.Last().Add(s);
                    }
                }
            }

            resultado.RemoveAll(list => list.Count < (num_atrA + num_atrB));

            return resultado;
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
            List<string> atrs = t.obtenNombreAtributos().ToList();

            foreach (string s in seleccionaAtributos(atributos.ToList()))
                if (!atrs.Contains(s))
                    return false;
            return true;
        }

        private bool verifica_atributos(Tabla tabla)
        {
            List<string> atr_de_tabla = new List<string>();

            foreach (string s in atributos)
            {

                if (s.IndexOf(tabla.nombre) == 0)
                {
                    atr_de_tabla.Add(s.Substring((s.IndexOf('.') + 1), (s.Length - s.IndexOf('.') - 1)));
                }
            }
            return verifica_atributos(tabla, atr_de_tabla.ToArray());
        }


        private bool atributos_repetidos()
        {
            foreach (var atr in atributos)
            {
                if (atributos.Count(a => a == atr) > 1)
                    return true;
            }
            return false;
        }

        private bool verifica_tablas(Tabla tab1, Tabla tab2, List<string> atributos)
        {
            string tab = "";
            foreach (string s in atributos)
            {
                if (s.Contains('.'))
                {
                    tab = s.Substring(0, s.IndexOf('.'));
                    if (tab1.nombre != tab && tab2.nombre != tab)
                        return false;
                }
            }
            return true;
        }

        private void inicializa_variables(Tabla tabA, Tabla tabB)
        {
            atributosTabla1 = new List<string>();
            atributosTabla2 = new List<string>();
            ansA = new List<string>();
            ansB = new List<string>();
            if (tabA != null)
                atributosTabla1 = tabA.obtenNombreAtributos();
            if (tabB != null)
                atributosTabla2 = tabB.obtenNombreAtributos();
            datos.Clear();
        }

        public bool cumpleCondicion(Tabla t, string[] tupla)
        {
            int indiceAtributo = t.obtenNombreAtributos().IndexOf(nomAtrib);
            var r = false;

            if (indiceAtributo >= 0)
            {
                float valor = float.Parse(this.valor, System.Globalization.CultureInfo.InvariantCulture);
                float valorT = float.Parse(tupla[indiceAtributo], System.Globalization.CultureInfo.InvariantCulture);
                switch (condicion)
                {
                    case ">":
                        r = valorT > valor;
                        break;
                    case "<":
                        r = valorT < valor;
                        break;
                    case "=":
                        r = valorT == valor;
                        break;
                    case ">=":
                        r = valorT >= valor;
                        break;
                    case "<=":
                        r = valorT <= valor;
                        break;
                    case "<>":
                        r = valorT != valor;
                        break;
                    default:
                        r = false;
                        break;
                }
            }
            return r;
        }

        public bool baseAbierta()
        {
            var b = true;
            if (tablas == null)
                b = false;
            if (b == true && tablas.Count == 0)
            {
                resultado = "No hay tablas";
                b = false;
            }
            return b;
        }

        private bool existenAtributos(Tabla t, bool where)
        {
            if (!existenAtributos(t, this.atributos.ToArray()))
                return false;

            if (where)
                return verifica_where(t);

            return true;
        }

        private bool existenAtributos(Tabla t, string[] atributos)
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

        private List<string> copiaLista(List<string> lista)
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
            for (int i = 0; i < t.registros.Count; i++)
            {
                registros.Add(new List<string>());
                string[] datos_split = obtenDatosAtributo(t.registros[i], t).Split(',');
                for (int j = 0; j < datos_split.Length; j++)
                {
                    if (where)
                    {
                        if (cumpleCondicion(t, datos_split))
                        {
                            registros.Last().Add(datos_split[j]);
                        }
                    }
                    else
                    {
                        registros.Last().Add(datos_split[j]);
                    }
                }
            }
            if (where)
                registros.RemoveAll(l => l.Count == 0);
            return registros;
        }

        private void inicializaVariables(Tabla ta, Tabla tb)
        {
            if (ta != null)
                atributosTabla1 = ta.obtenNombreAtributos();
            if (tb != null)
                atributosTabla2 = tb.obtenNombreAtributos();
            ordenAtrib = new List<int>();
            ordenAtrib2 = new List<int>();
            atr_no_selA = new List<int>();
            atr_no_selB = new List<int>();
            datos.Clear();
        }

        private void reorganiza_atributos(Tabla ta, Tabla tb)
        {
            for (int i = 0; i < this.atributos.Count; i++)
            {
                if (ta != null)
                    ordenAtrib.Add(ta.obtenNombreAtributos().IndexOf(this.atributos[i]));
                if (tb != null)
                    ordenAtrib2.Add(tb.obtenNombreAtributos().IndexOf(this.atributos[i]));
            }
        }
    }
}
