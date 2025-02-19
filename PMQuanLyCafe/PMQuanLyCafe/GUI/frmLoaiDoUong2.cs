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
    public partial class frmLoaiDoUong2 : Form
    {
        public frmLoaiDoUong2()
        {
            InitializeComponent();
        }
        LoaiDoUong_DAO db = new LoaiDoUong_DAO();
        private void frmLoaiDoUong2_Load(object sender, EventArgs e)
        {
            if (frmLoaiDoUong.save == true)
            {
                txtMaDanhMuc.Select();
                txtMaDanhMuc.Text = "";
                txtTenDanhMuc.Text = "";
            }
            else
            {
                txtMaDanhMuc.Select();
                txtMaDanhMuc.Enabled = false;
            }    
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaDanhMuc.Text == "")
            {
                MessageBox.Show("Mã loại không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaDanhMuc.Focus();
                return;
            }
            if (txtTenDanhMuc.Text == "")
            {
                MessageBox.Show("Tên loại không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenDanhMuc.Focus();
                return;
            }
            if (frmLoaiDoUong.save == true)
            {
                try
                {
                    db.Insert(txtMaDanhMuc.Text.Trim(), txtTenDanhMuc.Text.Trim());
                    MessageBox.Show("Thêm thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Mã loại đồ uống đã tồn tại ,hãy tạo mã khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenDanhMuc.Focus();
                    return;
                }
            }
            else
            {
                try
                {
                    db.Update(txtMaDanhMuc.Text.Trim(), txtTenDanhMuc.Text.Trim());
                    MessageBox.Show("Sửa thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Mã loại đồ uống đã tồn tại ,hãy tạo mã khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenDanhMuc.Focus();
                    return;
                }
            }    
        }
    }
}
