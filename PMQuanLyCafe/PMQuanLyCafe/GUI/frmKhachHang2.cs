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
    public partial class frmKhachHang2 : Form
    {
        public frmKhachHang2()
        {
            InitializeComponent();
        }
        KhachHang_DAO db = new KhachHang_DAO();
        private void frmKhachHang2_Load(object sender, EventArgs e)
        {
            if (frmKhachHang.save == true)
            {
                txtMaKH.Select();
                txtMaKH.Text = "KH" + DateTime.Now.ToString("yyyyMMddhhmmss");
                txtTenKH.Text = "";
                txtSDT.Text = "";
                DanhSachGioiTinh();
            }
            else
            {
                DanhSachGioiTinh();
                txtMaKH.Select();
                txtMaKH.Enabled = false;
                cboGioiTinh.Text = frmKhachHang.GioiTinh;
                
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
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaKH.Text == "")
            {
                MessageBox.Show("Mã KH không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaKH.Focus();
                return;
            }
            if (txtTenKH.Text == "")
            {
                MessageBox.Show("Tên KH không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenKH.Focus();
                return;
            }
            if (frmKhachHang.save == true)
            {
                try
                {
                    db.Insert(txtMaKH.Text.Trim(), txtTenKH.Text.Trim(),cboGioiTinh.Text,txtSDT.Text);
                    MessageBox.Show("Thêm thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Mã khách hàng đã tồn tại ,hãy tạo mã khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenKH.Focus();
                    return;
                }
            }
            else
            {
                try
                {
                    db.Update(txtMaKH.Text.Trim(), txtTenKH.Text.Trim(), cboGioiTinh.Text, txtSDT.Text);
                    MessageBox.Show("Sửa thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Mã khách hàng đã tồn tại ,hãy tạo mã khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenKH.Focus();
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
