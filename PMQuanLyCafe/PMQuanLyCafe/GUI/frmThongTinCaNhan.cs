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
    public partial class frmThongTinCaNhan : Form
    {
        public frmThongTinCaNhan()
        {
            InitializeComponent();
        }
        ThongTinCaNhan_DAO db = new ThongTinCaNhan_DAO();
        
        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmThongTinCaNhan_Load(object sender, EventArgs e)
        {
            DataTable dt = db.ThongTinCaNhan(frmDangNhap.MaDangNhap);
            txtMaNV.Text = dt.Rows[0][0].ToString();
            txtTenNV.Text = dt.Rows[0][1].ToString();
            txtGioiTinh.Text = dt.Rows[0][2].ToString();
            txtSDT.Text = dt.Rows[0][3].ToString();
            dtNgayVaoLam.Text = dt.Rows[0][4].ToString();
            txtTenDangNhap.Text = dt.Rows[0][5].ToString();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtmat_khau_cu.Text == "")
            {
                MessageBox.Show("Mật khẩu cũ không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtmat_khau_cu.Focus();
                return;
            }
            if (txtmat_khau_moi.Text == "")
            {
                MessageBox.Show("Mật khẩu mới không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtmat_khau_moi.Focus();
                return;
            }
            if ((txtmat_khau_moi.Text != txtmat_khau_nhap_lai.Text))
            {
                MessageBox.Show("Nhập lại mật khẩu không trùng khớp", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtmat_khau_nhap_lai.Focus();
                return;
            }
            if (txtmat_khau_cu.Text != frmDangNhap.MatKhau)
            {
                MessageBox.Show("Mật khẩu cũ không chính xác", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtmat_khau_cu.Focus();
                return;
            }
            db.DoiMatKhau(txtmat_khau_moi.Text.Trim(), txtMaNV.Text.Trim());
            MessageBox.Show("Đổi mật khẩu thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            this.Close();
        }
    }
}
