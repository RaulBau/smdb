using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMBD
{
    public class Atributo
    {
        public string nombre;
        public string tipoDato;
        public int tam;
        public bool PK;
        public bool FK;
        public bool datos;
        public int id;
        public int ref_id;
        private static int ID = 0;

        public Atributo()
        {
            nombre = "";
            tipoDato = "";
            tam = 0;
            PK = false;
            FK = false;
            id = ID++;
            datos = false;
            ref_id = -1;
        }

        public void agregaLlaveForanea(int refId)
        {
            this.ref_id = refId;
            this.FK = true;
        }

        public void eliminaLlaveForanea()
        {
            this.ref_id = -1;
            this.FK = false;
        }

        static public void actualizaId(int i)
        {
            if (i > ID)
            {
                ID = i;
            }
        }

        static public void reiniciaId()
        {
            ID = 0;
        }
    }
}
