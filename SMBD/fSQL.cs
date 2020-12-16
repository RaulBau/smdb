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
        private Consultas consulta;

        //Constructor
        public fSQL(string pb, List<Tabla> tabs)
        {
            this.pathBase = pb;
            this.tablas = tabs;
            this.ejecuta = false;
            leeDatos();
            consulta = new Consultas(tablas);
            InitializeComponent();
        }

        //Funcion que se ejecuta al cargar la forma
        private void fSQL_Load(object sender, EventArgs e)
        {
            Console.WriteLine(this.pathBase);
        }

        //Funcion que ejecuta la consulta
        private void btn_Ejecutar_Click(object sender, EventArgs e)
        {
            try
            {
                rTB_ejecucion.AppendText(ejecutaSentencia(rTB_Sentencias.SelectedText));
                muestraResultados();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        //Funcion para leer todos los registros de las tablas
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

        //Funcion para ejecutar la sentencia
        public string ejecutaSentencia(string entrada)
        {
            ejecuta = false;
            dGV_Registros.Rows.Clear();
            dGV_Registros.Columns.Clear();
            rTB_ejecucion.Clear();
            string msg = "Error de Sintaxis.\n";

            if (!consulta.baseAbierta())
            {
                ejecuta = false;
                msg = "Abre una base de datos\n";
            }

            if (consulta.selectAll(entrada))
            {
                if (consulta.ejecutaSelectAll())
                {
                    ejecuta = true;
                    rTB_ejecucion.AppendText(consulta.resultado + "\n");
                    msg = "Se mostraron todos los atributos de " + consulta.tabla1 + ".\n";
                }
                else
                    rTB_ejecucion.AppendText(consulta.resultado + "\n");
            }
            else
            {
                if (consulta.selectAtributos(entrada))
                {
                    if (consulta.ejecutaSelectAtributos(false))
                    {
                        ejecuta = true;
                        rTB_ejecucion.AppendText(consulta.resultado + "\n");
                        msg = "Se mostraron " + consulta.obtenAtributos() + "los atributos de " + consulta.tabla1 + ".\n";
                    }
                    else
                        rTB_ejecucion.AppendText(consulta.resultado + "\n");
                }
                else
                {
                    if (consulta.selectWhere(entrada))
                    {
                        if (consulta.ejecutaSelectAtributos(true))
                        {
                            ejecuta = true;
                            rTB_ejecucion.AppendText(consulta.resultado + "\n");
                            msg = "Se mostraron " + consulta.obtenAtributos() + "los atributos de " + consulta.tabla1 + " donde " + consulta.nomAtrib + " " + consulta.condicion + " " + consulta.valor + "\n";
                        }
                        else
                            rTB_ejecucion.AppendText(consulta.resultado + "\n");
                    }
                    else
                    {
                        if (consulta.innerJoin(entrada))
                        {
                            if (consulta.ejecutaSelectInnerJoin())
                            {
                                ejecuta = true;
                                rTB_ejecucion.AppendText(consulta.resultado + "\n");
                                msg = "Se mostraron " + consulta.obtenAtributos() + "los atributos de " + consulta.tabla1 + " donde se unio con la tabla " + consulta.tabla2 + "\n";
                            }
                            else rTB_ejecucion.AppendText(consulta.resultado + "\n");
                        }
                    }
                }
            }
            if (msg == "Error de Sintaxis.\n")
                dGV_Registros.Columns.Clear();

            return msg;
        }

        //Funcion para mostrar los resultados de la consulta en un data grid
        //private void muestraResultados()
        //{
        //    dGV_Registros.Columns.Clear();
        //    if (!ejecuta)
        //        return;

        //    try
        //    {
        //        for (int i = 0; i < consulta.atributosTabla1.Count; i++)
        //        {
        //            dGV_Registros.Columns.Add(consulta.tabla1 + "." + consulta.atributosTabla1[i], consulta.atributosTabla1[i]);
        //        }

        //        for (int i = 0; i < consulta.atributosTabla2.Count; i++)
        //        {
        //            dGV_Registros.Columns.Add(consulta.tabla2 + "." + consulta.atributosTabla2[i], consulta.atributosTabla2[i]);
        //        }

        //        for (int i = 0; i < consulta.datos.Count; i++)
        //        {
        //            dGV_Registros.Rows.Add(consulta.datos[i].ToArray());
        //        }

        //        for (int i = 0; i < consulta.atribNoSelT1.Count; i++)
        //            dGV_Registros.Columns.Remove(dGV_Registros.Columns[consulta.tabla1 + "." + consulta.atribNoSelT1[i]]);

        //        int desplazamiento = consulta.atribNoSelT1.Count + 1;

        //        for (int i = 0; i < consulta.atribNoSelT2.Count; i++)
        //            dGV_Registros.Columns.Remove(dGV_Registros.Columns[consulta.tabla2 + "." + consulta.atribNoSelT2[i]]);

        //        for (int i = 0; i < consulta.atributos.Count; i++)
        //        {
        //            if (dGV_Registros.Columns.Contains(consulta.tabla1 + "." + consulta.atributos[i]))
        //            {
        //                dGV_Registros.Columns[consulta.tabla1 + "." + consulta.atributos[i]].DisplayIndex = i;
        //            }
        //            else if (dGV_Registros.Columns.Contains(consulta.atributos[i]))
        //            {
        //                dGV_Registros.Columns[consulta.atributos[i]].DisplayIndex = i;
        //            }
        //            else if (dGV_Registros.Columns.Contains(consulta.tabla2 + "." + consulta.atributos[i]))
        //            {
        //                dGV_Registros.Columns[consulta.tabla2 + "." + consulta.atributos[i]].DisplayIndex = i;
        //            }
        //            else if (dGV_Registros.Columns.Contains(consulta.atributos[i]))
        //            {
        //                dGV_Registros.Columns[consulta.atributos[i]].DisplayIndex = i;
        //            }
        //            if (consulta.atributos[i].Contains("."))
        //                dGV_Registros.Columns[consulta.atributos[i]].HeaderText = consulta.atributos[i];
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message);
        //        dGV_Registros.Columns.Clear();
        //    }
        //}

        private void muestraResultados()
        {
            dGV_Registros.Columns.Clear();
            if (!ejecuta)
                return;

            try
            {
                generaDataGrid();
                eliminaColumnasDGV();
                acomodaColumnas();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                dGV_Registros.Columns.Clear();
            }
        }

        private void generaDataGrid()
        {
            for (int i = 0; i < consulta.atributosTabla1.Count; i++)
            {
                dGV_Registros.Columns.Add(consulta.tabla1 + "." + consulta.atributosTabla1[i], consulta.atributosTabla1[i]);
            }

            for (int i = 0; i < consulta.atributosTabla2.Count; i++)
            {
                dGV_Registros.Columns.Add(consulta.tabla2 + "." + consulta.atributosTabla2[i], consulta.atributosTabla2[i]);
            }

            for (int i = 0; i < consulta.datos.Count; i++)
            {
                dGV_Registros.Rows.Add(consulta.datos[i].ToArray());
            }
        }

        private void eliminaColumnasDGV()
        {
            for (int i = 0; i < consulta.atribNoSelT1.Count; i++)
                dGV_Registros.Columns.Remove(dGV_Registros.Columns[consulta.tabla1 + "." + consulta.atribNoSelT1[i]]);

            int desplazamiento = consulta.atribNoSelT1.Count + 1;

            for (int i = 0; i < consulta.atribNoSelT2.Count; i++)
                dGV_Registros.Columns.Remove(dGV_Registros.Columns[consulta.tabla2 + "." + consulta.atribNoSelT2[i]]);
        }

        private void acomodaColumnas()
        {
            for (int i = 0; i < consulta.atributos.Count; i++)
            {
                if (dGV_Registros.Columns.Contains(consulta.tabla1 + "." + consulta.atributos[i]))
                {
                    dGV_Registros.Columns[consulta.tabla1 + "." + consulta.atributos[i]].DisplayIndex = i;
                }
                else if (dGV_Registros.Columns.Contains(consulta.atributos[i]))
                {
                    dGV_Registros.Columns[consulta.atributos[i]].DisplayIndex = i;
                }
                else if (dGV_Registros.Columns.Contains(consulta.tabla2 + "." + consulta.atributos[i]))
                {
                    dGV_Registros.Columns[consulta.tabla2 + "." + consulta.atributos[i]].DisplayIndex = i;
                }
                else if (dGV_Registros.Columns.Contains(consulta.atributos[i]))
                {
                    dGV_Registros.Columns[consulta.atributos[i]].DisplayIndex = i;
                }
                if (consulta.atributos[i].Contains("."))
                    dGV_Registros.Columns[consulta.atributos[i]].HeaderText = consulta.atributos[i];
            }
        }


        //Funcion para eejcutar las sentencias al presionar la tecla F5
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
