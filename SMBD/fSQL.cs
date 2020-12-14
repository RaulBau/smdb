using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using SMBD.Sentencias;

namespace SMBD
{
    public partial class fSQL : Form
    {
        public string pathBase;
        public List<Tabla> tablas;
        private bool ejecuta;
        private Select select;

        public fSQL(string pb, List<Tabla> tabs)
        {
            this.pathBase = pb;
            this.tablas = tabs;
            this.ejecuta = false;
            leeDatos();
            select = new Select(tablas);
            InitializeComponent();
        }

        private void fSQL_Load(object sender, EventArgs e)
        {
            Console.WriteLine(this.pathBase);
        }

        private void btn_Ejecutar_Click(object sender, EventArgs e)
        {
            try
            {
                tB_Compilacion.Text = ejecutaSentencia();
                muestraResultados();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void leeDatos()
        {
            List<Dictionary<string, object>> lista;
            Tabla t = null;
            string arch = "";
            string d;
            try
            {
                for (int i = 0; i < tablas.Count; i++)
                {
                    lista = new List<Dictionary<string, object>>();
                    t = tablas[i];
                    arch = this.pathBase + Path.DirectorySeparatorChar + t.nombre;

                    using (StreamReader archivo = new StreamReader(arch))
                    {
                        d = archivo.ReadLine();
                    }
                    if (d != null && d != "")
                    {
                        lista = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(d);
                    }

                    t.registros.Clear();

                    lista.ForEach(it => t.registros.Add(it));
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
                this.Close();
            }
        }

        public string ejecutaSentencia()
        {
            ejecuta = true;
            // si no hay tablas, amonos
            if (!select.hay_tablas())
            {
                ejecuta = false;
                return "Abre una base de datos primero!.";
            }
            string entrada = rTB_Sentencias.Text;
            if (select.selectAll(entrada) && select.ejecutaSelectAll())
            {
                tB_Salida.Text = select.resultado;
                return "de la tabla " + select.tabla1 + " muestra todos las tuplas.";
            }
            else if (select.selectAtributos(entrada) && select.ejecutaSelectAtributos(false))
            {
                tB_Salida.Text = select.resultado;
                return "de la tabla " + select.tabla1 + " muestra todos las tuplas pero solo con los atributos " + select.obtenAtributos();
            }
            else if (select.selectWhere(entrada) && select.ejecutaSelectAtributos(true))
            {
                tB_Salida.Text = select.resultado;
                return "de la tabla " + select.tabla1 + " muestra todos las tuplas pero solo con los atributos " + select.obtenAtributos() + " y se cumple que el atributo " + select.nomAtrib + " " + select.condicion + " " + select.valor;
            }
            ejecuta = false;
            //limpia_grid();
            return "Error de Sintaxis!.";
        }

        private void muestraResultados()
        {
            if (!ejecuta)
                return;
            dGV_Registros.Columns.Clear();

            // agregando el encabezado del grid
            for (int i = 0; i < select.atributos_tablaA.Length; i++)
            {
                if (!select.resuelve_ambiguedad)
                    dGV_Registros.Columns.Add(select.atributos_tablaA[i], select.atributos_tablaA[i]);
                else
                    dGV_Registros.Columns.Add(select.tabla1 + "." + select.atributos_tablaA[i], select.tabla1 + "." + select.atributos_tablaA[i]);
            }

            for (int i = 0; i < select.atributos_tablaB.Length; i++)
            {
                if (!select.resuelve_ambiguedad)
                    dGV_Registros.Columns.Add(select.atributos_tablaB[i], select.atributos_tablaB[i]);
                else
                    dGV_Registros.Columns.Add(select.tabla2 + "." + select.atributos_tablaB[i], select.tabla2 + "." + select.atributos_tablaB[i]);
            }

            // agregando los datos
            for (int i = 0; i < select.datos.Count; i++)
            {
                dGV_Registros.Rows.Add(select.datos[i].ToArray());
            }

            // ocultando las columnas que no nos interesan
            for (int i = 0; i < select.atr_no_selA.Count; i++)
                dGV_Registros.Columns[select.atr_no_selA[i]].Visible = false;

            int desplazamiento = select.atr_no_selA.Count + 1;

            for (int i = 0; i < select.atr_no_selB.Count; i++)
                dGV_Registros.Columns[select.atr_no_selB[i] + desplazamiento].Visible = false;

            // reorganizando las columnas
            for (int i = 0; i < select.orden_atrA.Count; i++)
                dGV_Registros.Columns[select.orden_atrA[i]].DisplayIndex = i;
        }

        private void rTB_Sentencias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                tB_Compilacion.Text = ejecutaSentencia();
                muestraResultados();
            }
        }
    }
}
