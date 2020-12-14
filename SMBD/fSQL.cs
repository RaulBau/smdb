using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMBD
{
    public partial class fSQL : Form
    {
        public string pathBase;
        public List<Dictionary<string, object>> baseDatos;

        public fSQL(string pb)
        {
            this.pathBase = pb;
            InitializeComponent();
        }

        private void fSQL_Load(object sender, EventArgs e)
        {
            Console.WriteLine(this.pathBase);
        }

        private void btn_Ejecutar_Click(object sender, EventArgs e)
        {

        }
    }
}
