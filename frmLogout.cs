using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INV2019
{
    public partial class frmLogout : Form
    {
        public frmLogout()
        {
            InitializeComponent();
        }


        private void frmLogout_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Program.LogOut();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();            
        }
    }
}
