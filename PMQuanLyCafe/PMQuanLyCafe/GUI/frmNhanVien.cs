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
    public partial class frmNhanVien : Form
    {
        public frmNhanVien()
        {
            InitializeComponent();
        }
        NhanVien_DAO db = new NhanVien_DAO();
        public static Boolean save = true;
        public static string GioiTinh;
        public static DateTime NgayVaoLam;
        public static string role;
        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            GetAll();
        }
        private void GetAll()
        {
            dtgv.DataSource = db.GetAll(txtSearch.Text);
            dtgv.Columns[0].HeaderText = "Mã NV";
            dtgv.Columns[1].HeaderText = "Tên NV";
            dtgv.Columns[2].HeaderText = "Giới tính";
            dtgv.Columns[3].HeaderText = "SĐT";
            dtgv.Columns[4].HeaderText = "Ngày vào làm";
            dtgv.Columns[5].HeaderText = "Mật khẩu";
            dtgv.Columns[6].HeaderText = "Quyền";
            frmMain.SetupDataGridView(dtgv);
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            save = true;
            frmNhanVien2 frm = new frmNhanVien2();
            frm.Text = "Thêm mới nhân viên";
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
            frmNhanVien2 frm = new frmNhanVien2();
            frm.txtMaNV.Text = row.Cells[0].Value.ToString();
            frm.txtTenNV.Text = row.Cells[1].Value.ToString();
            GioiTinh = row.Cells[2].Value.ToString();
            frm.txtSDT.Text = row.Cells[3].Value.ToString();
            NgayVaoLam = DateTime.Parse(row.Cells[4].Value.ToString());
            frm.txtMatKhau.Text = row.Cells[5].Value.ToString();
            role = row.Cells[6].Value.ToString();
            frm.Text = "Sửa thông tin nhân viên";
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
                    db.Delete(dtgv.Rows[dtgv.CurrentCell.RowIndex].Cells[0].Value.ToString());
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
