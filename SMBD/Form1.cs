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
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;

namespace SMBD
{
    public partial class Form1 : Form
    {
        public class Atributo
        {
            public string nombre;
            public string tipoDato;
            public int tam;

            public Atributo()
            {
                nombre = "";
                tipoDato = "";
                tam = 0;
            }
        }

        public class Tabla
        {
            public string nombre;
            public Atributo PK;
            public List<Atributo> FK;
            public List<Atributo> atributos;

            public Tabla()
            {
                nombre = "";
                PK = null;
                FK = new List<Atributo>();
                atributos = new List<Atributo>();
            }
        }



        string pathBase;
        string tablaSeleccionada;
        private FolderBrowserDialog folderBD;
        CommonOpenFileDialog commonOFD;
        TreeNode nodo;
        List<Tabla> tablas;
        List<Atributo> FKs;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pathBase = "";
            tablaSeleccionada = "";
            folderBD = new FolderBrowserDialog();
            folderBD.ShowNewFolderButton = false;
            commonOFD = new CommonOpenFileDialog();
            commonOFD.IsFolderPicker = true;
            inicializaDirectorio(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
            tablas = new List<Tabla>();
            FKs = new List<Atributo>();
        }

        #region Archivo

        private void saveFD1_FileOk(object sender, CancelEventArgs e)
        {
            pathBase = saveFD1.FileName;
            Directory.CreateDirectory(pathBase);
            var f = File.Create(pathBase + Path.DirectorySeparatorChar + Path.GetFileName(pathBase) + ".bd");
            f.Close();

            inicializaDirectorio(pathBase);
        }

        private void openFD1_FileOk(object sender, CancelEventArgs e)
        {
            pathBase = commonOFD.FileName;
            Directory.SetCurrentDirectory(pathBase);
            Console.WriteLine(Directory.GetCurrentDirectory());
        }

        private void inicializaDirectorio(string directorio)
        {
            commonOFD.InitialDirectory = directorio;
            openFD1.InitialDirectory = directorio;
            saveFD1.InitialDirectory = directorio;
            saveFDTabla.InitialDirectory = directorio;
            Directory.SetCurrentDirectory(directorio);
            toolSTB_NombreBD.Text = obtenNombreBD();
            Console.WriteLine(directorio);

            listaTablasDirectorio();
        }

        private void listaTablasDirectorio()
        {
            tV_ListaTablas.Nodes.Clear();
            TreeNode t = new TreeNode();
            string[] elementos;
            if (pathBase != "")
            {
                t.Text = Path.GetFileName(pathBase);
                t.Name = Path.GetFileName(pathBase);
                elementos = Directory.GetFiles(pathBase);
                elementos.ToList().ForEach(a =>
                {
                    if (Path.GetExtension(a) == ".t")
                    {
                        Console.WriteLine(a);
                        var nodo = new TreeNode();
                        nodo.Text = Path.GetFileNameWithoutExtension(a);
                        nodo.Name = a;

                        t.Nodes.Add(nodo);
                    }
                }
                );
                tV_ListaTablas.Nodes.Add(t);
                tV_ListaTablas.ExpandAll();
            }

        }

