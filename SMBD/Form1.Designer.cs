
namespace SMBD
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.archivoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.abrirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renombrarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolSTB_NombreBD = new System.Windows.Forms.ToolStripTextBox();
            this.tablasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevaToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFD1 = new System.Windows.Forms.SaveFileDialog();
            this.openFD1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFDTabla = new System.Windows.Forms.SaveFileDialog();
            this.listaTablas = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.archivoToolStripMenuItem,
            this.tablasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // archivoToolStripMenuItem
            // 
            this.archivoToolStripMenuItem.AccessibleName = "Archivo";
            this.archivoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.abrirToolStripMenuItem,
            this.cerrarToolStripMenuItem,
            this.nuevaToolStripMenuItem,
            this.renombrarToolStripMenuItem});
            this.archivoToolStripMenuItem.Name = "archivoToolStripMenuItem";
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.archivoToolStripMenuItem.Text = "Archivo";
            this.archivoToolStripMenuItem.DropDownClosed += new System.EventHandler(this.archivoToolStripMenuItem_DropDownClosed);
            this.archivoToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.archivoToolStripMenuItem_DropDownItemClicked);
            // 
            // abrirToolStripMenuItem
            // 
            this.abrirToolStripMenuItem.AccessibleName = "Abrir";
            this.abrirToolStripMenuItem.Name = "abrirToolStripMenuItem";
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            // 
            // cerrarToolStripMenuItem
            // 
            this.cerrarToolStripMenuItem.AccessibleName = "Cerrar";
            this.cerrarToolStripMenuItem.Name = "cerrarToolStripMenuItem";
            this.cerrarToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.cerrarToolStripMenuItem.Text = "Cerrar";
            // 
            // nuevaToolStripMenuItem
            // 
            this.nuevaToolStripMenuItem.AccessibleName = "Nueva";
            this.nuevaToolStripMenuItem.Name = "nuevaToolStripMenuItem";
            this.nuevaToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.nuevaToolStripMenuItem.Text = "Nueva";
            // 
            // renombrarToolStripMenuItem
            // 
            this.renombrarToolStripMenuItem.AccessibleName = "Renombrar";
            this.renombrarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSTB_NombreBD});
            this.renombrarToolStripMenuItem.Name = "renombrarToolStripMenuItem";
            this.renombrarToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.renombrarToolStripMenuItem.Text = "Renombrar";
            // 
            // toolSTB_NombreBD
            // 
            this.toolSTB_NombreBD.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolSTB_NombreBD.Name = "toolSTB_NombreBD";
            this.toolSTB_NombreBD.Size = new System.Drawing.Size(100, 23);
            this.toolSTB_NombreBD.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolSTB_NombreBD_KeyPress);
            // 
            // tablasToolStripMenuItem
            // 
            this.tablasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevaToolStripMenuItem1});
            this.tablasToolStripMenuItem.Name = "tablasToolStripMenuItem";
            this.tablasToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.tablasToolStripMenuItem.Text = "Tabla";
            this.tablasToolStripMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.tablasToolStripMenuItem_DropDownItemClicked);
            // 
            // nuevaToolStripMenuItem1
            // 
            this.nuevaToolStripMenuItem1.AccessibleName = "Nueva";
            this.nuevaToolStripMenuItem1.Name = "nuevaToolStripMenuItem1";
            this.nuevaToolStripMenuItem1.Size = new System.Drawing.Size(108, 22);
            this.nuevaToolStripMenuItem1.Text = "Nueva";
            // 
            // saveFD1
            // 
            this.saveFD1.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFD1_FileOk);
            // 
            // openFD1
            // 
            this.openFD1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFD1_FileOk);
            // 
            // saveFDTabla
            // 
            this.saveFDTabla.DefaultExt = "t";
            this.saveFDTabla.Filter = "Tabla|*.t|Todos los archivos|*.*";
            this.saveFDTabla.FileOk += new System.ComponentModel.CancelEventHandler(this.saveFDTabla_FileOk);
            // 
            // listaTablas
            // 
            this.listaTablas.FormattingEnabled = true;
            this.listaTablas.Location = new System.Drawing.Point(12, 27);
            this.listaTablas.Name = "listaTablas";
            this.listaTablas.Size = new System.Drawing.Size(120, 212);
            this.listaTablas.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.listaTablas);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem archivoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem abrirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevaToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog saveFD1;
        private System.Windows.Forms.OpenFileDialog openFD1;
        private System.Windows.Forms.ToolStripMenuItem tablasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevaToolStripMenuItem1;
        private System.Windows.Forms.SaveFileDialog saveFDTabla;
        private System.Windows.Forms.ToolStripMenuItem renombrarToolStripMenuItem;
        private System.Windows.Forms.ToolStripTextBox toolSTB_NombreBD;
        private System.Windows.Forms.ListBox listaTablas;
    }
}

