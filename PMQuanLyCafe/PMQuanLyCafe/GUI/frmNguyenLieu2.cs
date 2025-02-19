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
    public partial class frmNguyenLieu2 : Form
    {
        public frmNguyenLieu2()
        {
            InitializeComponent();
        }
        NguyenLieu_DAO db = new NguyenLieu_DAO();
        private void frmNguyenLieu2_Load(object sender, EventArgs e)
        {
            if (frmNguyenLieu.save == true)
            {
                txtMaNguyenLieu.Select();
                txtMaNguyenLieu.Text = "";
                txtTenNguyenLieu.Text = "";
                txtSoLuong.Text = "";
            }
            else
            {
                txtMaNguyenLieu.Select();
                txtMaNguyenLieu.Enabled = false;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaNguyenLieu.Text == "")
            {
                MessageBox.Show("Mã nguyên liệu không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaNguyenLieu.Focus();
                return;
            }
            if (txtTenNguyenLieu.Text == "")
            {
                MessageBox.Show("Tên nguyên liệu không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenNguyenLieu.Focus();
                return;
            }
            if (txtSoLuong.Text == "")
            {
                MessageBox.Show("Số lượng không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoLuong.Focus();
                return;
            }
            if (frmNguyenLieu.save == true)
            {
                try
                {
                    db.Insert(txtMaNguyenLieu.Text.Trim(), txtTenNguyenLieu.Text.Trim(),txtSoLuong.Text);
                    MessageBox.Show("Thêm thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Mã nguyên liệu đã tồn tại ,hãy tạo mã khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenNguyenLieu.Focus();
                    return;
                }
            }
            else
            {
                try
                {
                    db.Update(txtMaNguyenLieu.Text.Trim(), txtTenNguyenLieu.Text.Trim(), txtSoLuong.Text);
                    MessageBox.Show("Sửa thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Mã nguyên liệu đã tồn tại ,hãy tạo mã khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenNguyenLieu.Focus();
                    return;
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
