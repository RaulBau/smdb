
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
            this.components = new System.ComponentModel.Container();
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
            this.cMS_ListaTablas = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nuevaToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tSTB_NuevaTabla = new System.Windows.Forms.ToolStripTextBox();
            this.tV_ListaTablas = new System.Windows.Forms.TreeView();
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nombreTabla = new System.Windows.Forms.Label();
            this.dGV_AtributosTabla = new System.Windows.Forms.DataGridView();
            this.renombrarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tSTB_nombreTabla = new System.Windows.Forms.ToolStripTextBox();
            this.menuStrip1.SuspendLayout();
            this.cMS_ListaTablas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGV_AtributosTabla)).BeginInit();
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
            this.eliminarToolStripMenuItem,
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
            this.abrirToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.abrirToolStripMenuItem.Text = "Abrir";
            // 
            // cerrarToolStripMenuItem
            // 
            this.cerrarToolStripMenuItem.AccessibleName = "Cerrar";
            this.cerrarToolStripMenuItem.Name = "cerrarToolStripMenuItem";
            this.cerrarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.cerrarToolStripMenuItem.Text = "Cerrar";
            // 
            // nuevaToolStripMenuItem
            // 
            this.nuevaToolStripMenuItem.AccessibleName = "Nueva";
            this.nuevaToolStripMenuItem.Name = "nuevaToolStripMenuItem";
            this.nuevaToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.nuevaToolStripMenuItem.Text = "Nueva";
            // 
            // renombrarToolStripMenuItem
            // 
            this.renombrarToolStripMenuItem.AccessibleName = "Renombrar";
            this.renombrarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSTB_NombreBD});
            this.renombrarToolStripMenuItem.Name = "renombrarToolStripMenuItem";
            this.renombrarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
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
            // cMS_ListaTablas
            // 
            this.cMS_ListaTablas.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevaToolStripMenuItem2,
            this.renombrarToolStripMenuItem1});
            this.cMS_ListaTablas.Name = "cMS_ListaTablas";
            this.cMS_ListaTablas.Size = new System.Drawing.Size(181, 70);
            // 
            // nuevaToolStripMenuItem2
            // 
            this.nuevaToolStripMenuItem2.AccessibleName = "Nueva";
            this.nuevaToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSTB_NuevaTabla});
            this.nuevaToolStripMenuItem2.Name = "nuevaToolStripMenuItem2";
            this.nuevaToolStripMenuItem2.Size = new System.Drawing.Size(180, 22);
            this.nuevaToolStripMenuItem2.Text = "Nueva";
            // 
            // tSTB_NuevaTabla
            // 
            this.tSTB_NuevaTabla.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tSTB_NuevaTabla.Name = "tSTB_NuevaTabla";
            this.tSTB_NuevaTabla.Size = new System.Drawing.Size(100, 23);
            this.tSTB_NuevaTabla.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tSTB_NuevaTabla_KeyPress);
            // 
            // tV_ListaTablas
            // 
<<<<<<< HEAD
            this.tV_ListaTablas.Location = new System.Drawing.Point(12, 27);
            this.tV_ListaTablas.Name = "tV_ListaTablas";
            this.tV_ListaTablas.Size = new System.Drawing.Size(121, 245);
=======
            this.tV_ListaTablas.ContextMenuStrip = this.cMS_ListaTablas;
            this.tV_ListaTablas.Location = new System.Drawing.Point(12, 27);
            this.tV_ListaTablas.Name = "tV_ListaTablas";
            this.tV_ListaTablas.Size = new System.Drawing.Size(121, 258);
>>>>>>> 2d665c868794be0e68d97e918e42bf66e6cace02
            this.tV_ListaTablas.TabIndex = 4;
            this.tV_ListaTablas.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tV_ListaTablas_NodeMouseClick);
            // 
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.AccessibleName = "Eliminar";
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
            // 
            // nombreTabla
            // 
            this.nombreTabla.AutoSize = true;
            this.nombreTabla.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nombreTabla.Location = new System.Drawing.Point(139, 27);
            this.nombreTabla.Name = "nombreTabla";
            this.nombreTabla.Size = new System.Drawing.Size(0, 20);
            this.nombreTabla.TabIndex = 5;
            // 
            // dGV_AtributosTabla
            // 
            this.dGV_AtributosTabla.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dGV_AtributosTabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGV_AtributosTabla.Location = new System.Drawing.Point(140, 60);
            this.dGV_AtributosTabla.Name = "dGV_AtributosTabla";
            this.dGV_AtributosTabla.Size = new System.Drawing.Size(648, 225);
            this.dGV_AtributosTabla.TabIndex = 6;
            // 
            // renombrarToolStripMenuItem1
            // 
            this.renombrarToolStripMenuItem1.AccessibleName = "Renombrar";
            this.renombrarToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSTB_nombreTabla});
            this.renombrarToolStripMenuItem1.Name = "renombrarToolStripMenuItem1";
            this.renombrarToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
            this.renombrarToolStripMenuItem1.Text = "Renombrar";
            // 
            // tSTB_nombreTabla
            // 
            this.tSTB_nombreTabla.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tSTB_nombreTabla.Name = "tSTB_nombreTabla";
            this.tSTB_nombreTabla.Size = new System.Drawing.Size(100, 23);
            this.tSTB_nombreTabla.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tSTB_nombreTabla_KeyPress);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dGV_AtributosTabla);
            this.Controls.Add(this.nombreTabla);
            this.Controls.Add(this.tV_ListaTablas);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.cMS_ListaTablas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dGV_AtributosTabla)).EndInit();
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
        private System.Windows.Forms.ContextMenuStrip cMS_ListaTablas;
        private System.Windows.Forms.ToolStripMenuItem nuevaToolStripMenuItem2;
        private System.Windows.Forms.ToolStripTextBox tSTB_NuevaTabla;
        private System.Windows.Forms.TreeView tV_ListaTablas;
        private System.Windows.Forms.ToolStripMenuItem eliminarToolStripMenuItem;
        private System.Windows.Forms.Label nombreTabla;
        private System.Windows.Forms.DataGridView dGV_AtributosTabla;
        private System.Windows.Forms.ToolStripMenuItem renombrarToolStripMenuItem1;
        private System.Windows.Forms.ToolStripTextBox tSTB_nombreTabla;
    }
}

