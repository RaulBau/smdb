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
        public Regex innerJoinRegEx;
        public bool sentenciaCorrecta;
        public List<string> atributos;
        public string tabla1, tabla2;
        public string atributo;

        public string nomAtrib = "";
        public string condicion = "";
        public string valor = "";

        //Constuctor de la clase
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

        //Funcion para inicialozar los datos
        public void inicializaDatos()
        {
            atributos.Clear();
            tabla1 = "";
            tabla2 = "";
            nomAtrib = "";
            condicion = "";
            valor = "";
        }

        //Funcion para verificar la sintaxis de select all
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

        //Funcion para verificar la sintaxis de select con atributos
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

        //Funcion para verificar la sintaxis de select where
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

        //Funcion para verificar la sintaxis de inner join
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

        //Funcion para obtener la lista de los nombres de los atributos
        public string obtenAtributos()
        {
            string s = "";

            atributos.ForEach(str => s += str + " ");

            return s;
        }

        //Funcion para eliminar los elementos 
        private List<string> limpiaCadena(string cadena)
        {
            Regex r = new Regex(@"\s+|,");
            List<string> listaAtrib = new List<string>();
            listaAtrib = r.Replace(cadena, " ").Split(' ').ToList();
            listaAtrib.RemoveAll(s => s == "");
            return listaAtrib;
        }
    }

    class Consultas : Sentencias
    {
        public string resultado;
        public List<string> atributosTabla1, atributosTabla2;
        public List<List<string>> datos;
        public List<string> atribNoSelT1, atribNoSelT2;
        public List<int> ordenAtrib, ordenAtrib2;
        public List<Tabla> tablas;

        //Constuctor de la clase
        public Consultas(List<Tabla> t)
        {
            resultado = "";
            tablas = t;
            datos = new List<List<string>>();
            atributosTabla1 = new List<string>();
            atributosTabla2 = new List<string>();
            atribNoSelT1 = new List<string>();
            atribNoSelT2 = new List<string>();
        }

        //Funcion para elecutar la consulta select all
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

        //Funcion para elecutar la consulta select con atributos
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

                    atribNoSelT1 = separaAtributos(t, 1);
                    datos = obtenRegistros(t, where);
                    resultado = "Ejecución terminada correctamente.";
                    return true;
                }
            }

            resultado = "La tabla no existe.";
            return false;
        }

        //Inicaliza los atributos
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

        //Funcion para obtener atributo, separandolos si se usa el nombre de la tabla antes
        public List<string> seleccionaAtributos(List<string> listaAtrib)
        {
            List<string> atributosSeleccionados = new List<string>();
            string t = "";

            List<string> atributosSel;

            if (listaAtrib != null)
                atributosSel = listaAtrib;
            else
                atributosSel = this.atributos;

            foreach (string atr in atributosSel)
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

        //Funcion para elecutar la consulta select con inner join
        public bool ejecutaSelectInnerJoin()
        {
            Tabla t1 = tablas.Find(t => t.nombre == tabla1 + ".t");
            Tabla t2 = tablas.Find(t => t.nombre == tabla2 + ".t");

            if (t1 != null && t2 != null)
            {
                if (!ExisteTablaAtributo(t1, t2, atributos))
                {
                    resultado = "Una de las tablas no existe.";
                    return false;
                }

                foreach (string a in atributos)
                {
                    if (atributosRepetidos())
                    {
                        resultado = "Existen atributos repetidos.";
                        return false;
                    }

                    if (revisaAtributos(t1, new List<string> { a }) && revisaAtributos(t2, new List<string> { a }))
                    {
                        if (!(revisaAtributos(t1) && revisaAtributos(t2)))
                        {
                            resultado = "No existe un atributo para unir.";
                            return false;
                        }
                    }
                    if (!revisaAtributos(t1, new List<string> { a }) && !revisaAtributos(t2, new List<string> { a }))
                    {
                        resultado = "Un atrtibuto no existe.";
                        return false;
                    }
                }

                inicializaTablas(t1, t2);

                atribNoSelT1 = separaAtributos(t1, 1);
                atribNoSelT2 = separaAtributos(t2, 1);

                int indiceAtribT1 = t1.obtenNombreAtributos().IndexOf(atributo);
                int indiceAtribT2 = t2.obtenNombreAtributos().IndexOf(atributo);
                datos = obtenRegistrosIJ(t1, t2, indiceAtribT1, indiceAtribT2);
                resultado = "Ejecución terminada correctamente!.";
                return true;
            }
            resultado = "La tabla no existe!.";
            return false;
        }

        //Funcion pára obtener los registros del inner join
        private List<List<string>> obtenRegistrosIJ
            (Tabla a, Tabla b, int ia, int ib)
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

        //Funcion para verificar si existen atributos
        private bool revisaAtributos(Tabla t, List<string> atributos)
        {
            List<string> atrs = t.obtenNombreAtributos().ToList();

            foreach (string s in seleccionaAtributos(atributos))
                if (!atrs.Contains(s))
                    return false;
            return true;
        }

        //Funcion para verificar si existen atributos
        private bool revisaAtributos(Tabla tabla)
        {
            List<string> atr_de_tabla = new List<string>();

            foreach (string s in atributos)
            {

                if (s.IndexOf(tabla.nombre) == 0)
                {
                    atr_de_tabla.Add(s.Substring((s.IndexOf('.') + 1), (s.Length - s.IndexOf('.') - 1)));
                }
            }
            return revisaAtributos(tabla, atr_de_tabla);
        }

        //Funcion para saber si hay atributos repetidos
        private bool atributosRepetidos()
        {
            foreach (var atr in atributos)
            {
                if (atributos.Count(a => a == atr) > 1)
                    return true;
            }
            return false;
        }

        //Funcion para verificar si existe la tabla antes del atributo -> Tabla.atributo
        private bool ExisteTablaAtributo(Tabla tab1, Tabla tab2, List<string> atributos)
        {
            string tab = "";
            foreach (string s in atributos)
            {
                if (s.Contains('.'))
                {
                    tab = s.Split('.')[0];
                    if (tab1.nombre != tab && tab2.nombre != tab)
                        return false;
                }
            }
            return true;
        }

        //Funcion para inicializar los datos usados en las consultas
        private void inicializaTablas(Tabla tabA, Tabla tabB)
        {
            atributosTabla1 = new List<string>();
            atributosTabla2 = new List<string>();
            atribNoSelT1 = new List<string>();
            atribNoSelT2 = new List<string>();
            if (tabA != null)
                atributosTabla1 = tabA.obtenNombreAtributos();
            if (tabB != null)
                atributosTabla2 = tabB.obtenNombreAtributos();
            datos.Clear();
        }

        //Funcion para verificar si los atributos cumplen la condicion de la consulta
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

        //Funcion para verificar si hay hablas
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

        //Funcion para verificar los atributos
        private bool existenAtributos(Tabla t, bool where)
        {
            if (!existenAtributos(t, this.atributos))
                return false;
            if (where)
                return revisaWhere(t);

            return true;
        }

        //Funcion para verificar si los atributos existen en la tabla
        private bool existenAtributos(Tabla t, List<string> atributos)
        {
            List<string> atrs = t.obtenNombreAtributos();
            foreach (string s in atributos)
                if (!atrs.Contains(s))
                    return false;
            return true;
        }

        //Funcion para verificar si el tipo de dato
        private bool revisaWhere(Tabla t)
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

        //Funcion para crear una nueva lista a partir de una ya existente
        private List<string> copiaLista(List<string> lista)
        {
            List<string> nl = new List<string>();
            foreach (string s in lista)
                nl.Add(s);
            return nl;
        }

        //Funcion para obtener los registros de un atributo
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

        //Funcion para obtener los registros de una tabla
        private List<List<string>> obtenRegistros(Tabla t, bool where)
        {
            List<List<string>> registros = new List<List<string>>();
            for (int i = 0; i < t.registros.Count; i++)
            {
                registros.Add(new List<string>());
                string[] split = obtenDatosAtributo(t.registros[i], t).Split(',');
                for (int j = 0; j < split.Length; j++)
                {
                    if (where)
                    {
                        if (cumpleCondicion(t, split))
                        {
                            registros.Last().Add(split[j]);
                        }
                    }
                    else
                    {
                        registros.Last().Add(split[j]);
                    }
                }
            }
            if (where)
                registros.RemoveAll(l => l.Count == 0);
            return registros;
        }

        //Funcion para inicializar las listas de atributos
        private void inicializaVariables(Tabla ta, Tabla tb)
        {
            if (ta != null)
                atributosTabla1 = ta.obtenNombreAtributos();
            if (tb != null)
                atributosTabla2 = tb.obtenNombreAtributos();
            ordenAtrib = new List<int>();
            ordenAtrib2 = new List<int>();
            datos.Clear();
        }
    }
}
