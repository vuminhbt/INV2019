using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INV2019.UC
{
    public partial class uc_User_Manager : UserControl
    {
        private static uc_User_Manager _instance;
        public static uc_User_Manager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_User_Manager();
                return _instance;
            }
        }
        public uc_User_Manager()
        {
            InitializeComponent();
        }

        private void uc_User_Manager_Load(object sender, EventArgs e)
        {
            Program.FormMain.CheckLogin();
        }
    }
}
