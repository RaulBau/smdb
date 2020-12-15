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
                rTB_ejecucion.AppendText(ejecutaSentencia(rTB_Sentencias.Text));
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

        public string ejecutaSentencia(string entrada)
        {
            ejecuta = true;
            dGV_Registros.Rows.Clear();
            dGV_Registros.Columns.Clear();
            rTB_ejecucion.Clear();
            string msg = "Error de Sintaxis.\n";

            if (!select.baseAbierta())
            {
                ejecuta = false;
                msg = "Abre una base de datos\n";
            }

            if (select.selectAll(entrada))
            {
                if (select.ejecutaSelectAll())
                {
                    ejecuta = true;
                    rTB_ejecucion.AppendText(select.resultado + "\n");
                    msg = "Se mostraron todos los atributos de " + select.tabla1 + ".\n";
                }
                else
                    rTB_ejecucion.AppendText(select.resultado + "\n");
            }
            else
            {
                if (select.selectAtributos(entrada))
                {
                    if (select.ejecutaSelectAtributos(false))
                    {
                        ejecuta = true;
                        rTB_ejecucion.AppendText(select.resultado + "\n");
                        msg = "Se mostraron " + select.obtenAtributos() + "los atributos de " + select.tabla1 + ".\n";
                    }
                    else
                        rTB_ejecucion.AppendText(select.resultado + "\n");
                }
                else
                {
                    if (select.selectWhere(entrada))
                    {
                        if (select.ejecutaSelectAtributos(true))
                        {
                            ejecuta = true;
                            rTB_ejecucion.AppendText(select.resultado + "\n");
                            msg = "Se mostraron " + select.obtenAtributos() + "los atributos de " + select.tabla1 + " donde " + select.nomAtrib + " " + select.condicion + " " + select.valor + "\n";
                        }
                        else
                            rTB_ejecucion.AppendText(select.resultado + "\n");
                    }
                    else
                    {
                        if (select.innerJoin(entrada))
                        {
                            if (select.ejecutaSelectInnerJoin())
                            {
                                ejecuta = true;
                                rTB_ejecucion.AppendText(select.resultado + "\n");
                                msg = "Se mostraron " + select.obtenAtributos() + "los atributos de " + select.tabla1 + " donde se unio con la tabla " + select.tabla2 + "\n";
                            }
                            else rTB_ejecucion.AppendText(select.resultado + "\n");
                        }
                    }
                }
            }

            return msg;
        }
        private void muestraResultados()
        {
            dGV_Registros.Columns.Clear();
            if (!ejecuta)
                return;

            try
            {
                generaDataGrid();
                eliminaColumnasDGV();
                organiza_columnas();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                dGV_Registros.Columns.Clear();
            }
        }

        private void generaDataGrid()
        {
            for (int i = 0; i < select.atributosTabla1.Count; i++)
            {
                dGV_Registros.Columns.Add(select.tabla1 + "." + select.atributosTabla1[i], select.atributosTabla1[i]);
            }

            for (int i = 0; i < select.atributosTabla2.Count; i++)
            {
                dGV_Registros.Columns.Add(select.tabla2 + "." + select.atributosTabla2[i], select.atributosTabla2[i]);
            }

            for (int i = 0; i < select.datos.Count; i++)
            {
                dGV_Registros.Rows.Add(select.datos[i].ToArray());
            }
        }


        private void eliminaColumnasDGV()
        {
            for (int i = 0; i < select.atribNoSelT1.Count; i++)
                dGV_Registros.Columns.Remove(dGV_Registros.Columns[select.tabla1 + "." + select.atribNoSelT1[i]]);

            int desplazamiento = select.atribNoSelT1.Count + 1;

            for (int i = 0; i < select.atribNoSelT2.Count; i++)
                dGV_Registros.Columns.Remove(dGV_Registros.Columns[select.tabla2 + "." + select.atribNoSelT2[i]]);
        }

        private void organiza_columnas()
        {
            // reorganizando las columnas
            for (int i = 0; i < select.atributos.Count; i++)
            {
                if (dGV_Registros.Columns.Contains(select.tabla1 + "." + select.atributos[i]))
                {
                    dGV_Registros.Columns[select.tabla1 + "." + select.atributos[i]].DisplayIndex = i;
                }
                else if (dGV_Registros.Columns.Contains(select.atributos[i]))
                {
                    dGV_Registros.Columns[select.atributos[i]].DisplayIndex = i;
                }
                else if (dGV_Registros.Columns.Contains(select.tabla2 + "." + select.atributos[i]))
                {
                    dGV_Registros.Columns[select.tabla2 + "." + select.atributos[i]].DisplayIndex = i;
                }
                else if (dGV_Registros.Columns.Contains(select.atributos[i]))
                {
                    dGV_Registros.Columns[select.atributos[i]].DisplayIndex = i;
                }
                if (select.atributos[i].Contains("."))
                    dGV_Registros.Columns[select.atributos[i]].HeaderText = select.atributos[i];
            }
        }


        private void rTB_Sentencias_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                rTB_ejecucion.AppendText(ejecutaSentencia(rTB_Sentencias.SelectedText));
                muestraResultados();
            }
        }
    }
}