        private void eliminarBase()
        {
            string[] archivos;
            if (MessageBox.Show("¿Quieres eliminar la base de datos actual?", "¿Estas seguro?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                try
                {

                    archivos = Directory.GetFiles(pathBase);

                    archivos.ToList().ForEach(arch =>
                    {
                        switch (Path.GetExtension(arch))
                        {
                            case ".bd":
                                File.Delete(arch);
                                break;
                            case ".t":
                                File.Delete(arch);
                                break;
                        }
                    });

                    var aux = Path.GetDirectoryName(pathBase);
                    Directory.SetCurrentDirectory(aux);
                    Directory.Delete(pathBase);

                    pathBase = "";
                    nombreTabla.Text = "";
                    tV_ListaTablas.Nodes.Clear();
                    dGV_AtributosTabla.Columns.Clear();
                    inicializaDirectorio(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                }
                catch (Exception excp)
                {
                    MessageBox.Show(excp.Message);
                }
            }
        }

        private void renombrarBase()
        {
            string path = "";
            if (toolSTB_NombreBD.Text != "" && pathBase.Split(Path.DirectorySeparatorChar).Last() != toolSTB_NombreBD.Text && pathBase != "")
            {
                path = Path.GetDirectoryName(pathBase) + Path.DirectorySeparatorChar + toolSTB_NombreBD.Text;
                Console.WriteLine(path);
                if (Directory.Exists(path))
                {
                    MessageBox.Show("Este nombre ya existe", "ERROR");
                }
                else
                {
                    try
                    {
                        Directory.SetCurrentDirectory(Path.GetDirectoryName(pathBase));
                        Directory.Move(pathBase, path);
                        Directory.Move(path + Path.DirectorySeparatorChar + Path.GetFileName(pathBase) + ".bd", path + Path.DirectorySeparatorChar + Path.GetFileName(path) + ".bd");
                        pathBase = path;
                        inicializaDirectorio(pathBase);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            else
            {
                if (pathBase == "")
                {
                    MessageBox.Show("Abre primero una base de datos", "ERROR");
                }
            }
        }

        #endregion

        #region menu
        private void archivoToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            archivoToolStripMenuItem.DropDown.Close();
            switch (e.ClickedItem.AccessibleName)
            {
                case "Abrir":
                    if (commonOFD.ShowDialog() == CommonFileDialogResult.Ok)
                    {
                        pathBase = commonOFD.FileName;
                        inicializaDirectorio(pathBase);
                        string arch = pathBase + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(pathBase) + ".bd";
                        if (File.Exists(arch))
                        {
                            try
                            {
                                using (StreamReader archivo = new StreamReader(arch))
                                {
                                    string j = archivo.ReadLine();
                                    tablas = JsonConvert.DeserializeObject<List<Tabla>>(j);
                                }
                            }
                            catch (Exception excep)
                            {
                                Console.WriteLine(excep);
                            }
                        }
                    }
                    break;
                case "Cerrar":
                    pathBase = "";
                    nombreTabla.Text = "";
                    tV_ListaTablas.Nodes.Clear();
                    dGV_AtributosTabla.Columns.Clear();
                    break;
                case "Eliminar":
                    eliminarBase();
                    break;
                case "Nueva":
                    saveFD1.ShowDialog();
                    break;
                case "Renombrar":

                    break;
            }
        }
        #endregion

        #region tabla

        private void tablasToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            tablasToolStripMenuItem.DropDown.Close();
            switch (e.ClickedItem.AccessibleName)
            {
                case "Nueva":
                    if (pathBase != "")
                    {
                        saveFDTabla.ShowDialog();
                    }
                    break;
                case "Renombrar":

                    break;
            }
        }

        private void renombrarTabla()
        {
            string path = "";

            if (nodo != null)
                if (tSTB_nombreTabla.Text != "" && tSTB_nombreTabla.Text != nodo.Text && pathBase != "")
                {
                    path = pathBase + Path.DirectorySeparatorChar + tSTB_nombreTabla.Text + ".t";
                    Console.WriteLine(path);
                    if (File.Exists(path))
                    {
                        MessageBox.Show("Este nombre ya existe", "ERROR");
                    }
                    else
                    {
                        try
                        {
                            Directory.Move(pathBase + Path.DirectorySeparatorChar + Path.GetFileName(nodo.Name), path);
                            listaTablasDirectorio();
                            for (int i = 0; i < tablas.Count; i++)
                            {
                                if (tablas[i].nombre == Path.GetFileNameWithoutExtension(nodo.Name))
                                {
                                    tablas[i].nombre = Path.GetFileNameWithoutExtension(path);
                                    break;
                                }
                            }

                            cargaTablaSeleccionada();
                            guardaTablas();

                            nodo = null;
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            MessageBox.Show(e.Message);
                        }
                    }
                }
                else
                {
                    if (pathBase == "")
                    {
                        MessageBox.Show("Abre primero una base de datos", "ERROR");
                    }
                }
        }

        private void guardaTablas()
        {
            string s = JsonConvert.SerializeObject(tablas);
            string arch = pathBase + Path.DirectorySeparatorChar + Path.GetFileName(pathBase) + ".bd";
            using (StreamWriter archivo = new StreamWriter(arch, false))
            {
                archivo.WriteLine(s);
            }
        }

        private void cargaTablaSeleccionada()
        {
            Tabla t = null;
            if (tablaSeleccionada != "")
                t = tablas.Find(x => x.nombre == Path.GetFileName(tablaSeleccionada));

            if (t != null)
            {
                dGV_AtributosTabla.Columns.Clear();
                var llave = "";

                t.atributos.ForEach(a =>
                {
                    if (t.PK != null && a.nombre == t.PK.nombre)
                    {
                        llave = " PK";
                    }

                    var fk = t.FK.Find(x => x.nombre == a.nombre);

                    if (fk != null)
                    {
                        llave = " FK";
                    }

                    dGV_AtributosTabla.Columns.Add(a.nombre, a.nombre + llave);
                });

                FKs.Clear();

                tablas.ForEach(tab =>
                {
                    if (tab.nombre != t.nombre)
                    {
                        FKs.Add(tab.PK);
                    }
                });

                cB_llavesForaneas.Items.Clear();

                FKs.ForEach(fk =>
                {
                    cB_llavesForaneas.Items.Add(fk.nombre);
                });
            }
        }

        private void tV_ListaTablas_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (Path.GetExtension(e.Node.Name) == ".t")
                    {
                        nombreTabla.Text = e.Node.Text;
                        tablaSeleccionada = e.Node.Name;
                        cargaTablaSeleccionada();
                    }
                    nodo = null;
                    break;
                case MouseButtons.Right:
                    nodo = e.Node;
                    tSTB_nombreTabla.Text = e.Node.Text;
                    break;
            }
        }

        private void tSTB_NuevaTabla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                creaTabla();
            }
        }

