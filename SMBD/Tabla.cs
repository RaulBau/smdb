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

        public Tabla()
        {
            nombre = "";
            atributos = new List<Atributo>();
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
    }
}
