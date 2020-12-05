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

namespace SMBD
{
    public partial class Form1 : Form
    {

        string pathBase;
        private FolderBrowserDialog folderBD;
        CommonOpenFileDialog commonOFD;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pathBase = "";
            folderBD = new FolderBrowserDialog();
            folderBD.ShowNewFolderButton = false;
            commonOFD = new CommonOpenFileDialog();
            commonOFD.IsFolderPicker = true;
            inicializaDirectorio(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        }

        #region Archivo

        private void saveFD1_FileOk(object sender, CancelEventArgs e)
        {
            pathBase = saveFD1.FileName;
            Directory.CreateDirectory(pathBase);
            //Directory.SetCurrentDirectory(pathBase);
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
                listaTablas.Items.Clear();
                listView1.Items.Clear();
                elementos.ToList().ForEach(a =>
                {
                    if (Path.GetExtension(a) == ".t")
                    {
                        Console.WriteLine(a);
                        listaTablas.Items.Add(Path.GetFileNameWithoutExtension(a));
                        listView1.Items.Add(Path.GetFileNameWithoutExtension(a));
                        var nodo = new TreeNode();
                        nodo.Text = Path.GetFileNameWithoutExtension(a);
                        nodo.Name = Path.GetFileNameWithoutExtension(a);

                        t.Nodes.Add(nodo);
                    }
                }
                );
                tV_ListaTablas.Nodes.Add(t);
                tV_ListaTablas.ExpandAll();
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
                    }
                    break;
                case "Cerrar":
                    pathBase = "";
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
                        File.Create(path);
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
    }
}
