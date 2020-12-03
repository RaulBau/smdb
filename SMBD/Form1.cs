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
            Console.WriteLine(Directory.GetCurrentDirectory());
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
                        Console.WriteLine(Directory.GetCurrentDirectory());
                    }
                    break;
                case "Cerrar":
                    pathBase = "";
                    break;
                case "Nueva":
                    saveFD1.ShowDialog();
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


        private void saveFDTabla_FileOk(object sender, CancelEventArgs e)
        {
            File.CreateText(saveFDTabla.FileName);
        }
        #endregion
    }
}
