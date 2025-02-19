using PMQuanLyCafe.DAO;
using PMQuanLyCafe.GUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMQuanLyCafe
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();
        }
        NhanVien_DAO db = new NhanVien_DAO();
        public static string MaDangNhap;
        public static string MatKhau;
        public static string Role;
        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (txtTenDangNhap.Text == "")
            {
                MessageBox.Show("Mã đăng nhập không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenDangNhap.Focus();
                return;
            }
            if (txtMatKhau.Text == "")
            {
                MessageBox.Show("Mật khẩu không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMatKhau.Focus();
                return;
            }
            DataTable dt = new DataTable();
            dt = db.DangNhap(txtTenDangNhap.Text.Trim(), txtMatKhau.Text.Trim(),cboQuyen.Text);
            if (dt == null || dt.Rows.Count > 0)
            {
                Role = cboQuyen.Text;
                MaDangNhap = txtTenDangNhap.Text;
                MatKhau = txtMatKhau.Text;
                frmMain frm = new frmMain();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không đúng tên người dùng hoặc mật khẩu", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenDangNhap.Focus();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát chương trình ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
            else
                return;
        }
        private void DanhSachQuyen()
        {
            DataTable dataTable = new DataTable();
            cboQuyen.Items.Clear();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Rows.Add("Nhân viên");
            dataTable.Rows.Add("Quản trị viên");
            cboQuyen.DataSource = dataTable;
            cboQuyen.DisplayMember = "Name";
            cboQuyen.ValueMember = "Name";
        }
        private void frmDangNhap_Load(object sender, EventArgs e)
        {
            DanhSachQuyen();
        }
    }
}
