using PMQuanLyCafe.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMQuanLyCafe.GUI
{
    public partial class frmNguyenLieu : Form
    {
        public frmNguyenLieu()
        {
            InitializeComponent();
        }
        NguyenLieu_DAO db = new NguyenLieu_DAO();
        public static Boolean save = true;
        private void frmNguyenLieu_Load(object sender, EventArgs e)
        {
            GetAll();
        }
        private void GetAll()
        {
            dtgv.DataSource = db.GetAll(txtSearch.Text);
            dtgv.Columns[0].HeaderText = "Mã nguyên liệu";
            dtgv.Columns[1].HeaderText = "Tên nguyên liệu";
            dtgv.Columns[2].HeaderText = "Số lượng";
            frmMain.SetupDataGridView(dtgv);
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            save = true;
            frmNguyenLieu2 frm = new frmNguyenLieu2();
            frm.Text = "Thêm mới nguyên liệu";
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
            frmNguyenLieu2 frm = new frmNguyenLieu2();
            frm.txtMaNguyenLieu.Text = row.Cells[0].Value.ToString();
            frm.txtTenNguyenLieu.Text = row.Cells[1].Value.ToString();
            frm.txtSoLuong.Text = row.Cells[2].Value.ToString();
            frm.Text = "Sửa thông tin nguyên liệu";
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