        private void creaTabla()
        {
            string path = "";
            if (tSTB_NuevaTabla.Text != "" && pathBase != "")
            {
                path = pathBase + Path.DirectorySeparatorChar + tSTB_NuevaTabla.Text + ".t";
                Console.WriteLine(path);
                if (Directory.Exists(path))
                {
                    MessageBox.Show("Este nombre ya existe", "ERROR");
                }
                else
                {
                    try
                    {
                        var f = File.Create(path);
                        f.Close();
                        var t = new Tabla();
                        t.nombre = Path.GetFileName(path);
                        tablas.Add(t);

                        cargaTablaSeleccionada();
                        guardaTablas();

                        listaTablasDirectorio();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
            else
            {
                if (pathBase == "")
                {
                    MessageBox.Show("Abre primero una base de datos", "ERROR");
                }
            }
        }


        private void saveFDTabla_FileOk(object sender, CancelEventArgs e)
        {
            File.CreateText(saveFDTabla.FileName);
            listaTablasDirectorio();
        }

        #endregion

        private void toolSTB_NombreBD_Validated(object sender, EventArgs e)
        {
            MessageBox.Show("validado");
        }

        private void toolSTB_NombreBD_Leave(object sender, EventArgs e)
        {
            //toolSTB_NombreBD.Text = "";
        }

        private string obtenNombreBD()
        {
            string s = "";

            if (pathBase != "")
                s = Path.GetFileName(pathBase);

            return s;
        }

        private void archivoToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            toolSTB_NombreBD.Text = obtenNombreBD();
        }

        private void toolSTB_NombreBD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                renombrarBase();
            }
        }

