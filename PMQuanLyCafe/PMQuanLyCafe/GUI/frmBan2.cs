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
    public partial class frmBan2 : Form
    {
        public frmBan2()
        {
            InitializeComponent();
        }
        Ban_DAO db = new Ban_DAO();
        private void frmBan2_Load(object sender, EventArgs e)
        {
            if (frmBan.save == true)
            {
                txtSoBan.Select();
                txtSoBan.Text = "";
                txtTenBan.Text = "";
                DanhSachViTri();
            }
            else
            {
                DanhSachViTri();
                txtSoBan.Select();
                txtSoBan.Enabled = false;
                cboViTri.Text = frmBan.ViTri;
            }
        }
        private void DanhSachViTri()
        {
            DataTable dataTable = new DataTable();
            cboViTri.Items.Clear();
            dataTable.Columns.Add("Name", typeof(string));
            dataTable.Rows.Add("Tầng 1");
            dataTable.Rows.Add("Tầng 2");
            dataTable.Rows.Add("Tầng 3");
            cboViTri.DataSource = dataTable;
            cboViTri.DisplayMember = "Name";
            cboViTri.ValueMember = "Name";
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtSoBan.Text == "")
            {
                MessageBox.Show("Số bàn không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoBan.Focus();
                return;
            }
            if (txtTenBan.Text == "")
            {
                MessageBox.Show("Tên bàn không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenBan.Focus();
                return;
            }
            if (frmBan.save == true)
            {
                try
                {
                    db.Insert(txtSoBan.Text.Trim(), txtTenBan.Text.Trim(),cboViTri.Text);
                    MessageBox.Show("Thêm thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Mã bàn đã tồn tại ,hãy tạo mã khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSoBan.Focus();
                    return;
                }
            }
            else
            {
                try
                {
                    db.Update(txtSoBan.Text.Trim(), txtTenBan.Text.Trim(), cboViTri.Text);
                    MessageBox.Show("Sửa thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Mã bàn đã tồn tại ,hãy tạo mã khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSoBan.Focus();
                    return;
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
