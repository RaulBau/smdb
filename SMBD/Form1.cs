﻿using System;
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
using System.Text.RegularExpressions;

namespace SMBD
{
    public partial class Form1 : Form
    {
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

        //Inicializa las variables del formulario
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

        //Crea una carpeta en la ruta seleccionada con el nombre que da el usuario
        private void saveFD1_FileOk(object sender, CancelEventArgs e)
        {
            pathBase = saveFD1.FileName;
            Directory.CreateDirectory(pathBase);
            var f = File.Create(pathBase + Path.DirectorySeparatorChar + Path.GetFileName(pathBase) + ".bd");
            f.Close();

            inicializaDirectorio(pathBase);
        }

        //Selecciona la ruta de la base de datos seleccionada
        private void openFD1_FileOk(object sender, CancelEventArgs e)
        {
            pathBase = commonOFD.FileName;
            Directory.SetCurrentDirectory(pathBase);
            Console.WriteLine(Directory.GetCurrentDirectory());
        }

        //Configura el directorio de los dialogos
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

        //Muestra las tablas del directorio seleccionado en un TreeView
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

        //Elimina la base de datos los archivos que tienen la terminacion .t y .bd por si se selecciona un directorio incorrecto
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

        //Renombra el archivo que contiene la base de datos y la carpeta que lo contiene
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
        //Controla las opciones del menu de la base de datos
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
                                    tablas.ForEach(t =>
                                    {
                                        t.atributos.ForEach(a =>
                                        {
                                            Atributo.actualizaId(a.id);
                                        });
                                    });
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
                    tablas.Clear();
                    tV_ListaTablas.Nodes.Clear();
                    dGV_AtributosTabla.Columns.Clear();
                    dGV_nuevoRegistro.Columns.Clear();
                    Atributo.reiniciaId();
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

        //Controla las opcciones del menu tablas
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

        //Renombra la tabla seleccionada
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

                            guardaTablas();
                            cargaTablaSeleccionada();

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

        //Guarda los registros de la tabla
        private void guardaTablas()
        {
            string s = JsonConvert.SerializeObject(tablas);
            string arch = pathBase + Path.DirectorySeparatorChar + Path.GetFileName(pathBase) + ".bd";
            using (StreamWriter archivo = new StreamWriter(arch, false))
            {
                archivo.WriteLine(s);
            }
        }

        //Carga los datos de la tabla seleccionada en el datagrid
        private void cargaTablaSeleccionada()
        {
            Tabla t = null;
            if (tablaSeleccionada != "")
                t = tablas.Find(x => x.nombre == Path.GetFileName(tablaSeleccionada));

            if (t != null)
            {
                dGV_AtributosTabla.Columns.Clear();
                dGV_nuevoRegistro.Columns.Clear();
                var llave = "";

                t.atributos.ForEach(a =>
                {
                    llave = "";
                    if (a.PK == true)
                    {
                        llave = " PK";
                    }
                    else
                    if (a.FK == true)
                    {
                        llave = " FK";
                    }
                    dGV_AtributosTabla.Columns.Add(a.nombre, a.nombre + llave);
                    dGV_AtributosTabla.Columns[dGV_AtributosTabla.Columns.Count - 1].SortMode = DataGridViewColumnSortMode.Programmatic;

                    dGV_nuevoRegistro.Columns.Add(a.nombre, a.nombre + llave);
                    dGV_nuevoRegistro.Columns[dGV_nuevoRegistro.Columns.Count - 1].SortMode = DataGridViewColumnSortMode.Programmatic;
                });

                string arch = tablaSeleccionada;
                string d;

                var lista = new List<Dictionary<string, object>>();
                using (StreamReader archivo = new StreamReader(arch))
                {
                    d = archivo.ReadLine();
                }
                if (d != null && d != "")
                {
                    lista = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(d);
                }

                dGV_AtributosTabla.Rows.Clear();
                int n;
                foreach (var item in lista)
                {
                    n = dGV_AtributosTabla.Rows.Add();
                    for (int i = 0; i < t.atributos.Count; i++)
                    {
                        dGV_AtributosTabla.Rows[n].Cells[i].Value = item[t.atributos[i].nombre];
                    }
                }

                FKs.Clear();

                tablas.ForEach(tab =>
                {
                    if (tab.nombre != t.nombre)
                    {
                        tab.atributos.ForEach(atr =>
                        {
                            if (atr.PK)
                                FKs.Add(atr);
                        });
                    }
                });

                cB_llavesForaneas.Items.Clear();

                FKs.ForEach(fk =>
                {
                    if (fk != null && fk.nombre != null)
                        cB_llavesForaneas.Items.Add(fk.nombre);
                });
            }

            dGV_AtributosTabla.ClearSelection();
            dGV_nuevoRegistro.Rows.Clear();
        }

