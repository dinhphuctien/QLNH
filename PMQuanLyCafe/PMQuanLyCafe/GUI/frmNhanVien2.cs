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
    public partial class frmNhanVien2 : Form
    {
        public frmNhanVien2()
        {
            InitializeComponent();
        }
        NhanVien_DAO db = new NhanVien_DAO();
        private void frmNhanVien2_Load(object sender, EventArgs e)
        {
            if (frmNhanVien.save == true)
            {
                txtMaNV.Select();
                txtMaNV.Text = "";
                txtTenNV.Text = "";
                txtSDT.Text = "";
                txtMatKhau.Text = "";
                DanhSachGioiTinh();
                DanhSachQuyen();
            }
            else
            {
                DanhSachGioiTinh();
                DanhSachQuyen();
                txtMaNV.Select();
                txtMaNV.Enabled = false;
                cboGioiTinh.Text = frmNhanVien.GioiTinh;
                dtNgayVaoLam.Value = frmNhanVien.NgayVaoLam;
                cboQuyen.Text = frmNhanVien.role;

        
            }
        }
        private void DanhSachGioiTinh()
        {
            DataTable dataTable = new DataTable();
            cboGioiTinh.Items.Clear();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Rows.Add("Nam");
            dataTable.Rows.Add("Nữ");
            cboGioiTinh.DataSource = dataTable;
            cboGioiTinh.DisplayMember = "Name";
            cboGioiTinh.ValueMember = "Name";
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
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaNV.Text == "")
            {
                MessageBox.Show("Mã nhân viên không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaNV.Focus();
                return;
            }
            if (txtTenNV.Text == "")
            {
                MessageBox.Show("Tên nhân viên không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenNV.Focus();
                return;
            }
            if (txtMatKhau.Text == "")
            {
                MessageBox.Show("Mật khẩu không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMatKhau.Focus();
                return;
            }
            if (frmNhanVien.save == true)
            {
                try
                {
                    db.Insert(txtMaNV.Text.Trim(), txtTenNV.Text.Trim(),cboGioiTinh.Text, txtSDT.Text, dtNgayVaoLam.Value.ToString("yyyyMMdd"), txtMatKhau.Text,cboQuyen.Text);
                    MessageBox.Show("Thêm thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Mã nhân viên đã tồn tại ,hãy tạo mã khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenNV.Focus();
                    return;
                }
            }
            else
            {
                try
                {
                    db.Update(txtMaNV.Text.Trim(), txtTenNV.Text.Trim(), cboGioiTinh.Text, txtSDT.Text, dtNgayVaoLam.Value.ToString("yyyyMMdd"), txtMatKhau.Text, cboQuyen.Text);
                    MessageBox.Show("Sửa thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Mã nhân viên đã tồn tại ,hãy tạo mã khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenNV.Focus();
                    return;
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
