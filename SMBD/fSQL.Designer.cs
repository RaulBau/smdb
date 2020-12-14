
namespace SMBD
{
    partial class fSQL
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tB_Compilacion = new System.Windows.Forms.TextBox();
            this.tB_Salida = new System.Windows.Forms.TextBox();
            this.btn_Ejecutar = new System.Windows.Forms.Button();
            this.rTB_Sentencias = new System.Windows.Forms.RichTextBox();
            this.dGV_Registros = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGV_Registros)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tB_Compilacion);
            this.splitContainer1.Panel1.Controls.Add(this.tB_Salida);
            this.splitContainer1.Panel1.Controls.Add(this.btn_Ejecutar);
            this.splitContainer1.Panel1.Controls.Add(this.rTB_Sentencias);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dGV_Registros);
            this.splitContainer1.Size = new System.Drawing.Size(838, 441);
            this.splitContainer1.SplitterDistance = 227;
            this.splitContainer1.TabIndex = 1;
            // 
            // tB_Compilacion
            // 
            this.tB_Compilacion.Location = new System.Drawing.Point(367, 16);
            this.tB_Compilacion.Name = "tB_Compilacion";
            this.tB_Compilacion.Size = new System.Drawing.Size(100, 20);
            this.tB_Compilacion.TabIndex = 3;
            this.tB_Compilacion.Visible = false;
            // 
            // tB_Salida
            // 
            this.tB_Salida.Location = new System.Drawing.Point(104, 16);
            this.tB_Salida.Name = "tB_Salida";
            this.tB_Salida.Size = new System.Drawing.Size(257, 20);
            this.tB_Salida.TabIndex = 2;
            // 
            // btn_Ejecutar
            // 
            this.btn_Ejecutar.Location = new System.Drawing.Point(12, 13);
            this.btn_Ejecutar.Name = "btn_Ejecutar";
            this.btn_Ejecutar.Size = new System.Drawing.Size(75, 23);
            this.btn_Ejecutar.TabIndex = 1;
            this.btn_Ejecutar.Text = "Ejecutar";
            this.btn_Ejecutar.UseVisualStyleBackColor = true;
            this.btn_Ejecutar.Click += new System.EventHandler(this.btn_Ejecutar_Click);
            // 
            // rTB_Sentencias
            // 
            this.rTB_Sentencias.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rTB_Sentencias.Location = new System.Drawing.Point(12, 48);
            this.rTB_Sentencias.Name = "rTB_Sentencias";
            this.rTB_Sentencias.Size = new System.Drawing.Size(814, 176);
            this.rTB_Sentencias.TabIndex = 0;
            this.rTB_Sentencias.Text = "";
            this.rTB_Sentencias.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rTB_Sentencias_KeyDown);
            // 
            // dGV_Registros
            // 
            this.dGV_Registros.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dGV_Registros.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dGV_Registros.Location = new System.Drawing.Point(12, 3);
            this.dGV_Registros.Name = "dGV_Registros";
            this.dGV_Registros.Size = new System.Drawing.Size(814, 195);
            this.dGV_Registros.TabIndex = 0;
            // 
            // fSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 441);
            this.Controls.Add(this.splitContainer1);
            this.Name = "fSQL";
            this.Text = "SQL";
            this.Load += new System.EventHandler(this.fSQL_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dGV_Registros)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox rTB_Sentencias;
        private System.Windows.Forms.DataGridView dGV_Registros;
        private System.Windows.Forms.Button btn_Ejecutar;
        private System.Windows.Forms.TextBox tB_Salida;
        private System.Windows.Forms.TextBox tB_Compilacion;
    }
}