        //Selecciona la tabla al dar click en el Treeview
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

        //Revisa la tecla presionada para crear una tabla
        private void tSTB_NuevaTabla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                creaTabla();
            }
        }

        //Crea una tabla con el nombre escrito en el control
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

                        guardaTablas();
                        cargaTablaSeleccionada();

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

        //Crea un archivo de texto con el nombre escrito en el control
        private void saveFDTabla_FileOk(object sender, CancelEventArgs e)
        {
            File.CreateText(saveFDTabla.FileName);
            listaTablasDirectorio();
        }

        #endregion

        //Funcion de pruebas
        private void toolSTB_NombreBD_Validated(object sender, EventArgs e)
        {
            MessageBox.Show("validado");
        }

        //Funcion de pruebas
        private void toolSTB_NombreBD_Leave(object sender, EventArgs e)
        {
            //toolSTB_NombreBD.Text = "";
        }

        //Obtiene el nombre de la base de datos
        private string obtenNombreBD()
        {
            string s = "";

            if (pathBase != "")
                s = Path.GetFileName(pathBase);

            return s;
        }

        //Muestra el nombre de la base de datos en el control
        private void archivoToolStripMenuItem_DropDownClosed(object sender, EventArgs e)
        {
            toolSTB_NombreBD.Text = obtenNombreBD();
        }

        //Verifica la letra presionada y si es la correcta llama la funcion para renombrar la base de datos
        private void toolSTB_NombreBD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                renombrarBase();
            }
        }

        //Verifica la letra presionada y si es la correcta llama la funcion para renombrar la tabla
        private void tSTB_nombreTabla_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                renombrarTabla();
            }
        }

        #region Atributos
        //Desactiva el control de llave foranea si se activa el de llave primaria
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

        //Desactiva el control de llave primaria si se activa el de llave foranea
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

        //Funcion para agregar un atributo con los datos seleccionados
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

                        switch (atrib.tipoDato)
                        {
                            case "Entero":
                                atrib.tam = 4;
                                break;
                            case "Cadena":
                                atrib.tam = int.Parse(tB_tamCadena.Text);
                                break;
                            case "Decimal":
                                atrib.tam = 8;
                                break;
                        }

                        var t = tablas.Find(x => x.nombre == Path.GetFileName(tablaSeleccionada));

                        if (t != null)
                        {
                            a = t.atributos.Find(x => x.nombre == atrib.nombre);
                        }

                        if (a == null)
                        {
                            MessageBox.Show("Atributo agregado");
                            if (cB_llavePrimaria.Checked == true && t.clavePrimaria() == false)
                            {
                                atrib.PK = true;
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
                                var at = FKs[cB_llavesForaneas.SelectedIndex];
                                atrib.tipoDato = at.tipoDato;
                                atrib.tam = at.tam;
                                atrib.nombre = at.nombre;
                                atrib.agregaLlaveForanea(at.id);
                            }

                            t.atributos.Add(atrib);
                            tB_NombreAtributo.Text = "";
                            cB_tipoAtributo.Text = "";
                            cB_llavesForaneas.SelectedItem = "";
                            guardaTablas();
                            cargaTablaSeleccionada();
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

        //Funcion que se activa cuando se presiona el boton para gregar atributos
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

        //Muestra el control para elegir el tamaño de la cadena
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

        //Carga los datos de la llave foranea en los controles
        private void cB_llavesForaneas_SelectedIndexChanged(object sender, EventArgs e)
        {
            cB_tipoAtributo.SelectedItem = FKs[cB_llavesForaneas.SelectedIndex].tipoDato;
            if (FKs[cB_llavesForaneas.SelectedIndex].tipoDato == "cadena")
            {
                tB_tamCadena.Text = FKs[cB_llavesForaneas.SelectedIndex].tam.ToString();
            }
            tB_NombreAtributo.Text = FKs[cB_llavesForaneas.SelectedIndex].nombre;
        }

        //Cambia el nombre del atributo seleccionado
        private void renombraAtributo(Tabla t, string vN, string nN, int id, int numAtrib)
        {
            string arch = "";
            string d;

            dGV_AtributosTabla.Columns[numAtrib].Name = nN;

            var lista = new List<Dictionary<string, object>>();
            if (id == -1)
            {
                arch = this.pathBase + Path.DirectorySeparatorChar + t.nombre;
                using (StreamReader archivo = new StreamReader(arch))
                {
                    d = archivo.ReadLine();
                }
                if (d != null && d != "")
                {
                    lista = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(d);
                }
                lista = cambiaNombreAtributoRegistro(t, nN, numAtrib, lista, false);
                guardaTabla(arch, lista);
                t.atributos[numAtrib].nombre = nN;
                guardaTablas();
                cargaTablaSeleccionada();
            }
            else
            {
                for (int i = 0; i < tablas.Count; i++)
                {
                    arch = this.pathBase + Path.DirectorySeparatorChar + tablas[i].nombre;

                    using (StreamReader archivo = new StreamReader(arch))
                    {
                        d = archivo.ReadLine();
                    }
                    if (d != null && d != "")
                    {
                        lista = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(d);
                    }

                    lista = cambiaNombreAtributoRegistro(tablas[i], nN, id, lista, true);
                    guardaTabla(arch, lista);

                }
                for (int i = 0; i < tablas.Count; i++)
                {
                    for (int j = 0; j < tablas[i].atributos.Count; j++)
                    {
                        var atrib = tablas[i].atributos[j];
                        if (atrib.id == id || atrib.ref_id == id)
                        {
                            atrib.nombre = nN;
                        }
                    }
                }
            }
            t.atributos[numAtrib].nombre = nN;
            guardaTablas();
            cargaTablaSeleccionada();
        }

        //Cambia el nombre de un atributo
        private List<Dictionary<string, object>> cambiaNombreAtributoRegistro(Tabla t, string nN, int id, List<Dictionary<string, object>> lista, bool cascada)
        {
            Atributo atr = null;
            object o;
            do
            {

                atr = cascada == true ? t.atributos.Find(a => a.id == id || a.ref_id == id) : t.atributos[id];
                if (atr != null)
                {
                    for (int j = 0; j < lista.Count; j++)
                    {
                        lista[j].Add(nN, lista[j][atr.nombre]);
                        lista[j].Remove(atr.nombre);
                    }
                }
            } while (atr != null && lista.Find(a => a.TryGetValue(atr.nombre, out o)) != null);

            return lista;
        }

        //Guarda los datos de la tabla en el archivo
        private void guardaTabla(string tabla, List<Dictionary<string, object>> lista)
        {
            string s = JsonConvert.SerializeObject(lista);
            using (StreamWriter archivo = new StreamWriter(tabla, false))
            {
                archivo.WriteLine(s);
            }
        }

        //Accion del boton de modiciar articulo
        private void btn_modoficarAtributo_Click(object sender, EventArgs e)
        {
            try
            {
                int i = -1;
                if (dGV_AtributosTabla.CurrentCell != null)
                    i = dGV_AtributosTabla.CurrentCell.ColumnIndex;
                var t = tablas.Find(x => x.nombre == Path.GetFileName(tablaSeleccionada));

                if (t != null)
                {
                    var atrib = t.atributos[i];
                    if (tB_NombreAtributo.Text != atrib.nombre)
                    {
                        if (tB_NombreAtributo.Text == "")
                            throw new Exception("Escribe un nombre");

                        renombraAtributo(t, atrib.nombre, tB_NombreAtributo.Text, atrib.PK == true ? atrib.id : atrib.ref_id, i);
                    }
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
        }

        //Carga los datos del atributo en los controles cuando se da click en una celda del control
        private void dGV_AtributosTabla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            switch (cB_renombrarAtributo.Checked)
            {
                case true:
                    try
                    {
                        int i = -1;
                        if (dGV_AtributosTabla.CurrentCell != null)
                            i = dGV_AtributosTabla.CurrentCell.ColumnIndex;
                        var t = tablas.Find(x => x.nombre == Path.GetFileName(tablaSeleccionada));
                        if (t != null)
                        {
                            var atrib = t.atributos[i];
                            tB_NombreAtributo.Text = atrib.nombre;
                            cB_tipoAtributo.Text = atrib.tipoDato;
                            cB_llavesForaneas.SelectedIndex = 0;
                            tB_tamCadena.Text = atrib.tam.ToString();
                            cB_llavePrimaria.Checked = atrib.PK;
                            cB_llaveForanea.Checked = atrib.FK;
                            cB_llavesForaneas.Visible = atrib.FK;
                        }
                    }
                    catch (Exception excep)
                    {
                        MessageBox.Show(excep.Message);
                    }
                    break;
                case false:
                    break;
            }
        }

        //Limpia los datos de los controles cuando se desmarca el control
        private void cB_renombrarAtributo_Click(object sender, EventArgs e)
        {
            switch (cB_renombrarAtributo.Checked)
            {
                case true:

                    break;
                case false:
                    tB_NombreAtributo.Text = "";
                    cB_tipoAtributo.Text = "";
                    cB_llavesForaneas.SelectedItem = "";
                    tB_tamCadena.Text = "";
                    tB_tamCadena.Visible = false;
                    cB_llavePrimaria.Checked = false;
                    cB_llaveForanea.Checked = false;
                    cB_llavesForaneas.Visible = false;
                    break;
            }
        }

        //Activa el menu de contexto de los atributos
        private void dGV_AtributosTabla_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    break;
                case MouseButtons.Right:
                    cMS_atributos.Enabled = true;
                    break;
            }
        }
        #endregion

        //Desactiva el menu de cotnexto de los atributos
        private void cMS_atributos_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            cMS_atributos.Enabled = false;
        }

        //Funcion de pruebas
        private void dGV_nuevoRegistro_RowValidated(object sender, DataGridViewCellEventArgs e)
        {

        }

        //Agrega un registro en la tabla seleccionada
        private void guardaDatosTabla(Dictionary<string, object> datos)
        {
            string arch = tablaSeleccionada;
            string d;

            var lista = new List<Dictionary<string, object>>();
            using (StreamReader archivo = new StreamReader(arch))
            {
                d = archivo.ReadLine();
            }
            if (d != null && d != "")
            {
                lista = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(d);
            }

            lista.Add(datos);

            string s = JsonConvert.SerializeObject(lista);
            using (StreamWriter archivo = new StreamWriter(arch, false))
            {
                archivo.WriteLine(s);
            }
        }

        //Funcion para agregar registros
        private void cargaRegistro()
        {
            var t = tablas.Find(x => x.nombre == Path.GetFileName(tablaSeleccionada));
            var renglon = new Dictionary<string, object>();
            var corr = true;
            var datos = true;
            var debil = true;

            try
            {
                if (t != null)
                {
                    if (dGV_AtributosTabla.SelectedRows.Count == 1)
                    {
                        for (int i = 0; i < t.atributos.Count; i++)
                        {
                            switch (t.atributos[i].tipoDato)
                            {
                                case "Entero":
                                    if (!Regex.IsMatch(dGV_nuevoRegistro.Rows[0].Cells[i].Value.ToString(), "^[0-9][0-9]*$"))
                                    {
                                        throw new Exception("El tipo de dato de " + t.atributos[i].nombre + " no es entero");
                                    }
                                    renglon.Add(t.atributos[i].nombre, dGV_nuevoRegistro.Rows[0].Cells[i].Value);
                                    break;
                                case "Decimal":
                                    if (!Regex.IsMatch(dGV_nuevoRegistro.Rows[0].Cells[i].Value.ToString(), "^-?[0-9]+(?:.[0-9]+)?$"))
                                    {
                                        throw new Exception("El tipo de dato de " + t.atributos[i].nombre + " no es decimal");
                                    }
                                    renglon.Add(t.atributos[i].nombre, dGV_nuevoRegistro.Rows[0].Cells[i].Value);
                                    break;
                                case "Cadena":
                                    renglon.Add(t.atributos[i].nombre, dGV_nuevoRegistro.Rows[0].Cells[i].Value.ToString().PadRight(t.atributos[i].tam));
                                    break;
                            }

                            if (t.atributos[i].FK == true)
                                datos = datos & datosFK(t.atributos[i].ref_id);

                            //revisamos si existe una llave fforanea
                            if (t.atributos[i].FK == true && existePK(obtenTablaAtributo(t.atributos[i].ref_id), dGV_nuevoRegistro.Rows[0].Cells[i].Value.ToString(), t.atributos[i].nombre) == false)
                            {
                                throw new Exception("La llave foranea no existe");
                            }

                            //Revisamos si la llave primaria esta repetida
                            if (t.atributos[i].PK == true && existePK(t.nombre, dGV_nuevoRegistro.Rows[0].Cells[i].Value.ToString(), t.atributos[i].nombre) == true)
                            {
                                throw new Exception("Llave primaria repetida");
                            }
                        }

                        if (datos == true)
                        {
                            actualizaRegistro(renglon, dGV_AtributosTabla.Rows.IndexOf(dGV_AtributosTabla.SelectedRows[0]));
                        }
                    }
                    else
                    if (dGV_nuevoRegistro.Rows[0] != null)
                    {
                        for (int i = 0; i < dGV_nuevoRegistro.Rows[0].Cells.Count; i++)
                        {
                            if (dGV_nuevoRegistro.Rows[0].Cells[i].Value == null)
                            {
                                corr = false;
                            }
                            corr = corr && dGV_nuevoRegistro.Rows[0].Cells[i].Value.ToString() != "";
                        }
                        if (corr == true)
                        {
                            for (int i = 0; i < t.atributos.Count; i++)
                            {
                                debil = debil && t.atributos[i].FK;
                                if (t.atributos[i].FK == true)
                                    datos = datos & datosFK(t.atributos[i].ref_id);
                            }

                            if (datos == true)
                            {


                                for (int i = 0; i < t.atributos.Count; i++)
                                {
                                    //revisamos si existe una llave fforanea
                                    if (t.atributos[i].FK == true && existePK(obtenTablaAtributo(t.atributos[i].ref_id), dGV_nuevoRegistro.Rows[0].Cells[i].Value.ToString(), t.atributos[i].nombre) == false)
                                    {
                                        throw new Exception("La llave foranea no existe");
                                    }

                                    //Revisamos si la llave primaria esta repetida
                                    if (t.atributos[i].PK == true && existePK(t.nombre, dGV_nuevoRegistro.Rows[0].Cells[i].Value.ToString(), t.atributos[i].nombre) == true)
                                    {
                                        throw new Exception("Llave primaria repetida");
                                    }

                                    switch (t.atributos[i].tipoDato)
                                    {
                                        case "Entero":
                                            if (!Regex.IsMatch(dGV_nuevoRegistro.Rows[0].Cells[i].Value.ToString(), "^[0-9][0-9]*$"))
                                            {
                                                throw new Exception("El tipo de dato de " + t.atributos[i].nombre + " no es entero");
                                            }
                                            renglon.Add(t.atributos[i].nombre, dGV_nuevoRegistro.Rows[0].Cells[i].Value);
                                            break;
                                        case "Decimal":
                                            if (!Regex.IsMatch(dGV_nuevoRegistro.Rows[0].Cells[i].Value.ToString(), "^-?[0-9]+(?:.[0-9]+)?$"))
                                            {
                                                throw new Exception("El tipo de dato de " + t.atributos[i].nombre + " no es decimal");
                                            }
                                            renglon.Add(t.atributos[i].nombre, dGV_nuevoRegistro.Rows[0].Cells[i].Value);
                                            break;
                                        case "Cadena":
                                            renglon.Add(t.atributos[i].nombre, dGV_nuevoRegistro.Rows[0].Cells[i].Value.ToString().PadRight(t.atributos[i].tam));
                                            break;
                                    }
                                    t.atributos[i].datos = true;
                                }

                                //Se verifica si es una relacion debil
                                if (debil == true)
                                {
                                    var iguales = true;

                                    for (int j = 0; j < dGV_AtributosTabla.Rows.Count - 1; j++)
                                    {
                                        iguales = true;
                                        //Verificamos si los datos están repetidos
                                        for (int i = 0; i < t.atributos.Count; i++)
                                        {
                                            iguales = iguales && renglon[t.atributos[i].nombre].ToString() == dGV_AtributosTabla.Rows[j].Cells[i].Value.ToString();
                                        }

                                        //Si hay datos repetidos lanza la excepcion
                                        if (iguales == true && t.atributos.Count > 0) throw new Exception("Registro duplicado");
                                    }

                                }
                            }
                            else
                            {
                                throw new Exception("La llave primaria no contiene datos");
                            }
                            if (datos == true)
                            {
                                guardaDatosTabla(renglon);
                                guardaTablas();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Llena todos los campos");
                        }
                    }
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
        }

        //Funcion de pruebas
        private void actualizaPK(int id, object pk)
        {

        }

        //Funcion para modificar los datos de un registro
        private void actualizaRegistro(Dictionary<string, object> reg, int numReg)
        {
            var t = tablas.Find(x => x.nombre == Path.GetFileName(tablaSeleccionada));

            var lista = new List<Dictionary<string, object>>();
            string d = "";
            if (t != null)
            {

                try
                {
                    var arch = this.tablaSeleccionada;
                    using (StreamReader archivo = new StreamReader(arch))
                    {
                        d = archivo.ReadLine();
                    }

                    if (d != null && d != "")
                    {
                        lista = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(d);
                    }

                    if (lista[numReg] != null)
                    {
                        lista[numReg] = reg;

                        string s = JsonConvert.SerializeObject(lista);
                        using (StreamWriter archivo = new StreamWriter(arch, false))
                        {
                            archivo.WriteLine(s);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //Funcion para modificar los datos de un registro
        private void modificaRegistro()
        {
            var t = tablas.Find(x => x.nombre == Path.GetFileName(tablaSeleccionada));

            for (int i = 0; i < t.atributos.Count; i++)
            {
                //revisamos si existe una llave fforanea
                if (t.atributos[i].FK == true && existePK(obtenTablaAtributo(t.atributos[i].ref_id), dGV_nuevoRegistro.Rows[0].Cells[i].Value.ToString(), t.atributos[i].nombre) == false)
                {
                    throw new Exception("La llave foranea no existe");
                }

                //Revisamos si la llave primaria esta repetida
                if (t.atributos[i].PK == true && existePK(t.nombre, dGV_nuevoRegistro.Rows[0].Cells[i].Value.ToString(), t.atributos[i].nombre) == true)
                {
                    throw new Exception("Llave primaria repetida");
                }
            }
        }

        //Funcipn para saber si hay datos en la llave foranea
        private bool datosFK(int id)
        {
            var b = false;

            if (id != -1)
            {
                b = tablas.Find(t => t.atributos.Find(a => a.id == id && a.datos == true && a.PK == true) != null) != null;
            }

            return b;
        }

        //Funcion para obtener la tabla de un atributo usando el id del atributo
        private string obtenTablaAtributo(int id)
        {
            string s = "";

            var tab = tablas.Find(t => t.atributos.Find(a => a.id == id && a.datos == true && a.PK == true) != null);

            s = tab != null ? tab.nombre : "";

            return s;
        }

        //Fucnion para revisar si la llave primaria esta repetida
        private bool existePK(string tabla, string pk, string nomAtrib)
        {
            var b = false;

            var lista = new List<Dictionary<string, object>>();
            string d = "";

            try
            {
                var arch = this.pathBase + Path.DirectorySeparatorChar + tabla;
                using (StreamReader archivo = new StreamReader(arch))
                {
                    d = archivo.ReadLine();
                }

                if (d != null && d != "")
                {
                    lista = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(d);
                }

                foreach (var item in lista)
                {
                    if (item[nomAtrib].ToString() == pk)
                    {
                        b = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return b;
        }

        //Funcion de pruebas
        private void dGV_nuevoRegistro_RowLeave(object sender, DataGridViewCellEventArgs e)
        {

        }

        //Funcin que llama a la funcion de rear registro cuando se presiona la telca enter
        private void dGV_nuevoRegistro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r' || e.KeyChar == '\n')
            {
                cargaRegistro();
                cargaTablaSeleccionada();
            }
        }

        //Revisa si hay datos en ese atributo
        private bool contieneDatos(string tabla, string nomAtributo)
        {
            var b = false;
            var lista = new List<Dictionary<string, object>>();
            string d = "";

            try
            {
                var arch = this.pathBase + Path.DirectorySeparatorChar + tabla;
                using (StreamReader archivo = new StreamReader(arch))
                {
                    d = archivo.ReadLine();
                }

                if (d != null && d != "")
                {
                    lista = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(d);
                }

                object dato;
                foreach (var item in lista)
                {
                    b = item.TryGetValue(nomAtributo, out dato);
                    if (b == true)
                    {

                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return b;
        }

        //Funcion para eliminar atributos
        private void btn_EliminarAtributo_Click(object sender, EventArgs e)
        {
            var ind = dGV_AtributosTabla.CurrentCell.ColumnIndex;

            try
            {
                var vacio = !contieneDatos(Path.GetFileName(tablaSeleccionada), dGV_AtributosTabla.Columns[ind].Name);

                if (vacio == false)
                    throw new Exception("Este atributo contiene datos");

                var tab = tablas.Find(t => t.atributos.Find(atr => atr.nombre == dGV_AtributosTabla.Columns[ind].Name) != null && t.nombre == Path.GetFileName(tablaSeleccionada));
                if (tab != null)
                {
                    var atr = tab.atributos.Find(a => a.nombre == dGV_AtributosTabla.Columns[ind].Name);

                    if (atr.PK == true)
                    {
                        for (int i = 0; i < tablas.Count; i++)
                        {
                            tablas[i].atributos = tablas[i].atributos.FindAll(a => a.id != atr.id && a.ref_id != atr.id);
                        }
                        guardaTablas();
                    }
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
        }

        //Funcion para eliminar un registro
        private void btn_eliminarRegistro_Click(object sender, EventArgs e)
        {

            var rIndex = -1;
            var cIndex = -1;

            if (dGV_AtributosTabla.CurrentCell != null)
            {
                rIndex = dGV_AtributosTabla.CurrentCell.RowIndex;
                cIndex = dGV_AtributosTabla.CurrentCell.ColumnIndex;
            }
            eliminaRegistro(rIndex, cIndex, tablaSeleccionada);
        }

        //Elimina el registro seleccionado
        private void eliminaRegistro(int rIndex, int cIndex, string tabSel)
        {
            if (cIndex != -1 && rIndex != -1)
            {
                try
                {
                    var t = tablas.Find(x => x.nombre == Path.GetFileName(tabSel));
                    if (t != null)
                    {
                        var pk = false;
                        var fk = false;
                        var allFK = true;
                        var pkId = -1;
                        var pkIndex = -1;

                        var j = -1;
                        t.atributos.ForEach(a =>
                        {
                            j++;
                            allFK = allFK && a.FK;
                            if (a.PK == true) { pk = true; pkId = a.id; pkIndex = j; }
                            if (a.FK == true) { fk = true; }
                        });

                        if (pk == false && fk == false)
                        {
                            eliminaRegistros(rIndex, t.nombre);
                        }
                        else
                        {
                            if (allFK == true)
                            {
                                eliminaRegistros(rIndex, t.nombre);
                            }
                            else
                            {
                                if (pk == true)
                                    eliminaRegistrosCascada(t.atributos[pkIndex], dGV_AtributosTabla.Rows[rIndex].Cells[pkIndex].Value.ToString(), t.nombre);
                            }
                        }
                        cargaTablaSeleccionada();
                    }
                }
                catch (Exception excep)
                {
                    MessageBox.Show(excep.Message);
                }
            }
        }

        //Elimina los registros de forma recursiva
        private void eliminaRegistrosCascada(Atributo atrib, string dato, string tabSel)
        {
            var t = tablas.Find(x => x.nombre == Path.GetFileName(tabSel));
            var ts = tablas.FindAll(x => x.nombre != Path.GetFileName(tabSel) && x.atributos.Find(a => a.ref_id == atrib.id) != null);
            var lista = new List<Dictionary<string, object>>();
            string d = "";

            if (t != null)
            {
                try
                {
                    var arch = this.pathBase + Path.DirectorySeparatorChar + tabSel;
                    using (StreamReader archivo = new StreamReader(arch))
                    {
                        d = archivo.ReadLine();
                    }

                    if (d != null && d != "")
                    {
                        lista = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(d);
                    }

                    if (lista.Count > 0)
                    {
                        object o;
                        var ind = lista.FindIndex(li => li.TryGetValue(atrib.nombre, out o));

                        if (ind != -1)
                            eliminaRegistros(ind, t.nombre);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            if (ts != null)
            {
                for (int i = 0; i < ts.Count; i++)
                {
                    var atribs = ts[i].atributos;

                    try
                    {
                        var arch = this.pathBase + Path.DirectorySeparatorChar + ts[i].nombre;
                        using (StreamReader archivo = new StreamReader(arch))
                        {
                            d = archivo.ReadLine();
                        }

                        if (d != null && d != "")
                        {
                            lista = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(d);
                        }

                        if (lista.Count > 0)
                        {
                            object o;
                            var index = -1;
                            var indexPK = -1;

                            index = lista.FindIndex(l => l.TryGetValue(atrib.nombre, out o) == true);
                            indexPK = atribs.FindIndex(a => a.PK == true);
                            if (indexPK != -1)
                                eliminaRegistrosCascada(atribs[indexPK], lista[0][atribs[indexPK].nombre].ToString(), ts[i].nombre);

                            lista.RemoveAll(l => l.TryGetValue(atrib.nombre, out o) == true && l[atrib.nombre].ToString() == dato);

                            guardaTabla(arch, lista);
                            if (lista.Count == 0)
                            {
                                for (int j = 0; j < atribs.Count; j++)
                                {
                                    atribs[j].datos = false;
                                }
                                guardaTablas();
                            }
                        }
                    }
                    catch (Exception exc)
                    {
                        MessageBox.Show(exc.Message);
                    }
                }
            }
        }

        //Funcion para eliminar el registro seleccionado
        private void eliminaRegistros(int i, string tabla)
        {
            var lista = new List<Dictionary<string, object>>();
            string d = "";
            try
            {
                var arch = this.pathBase + Path.DirectorySeparatorChar + tabla;
                using (StreamReader archivo = new StreamReader(arch))
                {
                    d = archivo.ReadLine();
                }

                if (d != null && d != "")
                {
                    lista = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(d);
                }

                if (lista.Count > 0)
                {
                    lista.RemoveAt(i);
                    guardaTabla(arch, lista);
                    if (lista.Count == 0)
                    {
                        var t = tablas.Find(tab => tab.nombre == tabla);
                        if (t != null)
                        {
                            for (int j = 0; j < t.atributos.Count; j++)
                            {
                                t.atributos[j].datos = false;
                            }
                            guardaTablas();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region Consultas
        //Funcion para abrir el formulario para la consultas
        private void consultasToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.AccessibleName)
            {
                case "Nueva":
                    var fSQL = new fSQL(this.pathBase, tablas);
                    fSQL.Show();
                    break;
            }
        }
        #endregion
    }
}
