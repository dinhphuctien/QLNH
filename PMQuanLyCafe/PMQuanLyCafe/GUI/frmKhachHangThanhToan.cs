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
    public partial class frmKhachHangThanhToan : Form
    {
        public frmKhachHangThanhToan()
        {
            InitializeComponent();
        }
        KhachHang_DAO dbKH = new KhachHang_DAO();
        BanHang_DAO db = new BanHang_DAO();
        private void frmKhachHangThanhToan_Load(object sender, EventArgs e)
        {
            GeSearch();
        }
        private void GeSearch()
        {
            dtgv.DataSource = dbKH.GeSearch(txtMaKH.Text,txtTenKH.Text,txtSDT.Text);
            dtgv.Columns[0].HeaderText = "Mã KH";
            dtgv.Columns[1].HeaderText = "Tên KH";
            dtgv.Columns[2].HeaderText = "Giới tính";
            dtgv.Columns[3].HeaderText = "Số điện thoại";
            frmMain.SetupDataGridView(dtgv);
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            GeSearch();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (dtgv.Rows.Count == 0)
            {
                return;
            }
            if (txtVAT.Text == "")
            {
                MessageBox.Show("VAT không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtVAT.Focus();
            }
            if (rdCK.Checked)
            {
                frmMain.PTTTInHD = "Chuyển khoản";
            }
            else if (rdThe.Checked)
            {
                frmMain.PTTTInHD = "Thẻ";
            }
            else
            {
                frmMain.PTTTInHD = "Tiền mặt";
            }
            frmMain.VATInHD = txtVAT.Text + " %";
            string HDD;
            if(chkHDD.Checked)
            {
                HDD = "1";
            }    
            else
            {
                HDD= "0";
            }    
            DialogResult dr = MessageBox.Show("Có chắc chắn thanh toán hóa đơn này không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    db.UpdateTrangThaiThanhToan(frmMain.MaHDThanhToan, dtgv.Rows[dtgv.CurrentCell.RowIndex].Cells[0].Value.ToString());
                    db.UpdateThanhBanTrong(frmMain.MaBanThanhToan);
                    db.InserttblPhieuThanhToan(frmMain.MaHDThanhToan);
                    db.UpdateHoaDon(frmMain.MaHDThanhToan,frmMain.PTTTInHD, frmMain.VATInHD, HDD);
                    MessageBox.Show("Thanh toán thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    frmInBill frm = new frmInBill();
                    frm.ShowDialog();
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Dữ liệu đã phát sinh, không thanh toán được");
                    return;
                }
            }
            else
                return;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            frmKhachHang frm = new frmKhachHang();
            frm.ShowDialog();
        }
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            if(txtVAT.Text == "")
            {
                MessageBox.Show("VAT không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtVAT.Focus();
            }    
            if(rdCK.Checked)
            {
                frmMain.PTTTInHD = "Chuyển khoản";
            }
            else if(rdThe.Checked)
            {
                frmMain.PTTTInHD = "Thẻ";
            }
            else
            {
                frmMain.PTTTInHD = "Tiền mặt";
            }
            frmMain.VATInHD = txtVAT.Text + " %";
            frmMain.TongTienInHD = frmMain.TongTienHD;
            string TongTien = new string(frmMain.TongTienInHD.Where(char.IsDigit).ToArray()); 
            if (chkHDD.Checked && frmMain.PTTTInHD == "Thẻ")
            {
                decimal total = decimal.Parse(TongTien) + (decimal.Parse(TongTien) * 5) / 100;
                frmMain.TongTienInHD = total.ToString("N0") + " VNĐ";
            }
            else
            {
                decimal total = decimal.Parse(TongTien) + (decimal.Parse(TongTien) * decimal.Parse(txtVAT.Text)) / 100;
                frmMain.TongTienInHD = total.ToString("N0") + " VNĐ";
            }
            frmInBill frm = new frmInBill();
            frm.ShowDialog();
        }

        private void txtVAT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }
    }
}
