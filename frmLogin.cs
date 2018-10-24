using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INV2019
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
        OleDbConnection strcon = new OleDbConnection();
        public void Open_DataAccess()
        {
            string s = Program.conString();
            strcon.ConnectionString = s;
            strcon.Open();
        }

        public void Close_Connect()
        {
            strcon.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                string username = txtUserName.Text.Trim();
                string str = txtPass.Text.Trim();
                var data = SecretPerson.EncryptString(str, "☆☆☆☆☆");
                data = StringExtensions.Encode(data);

                DataSet ds = new DataSet();
                string s = "select tblUsers.ID,tblUsers.USERNAME,tblUsers.PASS,tblUsers.ACTIVE from tblUsers where tblUsers.USERNAME='" + username + "' and tblUsers.PASS  = '" + data + "' ";
                Open_DataAccess();
                OleDbDataAdapter daShowData = new OleDbDataAdapter(s, strcon);
                daShowData.Fill(ds);
                Close_Connect();
                DataRow dr = ds.Tables[0].Rows[0];
                UserInfo userInfo = new UserInfo();
                try { userInfo.ID = int.Parse(dr["ID"].ToString()); } catch { }
                try { userInfo.USERNAME = dr["USERNAME"].ToString(); } catch { }
                try { userInfo.PASS = dr["PASS"].ToString(); } catch { }
                try
                {
                    userInfo.ACTIVE = dr["ACTIVE"].ToString();
                }
                catch { }
                Program.Login(userInfo);
                this.Close();


            }
            catch
            {
                Program.Login(null);
            }

        }
    }
}
