using System;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Drawing;

namespace INV2019.UC
{
    public partial class uc_InOut_Export_Excel : UserControl
    {
        private static uc_InOut_Export_Excel _instance;
        public static uc_InOut_Export_Excel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new uc_InOut_Export_Excel();
                return _instance;
            }
        }
        public uc_InOut_Export_Excel()
        {
            InitializeComponent();
        }

        private void uc_InOut_Export_Excel_Load(object sender, EventArgs e)
        {
            Program.FormMain.CheckLogin();
            dTo.Value = DateTime.Now;
            dtFrom.Value = DateTime.Now.AddDays(-7);
            loaddata(txtBarcode.Text, txtCarnumber.Text, dtFrom.Text, dTo.Text);
           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                loaddata(txtBarcode.Text, txtCarnumber.Text, dtFrom.Text, dTo.Text);
            }
            catch { }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    var XcelApp = new Microsoft.Office.Interop.Excel.Application();
                    XcelApp.Application.Workbooks.Add(Type.Missing);

                    for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
                    {
                        XcelApp.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                    }

                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        for (int j = 1; j < dataGridView1.Columns.Count; j++)
                        {
                            try
                            {
                                XcelApp.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                            }
                            catch
                            {
                                XcelApp.Cells[i + 2, j + 1] = "";
                            }
                        }
                    }
                    XcelApp.Columns.AutoFit();
                    XcelApp.Columns.Borders.Value = 0;

                    XcelApp.Visible = true;
                }
            }
            catch { }

        }




        public void loaddata(string barcode, string carnumber, string dfrom, string dto)
        {
            try
            {
                dataGridView1.Columns.Clear();
                string search = null;
                if (!string.IsNullOrEmpty(barcode))
                    search += string.Format(" and t.BARCODE like '%{0}%' ", barcode);
                if (!string.IsNullOrEmpty(carnumber))
                    search += string.Format(" and t.CARNUMBER like '%{0}%' ", carnumber);
                if (!string.IsNullOrEmpty(dfrom) && !string.IsNullOrEmpty(dto))
                {
                    search += string.Format(" and ((Format(i.TIMEIN,'dd/MM/yyyy')  between '{0}' and '{1}') or (Format(i.TIMEOUT,'dd/MM/yyyy')  between '{0}' and '{1}'))", dfrom, dto);
                }
                if (!string.IsNullOrEmpty(search))
                {
                    search = search.Trim();
                    search = search.Substring(3, search.Length - 3);
                    search = "WHERE " + search;
                }
                DataSet ds = new DataSet();
                string strQuery = string.Format("SELECT i.ID, t.BARCODE as[Mã Vạch], t.CARNUMBER as[Biển Số],t.COMPANY as[Công Ty], i.TIMEIN as[Giờ Vào], i.TIMEOUT as[Giờ Ra],IIf([i.TIMEOUT] is null, null, Format((cdate(i.TIMEOUT) - cdate(i.TIMEIN)),'hh:mm:ss')) as [Tổng Thời Gian(HH:MM:SS)], i.DESCRIPTION as[Mô Tả] FROM tblInOutINV i INNER JOIN tblTrantportInfo t ON i.TRANTID = t.ID {0} order by i.ID desc;", search);
                ds = DBHelper.ExecuteDataset(Program.conString(), CommandType.Text, strQuery);
                dataGridView1.DataSource = ds.Tables[0];

                dataGridView1.Columns[0].ReadOnly = true;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[2].ReadOnly = true;
                dataGridView1.Columns[3].ReadOnly = true;
                dataGridView1.Columns[4].ReadOnly = true;
                dataGridView1.Columns[5].ReadOnly = true;
                dataGridView1.Columns[6].ReadOnly = true;

                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[2].Width = 100;
                dataGridView1.Columns[3].Width = 200;
                dataGridView1.Columns[4].Width = 130;
                dataGridView1.Columns[5].Width = 130;
                dataGridView1.Columns[6].Width = 200;
                dataGridView1.Columns[7].Width = 350;

                DataGridViewCheckBoxColumn checkboxColumn = new DataGridViewCheckBoxColumn();
                checkboxColumn.Width = 30;
                checkboxColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns.Insert(0, checkboxColumn);

                // add checkbox header
                Rectangle rect = dataGridView1.GetCellDisplayRectangle(0, -1, true);
                // set checkbox header to center of header cell. +1 pixel to position correctly.
                rect.X = rect.Location.X + (rect.Width / 4);
                rect.Y = rect.Location.Y + (rect.Height / 4);
                CheckBox checkboxHeader = new CheckBox();
                checkboxHeader.Name = "checkboxHeader";
                checkboxHeader.Size = new Size(18, 18);
                checkboxHeader.Location = rect.Location;
                checkboxHeader.CheckedChanged += new EventHandler(checkboxHeader_CheckedChanged);

                dataGridView1.Controls.Add(checkboxHeader);


            }
            catch
            { }
        }
        private void checkboxHeader_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1[0, i].Value = ((CheckBox)dataGridView1.Controls.Find("checkboxHeader", true)[0]).Checked;
            }
            dataGridView1.EndEdit();
        }

        void deleteinv(string id)
        {
            try
            {

                string strQuery = string.Format(@"delete FROM tblInOutINV where ID in({0})", id);
                int i = DBHelper.ExecuteNonQuery(Program.conString(), CommandType.Text, strQuery);
                if (i > 0)
                {
                    MessageBox.Show("Xóa thành công "+i+" records", "Thông Báo");
                    loaddata(txtBarcode.Text, txtCarnumber.Text, dtFrom.Text, dTo.Text);
                }
                else
                    MessageBox.Show("Xóa thông tin lỗi. Nhập lại Thông Tin khác", "Lỗi");
            }
            catch
            {


            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 8 && e.RowIndex >= 0)
                {
                    int bid = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString());
                    var des = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    string strQuery = @"Update tblInOutINV set DESCRIPTION ='" + des + "' where ID=" + bid;
                    if (DBHelper.ExecuteNonQuery(Program.conString(), CommandType.Text, strQuery) > 0)
                        loaddata(txtBarcode.Text, txtCarnumber.Text, dtFrom.Text, dTo.Text);
                    else
                        MessageBox.Show("Sửa thông tin lỗi. Nhập lại Thông Tin khác", "Lỗi");
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
                string ids=null;
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    //dataGridView1[0, i].Value = ((CheckBox)dataGridView1.Controls.Find("checkboxHeader", true)[0]).Checked;
                    var t1 = dataGridView1[0, i].Value;
                   if(t1!=null && (bool)t1==true)
                    {
                        ids += ","+(dataGridView1.Rows[i].Cells["ID"].Value.ToString());
                    }
                   
                    
                }
                if(!string.IsNullOrEmpty(ids))
                {
                    DialogResult result = MessageBox.Show("Bạn chắc chắn muốn xóa", "Thông Báo", MessageBoxButtons.OKCancel);
                    if (result == DialogResult.OK)
                    {
                        ids = ids.TrimStart(',');
                        deleteinv(ids);
                        loaddata(txtBarcode.Text, txtCarnumber.Text, dtFrom.Text, dTo.Text);
                    }
                }

            }
            catch
            { }
        }
    }
}
