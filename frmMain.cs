using INV2019.UC;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace INV2019
{
    public partial class frmMain : Form
    {

        private frmLogin _myfrmLogin;
        public frmMain()
        {
            InitializeComponent();
        }

        private void inout_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!panel1.Controls.Contains(uc_InOut.Instance))
            {
                panel1.Controls.Add(uc_InOut.Instance);
                uc_InOut.Instance.Dock = DockStyle.Fill;
                uc_InOut.Instance.BringToFront();
            }
            else
            { uc_InOut.Instance.BringToFront(); uc_InOut.Instance.Load_Data(DateTime.Now.ToString("dd/MM/yyyy")); }
            }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                CheckLogin();
                if (!panel1.Controls.Contains(uc_InOut.Instance))
                {
                    panel1.Controls.Add(uc_InOut.Instance);
                    uc_InOut.Instance.Dock = DockStyle.Fill;
                    uc_InOut.Instance.BringToFront();
                }
                else
                { uc_InOut.Instance.BringToFront();  }

            }
            catch { }
        }
        #region Function
        public  void CheckLogin()
        {
            if (Program.IsLogin)
            {
                this.login_StripMenuItem1.Visible = false;
                this.logout_StripMenuItem1.Visible = true;
                this.toolsToolStripMenuItem.Visible = true;
                this.urser_ToolStripMenuItem.Visible = false;
            }
            else
            {
                this.login_StripMenuItem1.Visible = true;
                this.logout_StripMenuItem1.Visible = false;
                this.toolsToolStripMenuItem.Visible = false;
                this.urser_ToolStripMenuItem.Visible = false;
            }
        }
        public void LogInYes(Form form)
        {
            form.MdiParent = this;
            panel1.Controls.Add(form);
            form.Show();
            form.BringToFront();
            form.Location = new Point(0, 0);
        }
        public bool LogInNo()
        {
            if (!Program.IsLogin)
            {
                _myfrmLogin = new frmLogin();
                _myfrmLogin.ShowDialog(this);
                CheckLogin();
            }

            return Program.IsLogin;
        }
        #endregion

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

        private void urser_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!panel1.Controls.Contains(uc_User_Manager.Instance))
                {
                    panel1.Controls.Add(uc_User_Manager.Instance);
                    uc_User_Manager.Instance.Dock = DockStyle.Fill;
                    uc_User_Manager.Instance.BringToFront();
                }
                else
                {
                    uc_User_Manager.Instance.BringToFront();
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        private void tranInfo_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!panel1.Controls.Contains(uc_Trantport_Manager.Instance))
                {
                    panel1.Controls.Add(uc_Trantport_Manager.Instance);
                    uc_Trantport_Manager.Instance.Dock = DockStyle.Fill;
                    uc_Trantport_Manager.Instance.BringToFront();
                }
                else
                {
                    uc_Trantport_Manager.Instance.BringToFront();
                    uc_Trantport_Manager.Instance.loadDataToGrid();
                    uc_Trantport_Manager.Instance.cleartdata();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        private void managerInOut_StripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!panel1.Controls.Contains(uc_InOut_Export_Excel.Instance))
                {
                    panel1.Controls.Add(uc_InOut_Export_Excel.Instance);
                    uc_InOut_Export_Excel.Instance.Dock = DockStyle.Fill;
                    uc_InOut_Export_Excel.Instance.BringToFront();
                }
                else
                {
                    uc_InOut_Export_Excel.Instance.BringToFront();
                    uc_InOut_Export_Excel.Instance.loaddata("", "", DateTime.Now.AddDays(-7).ToString(), DateTime.Now.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        private void login_StripMenuItem1_Click(object sender, EventArgs e)
        {
            _myfrmLogin = new frmLogin();
            _myfrmLogin.ShowDialog(this);
            CheckLogin();
        }

        private void logout_StripMenuItem1_Click(object sender, EventArgs e)
        {
            frmLogout myfrmLogout = new frmLogout();
            myfrmLogout.ShowDialog(this);
            CheckLogin();
        }
    }
}
