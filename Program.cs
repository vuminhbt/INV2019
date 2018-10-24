using System;
using System.Windows.Forms;

namespace INV2019
{
    static class Program
    {
        private static string userName = "";
        private static string passWord = "";
        private static bool isLogin = false;
        private static UserInfo userInfo = null;
        private static frmMain formMain;

        public static string UserName { get => userName; set => userName = value; }
        public static string PassWord { get => passWord; set => passWord = value; }
        public static UserInfo UserInfo { get => userInfo; set => userInfo = value; }

        /// <summary>
        /// Check for user is logged in
        /// </summary>
        public static bool IsLogin { get => isLogin; set => isLogin = value; }
        public static frmMain FormMain { get => formMain; set => formMain = value; }

        public static void Login(UserInfo obj)
        {
            try
            {
                if (obj != null)
                {
                    IsLogin = true;
                    UserName = obj.USERNAME;
                    UserInfo = obj;
                }
            }
            catch
            {
                IsLogin = false;
                UserName = "";
                UserInfo = null;
            }
        }
        public static void LogOut()
        {
            try
            {
                IsLogin = false;
                UserName = "";
                UserInfo = null;
            }
            catch
            {

            }
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            FormMain = new frmMain();
            Application.Run(FormMain);
        }
        /// <summary>
        /// Get Connection string
        /// </summary>
        /// <returns></returns>
        public static string conString()
        {
            var str = StringExtensions.Decode(System.Configuration.ConfigurationManager.AppSettings["dbKey"]);
            str = SecretPerson.DecryptString(str, "☆☆☆☆☆");
            return string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Jet OLEDB:Database Password={0};Data Source=" + Application.StartupPath + "\\db\\{1}", str, System.Configuration.ConfigurationManager.AppSettings["dbName"]);
        }

    }
}
