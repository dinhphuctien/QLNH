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
    public partial class frmTonKho2 : Form
    {
        public frmTonKho2()
        {
            InitializeComponent();
        }
        TonKho_DAO db = new TonKho_DAO();
        DoUong_DAO dbDU = new DoUong_DAO();
        private void txtSoLuongTon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        private void DanhSachDoUong()
        {
            DataTable dt = dbDU.GetAll("");
            cboMaDoUong.DataSource = dt;
            cboMaDoUong.DisplayMember = "TenDoUong";
            cboMaDoUong.ValueMember = "MaDoUong";
        }
        private void frmTonKho2_Load(object sender, EventArgs e)
        {
            if (frmTonKho.save == true)
            {
                txtSoLuongTon.Select();
                dtNgayNhap.Text = DateTime.Now.ToString("dd/MM/yyyy"); ;
                txtSoLuongTon.Text = "";
                DanhSachDoUong();
            }
            else
            {
                DanhSachDoUong();
                dtNgayNhap.Select();
                dtNgayNhap.Enabled = false;
                cboMaDoUong.Enabled = false;
                dtNgayNhap.Text = frmTonKho.NgayNhap;
                cboMaDoUong.SelectedValue = frmTonKho.MaDoUong;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboMaDoUong.Text == "")
            {
                MessageBox.Show("Mã đồ uống không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboMaDoUong.Focus();
                return;
            }
            if (txtSoLuongTon.Text == "")
            {
                MessageBox.Show("Số lượng tồn không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoLuongTon.Focus();
                return;
            }
            if (frmTonKho.save == true)
            {
                try
                {
                    db.Insert(dtNgayNhap.Value.ToString("yyyyMMdd"), cboMaDoUong.SelectedValue.ToString(), txtSoLuongTon.Text);
                    MessageBox.Show("Thêm thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Đồ uống này đã nhập tồn cho ngày này, hãy chọn mặt hàng khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSoLuongTon.Focus();
                    return;
                }
            }
            else
            {
                try
                {
                    db.Update(dtNgayNhap.Value, cboMaDoUong.SelectedValue.ToString(), txtSoLuongTon.Text);
                    MessageBox.Show("Sửa thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Đồ uống này đã nhập tồn cho ngày này, hãy chọn mặt hàng khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
