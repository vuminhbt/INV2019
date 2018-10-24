using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;

namespace INV2019.UC
{
    public partial class uc_Trantport_Manager : UserControl
    {
        
        private static uc_Trantport_Manager _instance;
        public static uc_Trantport_Manager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_Trantport_Manager();
                return _instance;
            }
        }
        public uc_Trantport_Manager()
        {
            InitializeComponent();
        }

        private void uc_TranformInfo_Load(object sender, EventArgs e)
        {
            Program.FormMain.CheckLogin();
            ActiveControl = txtCode;
            txtCode.Focus();
            loadDataToGrid();
            button1.Visible = false;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            txtId.Visible = false;
        }
        
    

        public void loadDataToGrid()
        {
            try
            {
                DataSet ds = new DataSet();
                string strQuery = "select id, BARCODE as [Mã Vạch], CARNUMBER as [Biển Số Xe], COMPANY as [Công Ty]  from tblTrantportInfo order by ID desc";
                ds = DBHelper.ExecuteDataset(Program.conString(), CommandType.Text, strQuery);
                dataGridView1.DataSource = ds.Tables[0];

                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[2].ReadOnly = true;
                dataGridView1.Columns[3].ReadOnly = true;
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].Width = 300;
                dataGridView1.Columns[2].Width = 300;
                dataGridView1.Columns[3].Width = 400;
                btnSave.Visible = true;
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
                button1.Visible = false;

            }
            catch
            {

            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCode.Text) || string.IsNullOrEmpty(txtCode.Text))
                {
                    MessageBox.Show("Nhập đầy đủ thông tin", "Lỗi");
                    return;
                }
                var trantportInfo = new TrantportInfo();
                trantportInfo.BARCODE = txtCode.Text;
                trantportInfo.CARNUMBER = txtCarInfo.Text;
                trantportInfo.COMPANY = txtCompany.Text;
                trantportInfo.CREATEBY = Program.UserInfo;
                var t = getByBarcode(trantportInfo.BARCODE);
                if (t != null)
                {
                    MessageBox.Show("Đã Tồn Tại Mã Vạch", "Lỗi");
                    return;
                }
                string strQuery = string.Format("INSERT INTO tblTrantportInfo(BARCODE, CARNUMBER,COMPANY, CREATEBY) VALUES('{0}','{1}','{2}',{3})", trantportInfo.BARCODE, trantportInfo.CARNUMBER, trantportInfo.COMPANY, trantportInfo.CREATEBY.ID);

                var i = DBHelper.ExecuteNonQuery(Program.conString(), CommandType.Text, strQuery);
                if (i > 0)
                    MessageBox.Show("Tạo mới thành công", "Thông báo");
                else
                {
                    MessageBox.Show("Tạo mới không thành công", "Thông báo");
                    return;
                }
                cleartdata();
                loadDataToGrid();
            }
            catch
            {

            }
        }
     public   void cleartdata()
        {
            txtId.Clear();
            txtCarInfo.Clear();
            txtCode.Clear();
            txtCode.Focus();
            txtCompany.Clear();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            button1.Visible = false;
            txtId.Visible = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if(!string.IsNullOrEmpty(txtId.Text))
                {
                    if (string.IsNullOrEmpty(txtCode.Text) || string.IsNullOrEmpty(txtCode.Text))
                    {
                        MessageBox.Show("Nhập đầy đủ thông tin", "Lỗi");
                        return;
                    }
                    var trantportInfo = getByID(txtId.Text.Trim());
                    trantportInfo.BARCODE = txtCode.Text;
                    trantportInfo.CARNUMBER = txtCarInfo.Text;
                    trantportInfo.COMPANY = txtCompany.Text;
                    string strQuery = string.Format("UPDATE tblTrantportInfo set BARCODE='{0}', CARNUMBER='{1}', COMPANY='{2}' where id={3}", trantportInfo.BARCODE, trantportInfo.CARNUMBER, trantportInfo.COMPANY, trantportInfo.ID);

                    var i = DBHelper.ExecuteNonQuery(Program.conString(), CommandType.Text, strQuery);
                    if (i > 0)
                        MessageBox.Show("Cập nhật thành công", "Thông báo");
                    else
                    {
                        MessageBox.Show("Cập nhật không thành công", "Thông báo");
                        return;
                    }
                    cleartdata();
                    loadDataToGrid();
                }
                else
                {
                    MessageBox.Show("Chọn Xe cần sửa", "Lỗi");
                }
                
            }
            catch 
            {
               
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                string id = txtId.Text;
                if(getByIDFromInOut(id)>0)
                {
                    MessageBox.Show("Xóa không thành công. Bạn phải xóa thông tin giờ ra vào của xe trước.", "Thông báo");
                    return;
                }
                string strQuery = string.Format("DELETE FROM tblTrantportInfo where id={0}", id);
                var i = DBHelper.ExecuteNonQuery(Program.conString(), CommandType.Text, strQuery);
                if (i > 0)
                    MessageBox.Show("Xóa thành công", "Thông báo");
                else
                {
                    MessageBox.Show("Xóa không thành công", "Thông báo");
                    return;
                }
                cleartdata();
                loadDataToGrid();
            }
            catch
            {
               
            }
        }

        /// <summary>
        /// thong tin xe theo barcode
        /// </summary>
        /// <param name="BARCODE"></param>
        /// <returns></returns>
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
                    trantportInfo.COMPANY = dr["COMPANY"].ToString();
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
        /// thong tin xe theo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TrantportInfo getByID(string id)
        {
            try
            {
                DataSet ds = new DataSet();

                string strQuery = string.Format("Select *  FROM tblTrantportInfo where id={0}", id);
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
                    trantportInfo.COMPANY = dr["COMPANY"].ToString();
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
        /// kiem tra co ton tai thong tin xe trong table tblInOutINV
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int getByIDFromInOut(string id)
        {
            try
            {
                DataSet ds = new DataSet();
                string strQuery = string.Format("Select TRANTID  FROM tblInOutINV where TRANTID={0}", id);
                ds = DBHelper.ExecuteDataset(Program.conString(), CommandType.Text, strQuery);
                return ds.Tables[0].Rows.Count;
            }
            catch
            {
                return 0;
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string str = dataGridView1.Rows[e.RowIndex].Cells["id"].Value.ToString();
                var trantportInfo = getByID(str);
                txtCode.Text = trantportInfo.BARCODE.ToString();
                txtId.Text = trantportInfo.ID.ToString();
                txtCarInfo.Text = trantportInfo.CARNUMBER.ToString();
                txtCompany.Text = trantportInfo.COMPANY.ToString();
                btnSave.Visible = false;
                btnUpdate.Visible = true;
                btnDelete.Visible = true;
                button1.Visible = true; ;
            }
            catch 
            {
                
            }
        }

        private void btnSave1_Click(object sender, EventArgs e)
        {
            cleartdata();
        }
    }
}
