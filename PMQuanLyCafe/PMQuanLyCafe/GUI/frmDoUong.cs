using PMQuanLyCafe.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMQuanLyCafe.GUI
{
    public partial class frmDoUong : Form
    {
        public frmDoUong()
        {
            InitializeComponent();
        }
        DoUong_DAO db = new DoUong_DAO();
        public static Boolean save = true;
        public static string MaDanhMuc;
        private void frmDoUong_Load(object sender, EventArgs e)
        {
            GetAll();
        }
        private void GetAll()
        {
            DataTable dt = new DataTable();
            dt = db.GetAll(txtSearch.Text);
            dtgv.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string MaDoUong = dt.Rows[i]["MaDoUong"].ToString();
                string MaDanhMuc = dt.Rows[i]["MaDanhMuc"].ToString();
                string TenDoUong = dt.Rows[i]["TenDoUong"].ToString();
                string MoTa = dt.Rows[i]["MoTa"].ToString();
                string GiaTien = dt.Rows[i]["GiaTien"].ToString();
                string TenDanhMuc = dt.Rows[i]["TenDanhMuc"].ToString();
                string v = dt.Rows[i]["hinhAnh"].ToString();
                if (v != "")
                {
                    System.Drawing.Image img = ConvertStringtoImage(dt.Rows[i]["hinhAnh"].ToString());
                    dtgv.Rows.Add(new object[] { img, MaDoUong, TenDoUong, MoTa, GiaTien, MaDanhMuc,TenDanhMuc });
                }
                else
                {
                    dtgv.Rows.Add(new object[] { null, MaDoUong, TenDoUong, MoTa, GiaTien, MaDanhMuc, TenDanhMuc });
                    foreach (var column in dtgv.Columns)
                    {
                        if (column is DataGridViewImageColumn)
                            (column as DataGridViewImageColumn).DefaultCellStyle.NullValue = null;
                    }
                }
            }
            foreach (DataGridViewRow r in dtgv.Rows)
                r.Height = 60;
            frmMain.SetupDataGridView(dtgv);
        }
        public System.Drawing.Image ConvertStringtoImage(string commands)
        {
            byte[] photoarray = Convert.FromBase64String(commands);
            MemoryStream ms = new MemoryStream(photoarray, 0, photoarray.Length);
            ms.Write(photoarray, 0, photoarray.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
            return image;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            save = true;
            frmDoUong2 frm = new frmDoUong2();
            frm.Text = "Thêm mới đồ uống";
            frm.ShowDialog();
            GetAll();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dtgv.Rows.Count == 0)
            {
                return;
            }
            DataGridViewRow row = this.dtgv.Rows[dtgv.CurrentCell.RowIndex];
            save = false;
            frmDoUong2 frm = new frmDoUong2();
            frm.txtMaDoUong.Text = row.Cells[1].Value.ToString();
            frm.txtTenDoUong.Text = row.Cells[2].Value.ToString();
            frm.txtMoTa.Text = row.Cells[3].Value.ToString();
            frm.txtGiaTien.Text = row.Cells[4].Value.ToString();
            MaDanhMuc = row.Cells[5].Value.ToString();
          
            frm.Text = "Sửa thông tin đồ uống";
            frm.ShowDialog();
            GetAll();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dtgv.Rows.Count == 0)
            {
                return;
            }
            DialogResult dr = MessageBox.Show("Có chắc chắn xóa dòng dữ liệu này không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    db.Delete(dtgv.Rows[dtgv.CurrentCell.RowIndex].Cells[1].Value.ToString());
                    MessageBox.Show("Xóa thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    GetAll();
                }
                catch (Exception)
                {
                    MessageBox.Show("Dữ liệu đã phát sinh, không được xóa");
                    return;
                }
            }
            else
                return;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            GetAll();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = true;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            for (int i = 1; i < dtgv.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dtgv.Columns[i - 1].HeaderText;
            }
            for (int i = 0; i < dtgv.Rows.Count; i++)
            {
                for (int j = 0; j < dtgv.Columns.Count; j++)
                {
                    if (dtgv.Rows[i].Cells[j].Value != null)
                    {
                        worksheet.Cells[i + 2, j + 1] = dtgv.Rows[i].Cells[j].Value.ToString();
                    }
                    else
                    {
                        worksheet.Cells[i + 2, j + 1] = "";
                    }
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            GetAll();
        }
    }
}
