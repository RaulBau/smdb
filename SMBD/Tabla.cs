using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMBD
{
    public class Tabla
    {
        public string nombre;
        public List<Atributo> atributos;
        public List<Dictionary<string, object>> registros;

        public Tabla()
        {
            nombre = "";
            atributos = new List<Atributo>();
            registros = new List<Dictionary<string, object>>();
        }

        public bool clavePrimaria()
        {
            var pk = false;

            foreach (var item in atributos)
            {
                if (item.PK == true)
                {
                    pk = true;
                    break;
                }
            }

            return pk;
        }

        public List<string> obtenNombreAtributos()
        {
            var lista = new List<string>();

            atributos.ForEach(a => lista.Add(a.nombre));

            return lista;
        }
    }
}
