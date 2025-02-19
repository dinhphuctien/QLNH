using PMQuanLyCafe.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMQuanLyCafe.GUI
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        BanHang_DAO db = new BanHang_DAO();
        public static string MaHDThanhToan;
        public static string MaBanThanhToan;
        public static string TongTienInHD;
        public static string ViTriBanInHD;
        public static string MaDoUongTonKho;
        private void btnLDU_Click(object sender, EventArgs e)
        {
            frmLoaiDoUong frm = new frmLoaiDoUong();
            frm.ShowDialog();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            txtMaNV.Text = frmDangNhap.MaDangNhap;
            dtNgayOrder.Text = DateTime.Now.ToString("dd/MM/yyyy");
            DanhSachBan();
            DanhSachDoUong();
            DanhSachChiTietHoaDonTheoBan("1000");
            if(frmDangNhap.Role == "Nhân viên")
            {
                btnNhanVien.Enabled = false;
                btnDanhMuc.Enabled = true;
                btnLDU.Enabled = false;
                btnDoUong.Enabled = false;
                btnBan.Enabled = false;
                btnDoanhThuNgay.Enabled = false;
                btnThongKeDoanhThuNV.Enabled = false;
                btnThongKeBanHang.Enabled = false;
            }    
        }
        public void DanhSachBan()
        {
            DataTable dtBan = new DataTable();
            listViewBan.Items.Clear();
            dtBan = db.BanTrong();
            for (int i = 0; i < dtBan.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem(dtBan.Rows[i]["TenBan"].ToString());
                ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem(item, dtBan.Rows[i][0].ToString());
                item.ImageIndex = 0;
                item.SubItems.Add(subItem);
                listViewBan.Items.Add(item);
            }
            dtBan = db.BanCoNguoi();
            for (int i = 0; i < dtBan.Rows.Count; i++)
            {
                ListViewItem item = new ListViewItem(dtBan.Rows[i]["TenBan"].ToString());
                ListViewItem.ListViewSubItem subItem = new ListViewItem.ListViewSubItem(item, dtBan.Rows[i][0].ToString());
                item.ImageIndex = 1;
                item.SubItems.Add(subItem);
                listViewBan.Items.Add(item);
            }
        }
        private void DanhSachDoUong()
        {
            DataTable dt = new DataTable();
            dt = db.DanSachDoUong(txtTenDoUong.Text);
            dtgvDoUong.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string MaDoUong = dt.Rows[i]["MaDoUong"].ToString();
                string TenDoUong = dt.Rows[i]["TenDoUong"].ToString();
                string GiaTien = dt.Rows[i]["GiaTien"].ToString();
                string v = dt.Rows[i]["hinhAnh"].ToString();
                if (v != "")
                {
                    System.Drawing.Image img = ConvertStringtoImage(dt.Rows[i]["hinhAnh"].ToString());
                    dtgvDoUong.Rows.Add(new object[] { img, MaDoUong, TenDoUong, GiaTien });
                }
                else
                {
                    dtgvDoUong.Rows.Add(new object[] { null, MaDoUong, TenDoUong, GiaTien });
                    foreach (var column in dtgvDoUong.Columns)
                    {
                        if (column is DataGridViewImageColumn)
                            (column as DataGridViewImageColumn).DefaultCellStyle.NullValue = null;
                    }
                }
            }
            foreach (DataGridViewRow r in dtgvDoUong.Rows)
                r.Height = 60;
            frmMain.SetupDataGridView(dtgvDoUong);
        }
        public System.Drawing.Image ConvertStringtoImage(string commands)
        {
            byte[] photoarray = Convert.FromBase64String(commands);
            MemoryStream ms = new MemoryStream(photoarray, 0, photoarray.Length);
            ms.Write(photoarray, 0, photoarray.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
            return image;
        }
        private void DanhSachChiTietHoaDonTheoBan(string MaHD)
        {
            dtgvHoaDon.DataSource = db.DanSachChiTietHoaDonTheoBan(MaHD);
            dtgvHoaDon.Columns[0].Visible = false;
            dtgvHoaDon.Columns[1].Visible = false;
            dtgvHoaDon.Columns[2].HeaderText = "Mã đồ uống";
            dtgvHoaDon.Columns[3].HeaderText = "Tên đồ uống";
            dtgvHoaDon.Columns[4].HeaderText = "Đơn giá";
            dtgvHoaDon.Columns[5].HeaderText = "Số lượng";
            dtgvHoaDon.Columns[6].HeaderText = "Thành tiền";
            frmMain.SetupDataGridView(dtgvHoaDon);
            TongTien();

        }
        public void TongTien()
        {
            if (dtgvHoaDon.Rows.Count == 0)
            {
                lblTongTien.Text = "0 VNĐ";
                return;
            }
            int tien = dtgvHoaDon.Rows.Count;
            decimal thanhtien = 0;
            for (int i = 0; i < tien; i++)
            {
                thanhtien += decimal.Parse(dtgvHoaDon.Rows[i].Cells["ThanhTien"].Value.ToString());
            }
            lblTongTien.Text = thanhtien.ToString("#,###") + " VNĐ";
            if (lblTongTien.Text == " VNĐ")
            {
                lblTongTien.Text = "0 VNĐ";
            }
        }
        public static void SetupDataGridView(DataGridView dgvMain)
        {
            dgvMain.AllowUserToAddRows = false;
            dgvMain.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvMain.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvMain.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvMain.ColumnHeadersHeight = 30;
            dgvMain.ReadOnly = false;
            dgvMain.ColumnHeadersDefaultCellStyle.BackColor = Color.Blue;
            dgvMain.EnableHeadersVisualStyles = false;
            dgvMain.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvMain.ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", 9, FontStyle.Bold);
        }

        private void btnBan_Click(object sender, EventArgs e)
        {
            frmBan frm = new frmBan();
            frm.ShowDialog();
            DanhSachBan();
        }

        private void quảnLýKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKhachHang frm = new frmKhachHang();
            frm.ShowDialog();
        }

        private void btnNL_Click(object sender, EventArgs e)
        {
            
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            frmNhanVien frm = new frmNhanVien();
            frm.ShowDialog();
        }

        private void btnDX_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Bạn có muốn thoát chương trình ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Close();
            }
            else
                return;
        }

        private void btnDoUong_Click(object sender, EventArgs e)
        {
            frmDoUong frm = new frmDoUong();
            frm.ShowDialog();
            DanhSachDoUong();
        }

        private void listViewBan_Click(object sender, EventArgs e)
        {
            btnBanDaChon.Text = listViewBan.SelectedItems[0].SubItems[0].Text;
            DataTable dtMB = new DataTable();
            dtMB = db.LayMaBanTheoTen(btnBanDaChon.Text.Trim());
            DanhSachChiTietHoaDonTheoBan(dtMB.Rows[0][0].ToString());
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (btnBanDaChon.Text == "Chưa chọn bàn")
            {
                MessageBox.Show("Bạn chưa chọn bàn để gọi món", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(dtgvDoUong.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có đồ uống để lập hóa đơn, vào danh mục đồ uống để tạo mới", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (nmSoLuong.Value <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataTable dtSLT = new DataTable();
            dtSLT = db.KiemTraTonKho(dtgvDoUong.Rows[dtgvDoUong.CurrentCell.RowIndex].Cells[1].Value.ToString(),dtNgayOrder.Value.ToString("yyyyMMdd"));
            if(dtSLT.Rows.Count == 0)
            {
                MessageBox.Show("Đồ uống này chưa nhập tồn kho hôm nay, vui lòng nhập tồn kho trước khi bán hàng", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (int.Parse(dtSLT.Rows[0][0].ToString()) < nmSoLuong.Value)
            {
                MessageBox.Show("Đồ uống không đủ số lượng tồn để bán", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (dtgvHoaDon.Rows.Count ==0)
            {
                MaDoUongTonKho = dtgvDoUong.Rows[dtgvDoUong.CurrentCell.RowIndex].Cells[1].Value.ToString();
                string MaHD = "HD" + DateTime.Now.ToString("yyyyMMddhhmmss");
                string MaCTHD = "CTHD" + DateTime.Now.ToString("yyyyMMddhhmmss");
                string MaDoUong = dtgvDoUong.Rows[dtgvDoUong.CurrentCell.RowIndex].Cells[1].Value.ToString();
                string TenDoUong = dtgvDoUong.Rows[dtgvDoUong.CurrentCell.RowIndex].Cells[2].Value.ToString();
                DataTable dtMB = new DataTable();
                dtMB = db.LayMaBanTheoTen(btnBanDaChon.Text.Trim());
                db.ThemtblPhieuOrder(MaHD, dtNgayOrder.Value.ToString("yyyyMMdd"),txtMaNV.Text, dtMB.Rows[0][0].ToString(),"");
                db.ThemtblChiPhieuOrder(MaCTHD, MaHD, MaDoUong,TenDoUong, nmSoLuong.Value.ToString());
                db.UpdateThanhBanCoNguoi(dtMB.Rows[0][0].ToString());
                MessageBox.Show("Thêm thành công");

                db.TruSoLuongTon(dtNgayOrder.Value.ToString("yyyyMMdd"), MaDoUongTonKho, nmSoLuong.Value.ToString());

                DanhSachBan();
                DataTable dtTB = new DataTable();
                dtTB = db.LayTenBanTheoMa(dtMB.Rows[0][0].ToString());
                btnBanDaChon.Text = dtTB.Rows[0][0].ToString();
                DanhSachChiTietHoaDonTheoBan(dtMB.Rows[0][0].ToString());
            }    
            else
            {
                MaDoUongTonKho = dtgvDoUong.Rows[dtgvDoUong.CurrentCell.RowIndex].Cells[1].Value.ToString();
                string MaHD = dtgvHoaDon.Rows[dtgvHoaDon.CurrentCell.RowIndex].Cells[0].Value.ToString();
                string MaCTHD = "CTHD" + DateTime.Now.ToString("yyyyMMddhhmmss");
                string MaDoUong = dtgvDoUong.Rows[dtgvDoUong.CurrentCell.RowIndex].Cells[1].Value.ToString();
                string TenDoUong = dtgvDoUong.Rows[dtgvDoUong.CurrentCell.RowIndex].Cells[2].Value.ToString();
                DataTable dtMB = new DataTable();
                dtMB = db.LayMaBanTheoTen(btnBanDaChon.Text.Trim());
                
                db.ThemtblChiPhieuOrder(MaCTHD, MaHD, MaDoUong, TenDoUong, nmSoLuong.Value.ToString());
                MessageBox.Show("Thêm thành công");
                db.TruSoLuongTon(dtNgayOrder.Value.ToString("yyyyMMdd"), MaDoUongTonKho, nmSoLuong.Value.ToString());
                DanhSachBan();
                DataTable dtTB = new DataTable();
                dtTB = db.LayTenBanTheoMa(dtMB.Rows[0][0].ToString());
                btnBanDaChon.Text = dtTB.Rows[0][0].ToString();
                DanhSachChiTietHoaDonTheoBan(dtMB.Rows[0][0].ToString());
            }    
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if(dtgvHoaDon.Rows.Count == 0)
            {
                return;
            }    
            string MaHD = dtgvHoaDon.Rows[dtgvHoaDon.CurrentCell.RowIndex].Cells[0].Value.ToString();
            string MaCTHD = dtgvHoaDon.Rows[dtgvHoaDon.CurrentCell.RowIndex].Cells[1].Value.ToString();
            DataTable dtMB = new DataTable();
            dtMB = db.LayMaBanTheoTen(btnBanDaChon.Text.Trim());
            db.XoaDoUongTrongChiTietHoaDon(MaCTHD);  
            MessageBox.Show("Xóa thành công");
            db.CongSoLuongTon(dtNgayOrder.Value.ToString("yyyyMMdd"), dtgvHoaDon.Rows[dtgvHoaDon.CurrentCell.RowIndex].Cells[2].Value.ToString(), nmSoLuong.Value.ToString());
            DanhSachChiTietHoaDonTheoBan(dtMB.Rows[0][0].ToString());
            if (dtgvHoaDon.Rows.Count == 0)
            {
                db.XoatblPhieuOrder(MaHD);
                db.UpdateThanhBanTrong(dtMB.Rows[0][0].ToString());
            }
            DanhSachBan();
            DataTable dtTB = new DataTable();
            dtTB = db.LayTenBanTheoMa(dtMB.Rows[0][0].ToString());
            btnBanDaChon.Text = dtTB.Rows[0][0].ToString();
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (dtgvHoaDon.Rows.Count == 0)
            {
                return;
            }
            DataTable dtMB2 = new DataTable();
            dtMB2 = db.LayViTriBanTheoTen(btnBanDaChon.Text.Trim());

            ViTriBanInHD = btnBanDaChon.Text + " " + dtMB2.Rows[0][0].ToString();

            TongTienInHD = lblTongTien.Text;
            MaHDThanhToan = dtgvHoaDon.Rows[dtgvHoaDon.CurrentCell.RowIndex].Cells[0].Value.ToString();
            DataTable dtMB = new DataTable();
            dtMB = db.LayMaBanTheoTen(btnBanDaChon.Text.Trim());
            MaBanThanhToan = dtMB.Rows[0][0].ToString();
            frmKhachHangThanhToan frm = new frmKhachHangThanhToan();
            frm.ShowDialog();
            DanhSachBan();
            DataTable dtTB = new DataTable();
            dtTB = db.LayTenBanTheoMa(dtMB.Rows[0][0].ToString());
            btnBanDaChon.Text = dtTB.Rows[0][0].ToString();
            DanhSachChiTietHoaDonTheoBan(dtMB.Rows[0][0].ToString());
        }

        private void btnDoanhThuNgay_Click(object sender, EventArgs e)
        {
            frmDoanhThuTheoNgay frm = new frmDoanhThuTheoNgay();
            frm.ShowDialog();
        }

        private void btnThongKeDoanhThuNV_Click(object sender, EventArgs e)
        {
            frmDoanhThuTheoNhanVien frm = new frmDoanhThuTheoNhanVien();
            frm.ShowDialog();
        }

        private void btnThongKeBanHang_Click(object sender, EventArgs e)
        {
            frmThongKeBanHang frm = new frmThongKeBanHang();
            frm.ShowDialog();
        }

        private void btnLichSuHoaDon_Click(object sender, EventArgs e)
        {
            frmLichSuHoaDon frm = new frmLichSuHoaDon();
            frm.ShowDialog();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (dtgvHoaDon.Rows.Count == 0)
            {
                return;
            }
            DataTable dtMB = new DataTable();
            dtMB = db.LayViTriBanTheoTen(btnBanDaChon.Text.Trim());

            ViTriBanInHD = btnBanDaChon.Text + " " + dtMB.Rows[0][0].ToString();
            TongTienInHD = lblTongTien.Text;
            MaHDThanhToan = dtgvHoaDon.Rows[dtgvHoaDon.CurrentCell.RowIndex].Cells[0].Value.ToString();
            frmInBill frm = new frmInBill();
            frm.Show();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            DanhSachDoUong();
        }

        private void btnCT_Click(object sender, EventArgs e)
        {
            
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmThongTinCaNhan frm = new frmThongTinCaNhan();
            frm.ShowDialog();
        }

        private void nguyênLiệuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmNguyenLieu frm = new frmNguyenLieu();
            frm.ShowDialog();
        }

        private void côngThứcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCongThuc frm = new frmCongThuc();
            frm.ShowDialog();
        }

        private void btnNhapTon_Click(object sender, EventArgs e)
        {
            frmTonKho frm = new frmTonKho();
            frm.ShowDialog();
        }
    }
}