        private void tSTB_nombreTabla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                renombrarTabla();
            }
        }

        #region Atributos

        private void cB_llavePrimaria_Click(object sender, EventArgs e)
        {
            switch (cB_llavePrimaria.Checked)
            {
                case true:
                    cB_llaveForanea.Enabled = false;
                    break;
                case false:
                    cB_llaveForanea.Enabled = true;
                    break;
            }
        }

        private void cB_llaveForanea_Click(object sender, EventArgs e)
        {
            switch (cB_llaveForanea.Checked)
            {
                case true:
                    cB_llavePrimaria.Enabled = false;
                    cB_llavesForaneas.Visible = true;
                    break;
                case false:
                    cB_llavePrimaria.Enabled = true;
                    cB_llavesForaneas.Visible = false;
                    break;
            }
        }

        private void agregaAtributo()
        {
            int err = 0;

            if (tB_NombreAtributo.Text == "")
                err = 1;
            if (cB_tipoAtributo.Text == "" && cB_tipoAtributo.Text != "Entero" && cB_tipoAtributo.Text != "Decimal" && cB_tipoAtributo.Text != "Cadena")
                err = 2;
            if (cB_llavesForaneas.Text == "" && cB_llaveForanea.Checked == true)
                err = 3;

            switch (err)
            {
                case 0:
                    try
                    {
                        var atrib = new Atributo();
                        Atributo a = null;
                        atrib.nombre = tB_NombreAtributo.Text;
                        atrib.tipoDato = cB_tipoAtributo.Text;

                        if (atrib.tipoDato == "Cadena")
                        {
                            atrib.tam = int.Parse(tB_tamCadena.Text);
                        }

                        var t = tablas.Find(x => x.nombre == Path.GetFileName(tablaSeleccionada));

                        if (t != null)
                        {
                            a = t.atributos.Find(x => x.nombre == atrib.nombre);
                        }

                        if (a == null)
                        {
                            MessageBox.Show("Atributo agregado");
                            if (cB_llavePrimaria.Checked == true && t.PK == null)
                            {
                                t.PK = atrib;
                            }
                            else
                            {
                                if (cB_llavePrimaria.Checked == true)
                                {
                                    MessageBox.Show("Esta tabla ya contiene llave primaria");
                                }
                            }

                            if (cB_llaveForanea.Checked == true)
                            {
                                t.FK.Add(FKs[cB_llavesForaneas.SelectedIndex]);
                            }

                            t.atributos.Add(atrib);
                            cargaTablaSeleccionada();
                            guardaTablas();
                        }
                        else
                        {
                            MessageBox.Show("Nombre de atributo repetido");
                        }
                    }
                    catch (Exception excep)
                    {
                        MessageBox.Show(excep.Message);
                    }
                    break;
                case 1:
                    MessageBox.Show("Escribe un nombre");
                    break;
                case 2:
                    MessageBox.Show("Selecciona un tipo de atributo");
                    break;
                case 3:
                    MessageBox.Show("Selecciona una clave foranea");
                    break;

            }
        }

        private void btn_agregaArtibuto_Click(object sender, EventArgs e)
        {
            if (pathBase != "")
            {
                if (tablaSeleccionada != "")
                {
                    agregaAtributo();
                }
                else
                {
                    MessageBox.Show("Selecciona una tabla");
                }
            }
            else
            {
                MessageBox.Show("Abre una base de datos primero");
            }
        }

        #endregion

        private void cB_tipoAtributo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Console.WriteLine(cB_tipoAtributo.SelectedItem);

            if (cB_tipoAtributo.SelectedItem.ToString() == "Cadena")
            {
                tB_tamCadena.Text = "0";
                tB_tamCadena.Visible = true;
            }
            else
            {
                tB_tamCadena.Visible = false;
            }
        }

        private void cB_llavesForaneas_SelectedIndexChanged(object sender, EventArgs e)
        {
            cB_tipoAtributo.SelectedItem = FKs[cB_llavesForaneas.SelectedIndex].tipoDato;
        }
    }
}
