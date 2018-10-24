using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

namespace INV2019.UC
{
    public partial class uc_Login : UserControl
    {
       
        private static uc_Login _instance;
        public static uc_Login Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_Login();
                return _instance;
            }
        }
        public uc_Login()
        {
            InitializeComponent();
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

                   
                    frmMain frm = new frmMain();
                    
                    frm.Show();
                    

                }
                catch
                {
                    Program.Login(null);
                }
            }
            catch { }

        }

        private void uc_Login_Load(object sender, EventArgs e)
        {
           
          
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
