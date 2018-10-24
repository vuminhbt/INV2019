using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace INV2019.UC
{
    public partial class uc_InOut : UserControl
    {
        
        private static uc_InOut _instance;
        public static uc_InOut Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_InOut();
                return _instance;
            }
        }
        public uc_InOut()
        {
            InitializeComponent();
        }

        private void uc_InOut_Load(object sender, EventArgs e)
        {
            try
            {

                Load_Data(DateTime.Now.ToString("dd/MM/yyyy"));
            }
            catch { }
        }


        public void Load_Data(string date)
        {
            try
            {
                ActiveControl = textBox1;
                DataSet ds = new DataSet();
                string strQuery = string.Format("SELECT i.ID, t.BARCODE as[Mã Vạch], t.CARNUMBER as[Biển Số], i.TIMEIN as[Giờ Vào], i.TIMEOUT as[Giờ Ra],IIf([i.TIMEOUT] is null, null, Format((cdate(i.TIMEOUT) - cdate(i.TIMEIN)),'hh:mm:ss')) as [Tổng Thời Gian(HH:MM:SS)],i.DESCRIPTION as[Mô Tả] FROM tblInOutINV i INNER JOIN tblTrantportInfo t ON i.TRANTID = t.ID WHERE (Format(i.TIMEIN, 'dd/MM/yyyy')) = '{0}' or i.TIMEOUT is null order by i.ID desc;", date);
                ds = DBHelper.ExecuteDataset(Program.conString(), CommandType.Text, strQuery);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[2].ReadOnly = true;
                dataGridView1.Columns[3].ReadOnly = true;
                dataGridView1.Columns[4].ReadOnly = true;
                dataGridView1.Columns[5].ReadOnly = true;
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].Width = 200;
                dataGridView1.Columns[2].Width = 150;
                dataGridView1.Columns[3].Width = 200;
                dataGridView1.Columns[4].Width = 200;
                dataGridView1.Columns[5].Width = 250;
                dataGridView1.Columns[6].Width = 400;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Loi");
            }
        }

        public void INSERT(string barcode)
        {
            try
            {

                if (!string.IsNullOrEmpty(barcode))
                {
                    var trantportInfo = getByBarcode(barcode);
                    if (trantportInfo == null)
                    {

                        MessageBox.Show("Không Tìm Thấy Thông Tin Xe. Nhập lại code khác", "Lỗi");
                        textBox1.Clear();
                        ActiveControl = textBox1;
                        return;
                    }
                    string carinfo = trantportInfo.CARNUMBER.ToString();
                    string date = DateTime.Now.ToString("dd/MM/yyyy");
                    InOutINVInfo inOutINVInfo = GETInOutINV(trantportInfo, date);
                    string strQuery;
                    if (inOutINVInfo != null)
                    {
                        //update timeout ngay hien tai
                        strQuery = @"Update tblInOutINV set TIMEOUT ='" + DateTime.Now + "' where ID=" + inOutINVInfo.ID;

                    }
                    else
                    {
                        inOutINVInfo = GETInOutINV(trantportInfo, null); // ngay vao gan nhat
                        if (inOutINVInfo != null) // cap nhat ngay vao gan nhat
                            strQuery = @"Update tblInOutINV set TIMEOUT ='" + DateTime.Now + "' where ID=" + inOutINVInfo.ID;
                        else  // tao moi luot ra vo
                            strQuery = @"INSERT INTO tblInOutINV(TRANTID,TIMEIN) values (" + trantportInfo.ID + ",'" + DateTime.Now + "')";
                    }

                    var i = DBHelper.ExecuteNonQuery(Program.conString(), CommandType.Text, strQuery);
                }
            }
            catch
            {
            }
        }
        TrantportInfo getByBarcode(string BARCODE)
        {
            try
            {
                DataSet ds = new DataSet();
                string strQuery = string.Format("Select *  FROM tblTrantportInfo where BARCODE='{0}'", BARCODE);
                ds = DBHelper.ExecuteDataset(Program.conString(), CommandType.Text, strQuery);
                DataRow dr = ds.Tables[0].Rows[0];
                UserInfo userInfo = Program.UserInfo;

                var trantportInfo = new TrantportInfo();
                try
                {
                    trantportInfo.BARCODE = dr["BARCODE"].ToString();
                }
                catch { }
                try
                {
                    trantportInfo.CARNUMBER = dr["CARNUMBER"].ToString();
                }
                catch { }
                try
                {
                    trantportInfo.CREATEBY = Program.UserInfo;
                }
                catch { }
                try
                { trantportInfo.ID = int.Parse(dr["ID"].ToString()); }
                catch { }


                return trantportInfo;
            }
            catch 
            {
               
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TrantportInfo"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public InOutINVInfo GETInOutINV(TrantportInfo TrantportInfo, string date)
        {
            try
            {

                DataSet ds = new DataSet();
                if (!string.IsNullOrEmpty(date))
                    date = " and Format(tblInOutINV.TIMEIN, 'dd/MM/yyyy') = '" + date + "'";
                string strQuery = string.Format("select top 1 tblInOutINV.ID,tblInOutINV.TRANTID,tblInOutINV.TIMEOUT,tblInOutINV.DESCRIPTION from tblInOutINV where tblInOutINV.TIMEOUT is null and tblInOutINV.TRANTID= {0}  {1}  order by tblInOutINV.ID desc", TrantportInfo.ID, date);
                ds = DBHelper.ExecuteDataset(Program.conString(), CommandType.Text, strQuery);
                DataRow dr = ds.Tables[0].Rows[0];
                InOutINVInfo inOutINVInfo = new InOutINVInfo();
                try { inOutINVInfo.ID = int.Parse(dr["ID"].ToString()); } catch { }
                try { inOutINVInfo.TIMEIN = dr["TIMEIN"].ToString(); } catch { }
                try { inOutINVInfo.TIMEOUT = dr["TIMEOUT"].ToString(); } catch { }
                try
                {
                    inOutINVInfo.TrantportInfo = TrantportInfo;
                }
                catch { }
                try
                { inOutINVInfo.DESCRIPTION = dr["DESCRIPTION"].ToString(); }
                catch { }
                return inOutINVInfo;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// DOC MA VACH KET QUA TRA VE SAU TIENG BIP KHOANG 3 SECOND
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void textBox1_TextChangedAsync(object sender, EventArgs e)
        {
            try
            {
                if (await textBox1.GetIdle())
                {
                    INSERT(textBox1.Text.Trim());

                    Load_Data(DateTime.Now.ToString("dd/MM/yyyy"));
                    textBox1.Text = "";
                }
            }
            catch { }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int bid = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                var des = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                string strQuery = @"Update tblInOutINV set DESCRIPTION ='" + des + "' where ID=" + bid;
                if (DBHelper.ExecuteNonQuery(Program.conString(), CommandType.Text, strQuery) > 0)
                    Load_Data(DateTime.Now.ToString("dd/MM/yyyy"));
                else
                    MessageBox.Show("Sửa thông tin lỗi. Nhập lại Mô Thông Tin khác", "Lỗi");
            }
            catch
            {


            }
        }
    }

    public static class UIExtensionMethods
    {
        public static async Task<bool> GetIdle(this TextBox txb)
        {
            string txt = txb.Text;
            await Task.Delay(Int32.Parse(System.Configuration.ConfigurationManager.AppSettings["timeWaitReadBarcode"]));
            return txt == txb.Text;
        }


    }
}

