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
            DialogResult dr = MessageBox.Show("Có chắc chắn thanh toán hóa đơn này không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    db.UpdateTrangThaiThanhToan(frmMain.MaHDThanhToan, dtgv.Rows[dtgv.CurrentCell.RowIndex].Cells[0].Value.ToString());
                    db.UpdateThanhBanTrong(frmMain.MaBanThanhToan);
                    db.InserttblPhieuThanhToan(frmMain.MaHDThanhToan);
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
    }
}
