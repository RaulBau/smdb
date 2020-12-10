
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
            this.eliminarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.renombrarToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tSTB_nombreTabla = new System.Windows.Forms.ToolStripTextBox();
            this.tV_ListaTablas = new System.Windows.Forms.TreeView();
            this.nombreTabla = new System.Windows.Forms.Label();
            this.dGV_AtributosTabla = new System.Windows.Forms.DataGridView();
            this.cMS_atributos = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.renombrarToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tB_NombreAtributo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cB_tipoAtributo = new System.Windows.Forms.ComboBox();
            this.cB_llavePrimaria = new System.Windows.Forms.CheckBox();
            this.cB_llaveForanea = new System.Windows.Forms.CheckBox();
            this.cB_llavesForaneas = new System.Windows.Forms.ComboBox();
            this.btn_agregaArtibuto = new System.Windows.Forms.Button();
            this.tB_tamCadena = new System.Windows.Forms.TextBox();
            this.dGV_nuevoRegistro = new System.Windows.Forms.DataGridView();
            this.btn_EliminarAtributo = new System.Windows.Forms.Button();
            this.btn_eliminarRegistro = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.cMS_ListaTablas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGV_AtributosTabla)).BeginInit();
            this.cMS_atributos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGV_nuevoRegistro)).BeginInit();
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
            this.archivoToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.archivoToolStripMenuItem.Text = "Base de datos";
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
            // eliminarToolStripMenuItem
            // 
            this.eliminarToolStripMenuItem.AccessibleName = "Eliminar";
            this.eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            this.eliminarToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.eliminarToolStripMenuItem.Text = "Eliminar";
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
            this.cMS_ListaTablas.Size = new System.Drawing.Size(134, 48);
            // 
            // nuevaToolStripMenuItem2
            // 
            this.nuevaToolStripMenuItem2.AccessibleName = "Nueva";
            this.nuevaToolStripMenuItem2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSTB_NuevaTabla});
            this.nuevaToolStripMenuItem2.Name = "nuevaToolStripMenuItem2";
            this.nuevaToolStripMenuItem2.Size = new System.Drawing.Size(133, 22);
            this.nuevaToolStripMenuItem2.Text = "Nueva";
            // 
            // tSTB_NuevaTabla
            // 
            this.tSTB_NuevaTabla.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tSTB_NuevaTabla.Name = "tSTB_NuevaTabla";
            this.tSTB_NuevaTabla.Size = new System.Drawing.Size(100, 23);
            this.tSTB_NuevaTabla.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tSTB_NuevaTabla_KeyPress);
            // 
            // renombrarToolStripMenuItem1
            // 
            this.renombrarToolStripMenuItem1.AccessibleName = "Renombrar";
            this.renombrarToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tSTB_nombreTabla});
            this.renombrarToolStripMenuItem1.Name = "renombrarToolStripMenuItem1";
            this.renombrarToolStripMenuItem1.Size = new System.Drawing.Size(133, 22);
            this.renombrarToolStripMenuItem1.Text = "Renombrar";
            // 
            // tSTB_nombreTabla
            // 
            this.tSTB_nombreTabla.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tSTB_nombreTabla.Name = "tSTB_nombreTabla";
            this.tSTB_nombreTabla.Size = new System.Drawing.Size(100, 23);
            this.tSTB_nombreTabla.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tSTB_nombreTabla_KeyPress);
            // 
            // tV_ListaTablas
            // 
            this.tV_ListaTablas.ContextMenuStrip = this.cMS_ListaTablas;
            this.tV_ListaTablas.Location = new System.Drawing.Point(12, 27);
            this.tV_ListaTablas.Name = "tV_ListaTablas";
            this.tV_ListaTablas.Size = new System.Drawing.Size(151, 258);
            this.tV_ListaTablas.TabIndex = 4;
            this.tV_ListaTablas.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tV_ListaTablas_NodeMouseClick);
            // 
            // nombreTabla
            // 
            this.nombreTabla.AutoSize = true;
            this.nombreTabla.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nombreTabla.Location = new System.Drawing.Point(165, 27);
            this.nombreTabla.Name = "nombreTabla";
            this.nombreTabla.Size = new System.Drawing.Size(0, 20);
            this.nombreTabla.TabIndex = 5;
            // 
            // dGV_AtributosTabla
            // 
            this.dGV_AtributosTabla.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dGV_AtributosTabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGV_AtributosTabla.ContextMenuStrip = this.cMS_atributos;
            this.dGV_AtributosTabla.Location = new System.Drawing.Point(169, 60);
            this.dGV_AtributosTabla.Name = "dGV_AtributosTabla";
            this.dGV_AtributosTabla.Size = new System.Drawing.Size(619, 185);
            this.dGV_AtributosTabla.TabIndex = 6;
            this.dGV_AtributosTabla.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dGV_AtributosTabla_ColumnHeaderMouseClick);
            // 
            // cMS_atributos
            // 
            this.cMS_atributos.Enabled = false;
            this.cMS_atributos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.renombrarToolStripMenuItem2});
            this.cMS_atributos.Name = "cMS_atributos";
            this.cMS_atributos.Size = new System.Drawing.Size(105, 26);
            this.cMS_atributos.Closed += new System.Windows.Forms.ToolStripDropDownClosedEventHandler(this.cMS_atributos_Closed);
            // 
            // renombrarToolStripMenuItem2
            // 
            this.renombrarToolStripMenuItem2.AccessibleName = "Editar";
            this.renombrarToolStripMenuItem2.Enabled = false;
            this.renombrarToolStripMenuItem2.Name = "renombrarToolStripMenuItem2";
            this.renombrarToolStripMenuItem2.Size = new System.Drawing.Size(104, 22);
            this.renombrarToolStripMenuItem2.Text = "Editar";
            // 
            // tB_NombreAtributo
            // 
            this.tB_NombreAtributo.Location = new System.Drawing.Point(63, 289);
            this.tB_NombreAtributo.Name = "tB_NombreAtributo";
            this.tB_NombreAtributo.Size = new System.Drawing.Size(100, 20);
            this.tB_NombreAtributo.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 292);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Nombre";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 314);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Tipo";
            // 
            // cB_tipoAtributo
            // 
            this.cB_tipoAtributo.FormattingEnabled = true;
            this.cB_tipoAtributo.Items.AddRange(new object[] {
            "Entero",
            "Decimal",
            "Cadena"});
            this.cB_tipoAtributo.Location = new System.Drawing.Point(63, 311);
            this.cB_tipoAtributo.Name = "cB_tipoAtributo";
            this.cB_tipoAtributo.Size = new System.Drawing.Size(100, 21);
            this.cB_tipoAtributo.TabIndex = 10;
            this.cB_tipoAtributo.SelectedIndexChanged += new System.EventHandler(this.cB_tipoAtributo_SelectedIndexChanged);
            // 
            // cB_llavePrimaria
            // 
            this.cB_llavePrimaria.AutoSize = true;
            this.cB_llavePrimaria.Location = new System.Drawing.Point(63, 338);
            this.cB_llavePrimaria.Name = "cB_llavePrimaria";
            this.cB_llavePrimaria.Size = new System.Drawing.Size(91, 17);
            this.cB_llavePrimaria.TabIndex = 12;
            this.cB_llavePrimaria.Text = "Llave primaria";
            this.cB_llavePrimaria.UseVisualStyleBackColor = true;
            this.cB_llavePrimaria.Click += new System.EventHandler(this.cB_llavePrimaria_Click);
            // 
            // cB_llaveForanea
            // 
            this.cB_llaveForanea.AutoSize = true;
            this.cB_llaveForanea.Location = new System.Drawing.Point(63, 361);
            this.cB_llaveForanea.Name = "cB_llaveForanea";
            this.cB_llaveForanea.Size = new System.Drawing.Size(91, 17);
            this.cB_llaveForanea.TabIndex = 13;
            this.cB_llaveForanea.Text = "Llave foranea";
            this.cB_llaveForanea.UseVisualStyleBackColor = true;
            this.cB_llaveForanea.Click += new System.EventHandler(this.cB_llaveForanea_Click);
            // 
            // cB_llavesForaneas
            // 
            this.cB_llavesForaneas.FormattingEnabled = true;
            this.cB_llavesForaneas.Location = new System.Drawing.Point(63, 385);
            this.cB_llavesForaneas.Name = "cB_llavesForaneas";
            this.cB_llavesForaneas.Size = new System.Drawing.Size(100, 21);
            this.cB_llavesForaneas.TabIndex = 14;
            this.cB_llavesForaneas.Visible = false;
            this.cB_llavesForaneas.SelectedIndexChanged += new System.EventHandler(this.cB_llavesForaneas_SelectedIndexChanged);
            // 
            // btn_agregaArtibuto
            // 
            this.btn_agregaArtibuto.Location = new System.Drawing.Point(88, 415);
            this.btn_agregaArtibuto.Name = "btn_agregaArtibuto";
            this.btn_agregaArtibuto.Size = new System.Drawing.Size(75, 23);
            this.btn_agregaArtibuto.TabIndex = 15;
            this.btn_agregaArtibuto.Text = "Agregar";
            this.btn_agregaArtibuto.UseVisualStyleBackColor = true;
            this.btn_agregaArtibuto.Click += new System.EventHandler(this.btn_agregaArtibuto_Click);
            // 
            // tB_tamCadena
            // 
            this.tB_tamCadena.Location = new System.Drawing.Point(169, 312);
            this.tB_tamCadena.Name = "tB_tamCadena";
            this.tB_tamCadena.Size = new System.Drawing.Size(54, 20);
            this.tB_tamCadena.TabIndex = 16;
            this.tB_tamCadena.Visible = false;
            // 
            // dGV_nuevoRegistro
            // 
            this.dGV_nuevoRegistro.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dGV_nuevoRegistro.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGV_nuevoRegistro.Location = new System.Drawing.Point(169, 251);
            this.dGV_nuevoRegistro.Name = "dGV_nuevoRegistro";
            this.dGV_nuevoRegistro.Size = new System.Drawing.Size(619, 54);
            this.dGV_nuevoRegistro.TabIndex = 17;
            this.dGV_nuevoRegistro.RowLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGV_nuevoRegistro_RowLeave);
            this.dGV_nuevoRegistro.RowValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dGV_nuevoRegistro_RowValidated);
            this.dGV_nuevoRegistro.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dGV_nuevoRegistro_KeyPress);
            // 
            // btn_EliminarAtributo
            // 
            this.btn_EliminarAtributo.Location = new System.Drawing.Point(229, 309);
            this.btn_EliminarAtributo.Name = "btn_EliminarAtributo";
            this.btn_EliminarAtributo.Size = new System.Drawing.Size(105, 23);
            this.btn_EliminarAtributo.TabIndex = 18;
            this.btn_EliminarAtributo.Text = "Eliminar Atributo";
            this.btn_EliminarAtributo.UseVisualStyleBackColor = true;
            this.btn_EliminarAtributo.Click += new System.EventHandler(this.btn_EliminarAtributo_Click);
            // 
            // btn_eliminarRegistro
            // 
            this.btn_eliminarRegistro.Location = new System.Drawing.Point(340, 310);
            this.btn_eliminarRegistro.Name = "btn_eliminarRegistro";
            this.btn_eliminarRegistro.Size = new System.Drawing.Size(105, 23);
            this.btn_eliminarRegistro.TabIndex = 19;
            this.btn_eliminarRegistro.Text = "Eliminar Registro";
            this.btn_eliminarRegistro.UseVisualStyleBackColor = true;
            this.btn_eliminarRegistro.Click += new System.EventHandler(this.btn_eliminarRegistro_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_eliminarRegistro);
            this.Controls.Add(this.btn_EliminarAtributo);
            this.Controls.Add(this.dGV_nuevoRegistro);
            this.Controls.Add(this.tB_tamCadena);
            this.Controls.Add(this.btn_agregaArtibuto);
            this.Controls.Add(this.cB_llavesForaneas);
            this.Controls.Add(this.cB_llaveForanea);
            this.Controls.Add(this.cB_llavePrimaria);
            this.Controls.Add(this.cB_tipoAtributo);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tB_NombreAtributo);
            this.Controls.Add(this.dGV_AtributosTabla);
            this.Controls.Add(this.nombreTabla);
            this.Controls.Add(this.tV_ListaTablas);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(179, 489);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.cMS_ListaTablas.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dGV_AtributosTabla)).EndInit();
            this.cMS_atributos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dGV_nuevoRegistro)).EndInit();
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
        private System.Windows.Forms.TextBox tB_NombreAtributo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cB_tipoAtributo;
        private System.Windows.Forms.CheckBox cB_llavePrimaria;
        private System.Windows.Forms.CheckBox cB_llaveForanea;
        private System.Windows.Forms.ComboBox cB_llavesForaneas;
        private System.Windows.Forms.Button btn_agregaArtibuto;
        private System.Windows.Forms.TextBox tB_tamCadena;
        private System.Windows.Forms.ContextMenuStrip cMS_atributos;
        private System.Windows.Forms.ToolStripMenuItem renombrarToolStripMenuItem2;
        private System.Windows.Forms.DataGridView dGV_nuevoRegistro;
        private System.Windows.Forms.Button btn_EliminarAtributo;
        private System.Windows.Forms.Button btn_eliminarRegistro;
    }
}

