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
    public partial class frmCongThuc : Form
    {
        public frmCongThuc()
        {
            InitializeComponent();
        }
        CongThuc_DAO db = new CongThuc_DAO();
        DoUong_DAO dbDU = new DoUong_DAO();
        NguyenLieu_DAO dbNL = new NguyenLieu_DAO();
        private bool save;
        private bool saveCT;
        private void frmCongThuc_Load(object sender, EventArgs e)
        {
            DanhSachDoUong();
            DanhSachNguyenLieu();
            HienThiCongThuc();
            boolcontrols(true);
            boolcontrolsCT(true);
        }

        private void HienThiCongThuc()
        {
            dtgv.DataSource = db.GetAll(txtSearch.Text);
            dtgv.Columns[0].HeaderText = "Mã công thức";
            dtgv.Columns[1].HeaderText = "Tên công thức";
            dtgv.Columns[2].HeaderText = "Mã đồ uống";
            dtgv.Columns[3].HeaderText = "Nội dung";
            dtgv.Columns[0].Width = 150;
            frmMain.SetupDataGridView(dtgv);
            if (dtgv.Rows.Count == 0)
            {
                txtMaCongThuc.Text = "";
                txtTenCongThuc.Text = "";
                txtNoiDung.Text = "";
                txtMaCongThuccb.Text = "";
                HienThiChiTietCongThuc("");
            }
            else
            {
                var row = this.dtgv.Rows[0];
                txtMaCongThuc.Text = row.Cells[0].Value.ToString();
                txtTenCongThuc.Text = row.Cells[1].Value.ToString();
                cboMaDoUong.SelectedValue = row.Cells[2].Value.ToString();
                txtNoiDung.Text = row.Cells[3].Value.ToString();
                txtMaCongThuccb.Text = txtMaCongThuc.Text;
                HienThiChiTietCongThuc(txtMaCongThuc.Text);
            }
        }
        private void HienThiChiTietCongThuc(string MaCT)
        {
            dtgvCT.DataSource = db.ChiTietCongThuc(MaCT);
            dtgvCT.Columns[0].HeaderText = "Mã chi tiết CT";
            dtgvCT.Columns[1].HeaderText = "Mã công thức";
            dtgvCT.Columns[2].HeaderText = "Mã nguyên liệu";
            dtgvCT.Columns[3].HeaderText = "Số lượng";
            dtgvCT.Columns[0].Width = 150;
            dtgvCT.Columns[1].Width = 150;
            frmMain.SetupDataGridView(dtgvCT);
            if (dtgvCT.Rows.Count == 0)
            {
                txtMaChiTietCongThuc.Text = "";
            }
            else
            {
                var row = this.dtgvCT.Rows[0];
                txtMaChiTietCongThuc.Text = row.Cells[0].Value.ToString();
                txtMaCongThuccb.Text = row.Cells[1].Value.ToString();
                cboMaNguyenLieu.SelectedValue = row.Cells[2].Value.ToString();
                nmSoLuong.Text = row.Cells[3].Value.ToString();
            }
        }
        private void DanhSachDoUong()
        {
            DataTable dt = dbDU.GetAll("");
            cboMaDoUong.DataSource = dt;
            cboMaDoUong.DisplayMember = "TenDoUong";
            cboMaDoUong.ValueMember = "MaDoUong";
        }
        private void DanhSachNguyenLieu()
        {
            DataTable dt = dbNL.GetAll("");
            cboMaNguyenLieu.DataSource = dt;
            cboMaNguyenLieu.DisplayMember = "TenNguyenLieu";
            cboMaNguyenLieu.ValueMember = "MaNguyenLieu";
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaCongThuc.Text = "CT_" + DateTime.Now.ToString("yyyyMMddhhmmss");
            txtTenCongThuc.Text = "";
            txtNoiDung.Text = "";
            boolcontrols(false);
            save = true;
            txtTenCongThuc.Focus();
        }
        private void boolcontrols(bool iss)
        {
            btnThem.Enabled = iss;
            btnSua.Enabled = iss;
            btnXoa.Enabled = iss;
            btnLuu.Enabled = !iss;
            btnHuy.Enabled = !iss;
            btnThoat.Enabled = iss;
            btnTimKiem.Enabled = iss;
            txtTenCongThuc.Enabled = !iss;
            txtNoiDung.Enabled = !iss;
            cboMaDoUong.Enabled = !iss;
        }
        private void boolcontrolsCT(bool iss)
        {
            btnThemCT.Enabled = iss;
            btnSuaCT.Enabled = iss;
            btnXoaCT.Enabled = iss;
            btnLuuCT.Enabled = !iss;
            btnHuyCT.Enabled = !iss;
            btnThoatCT.Enabled = iss;
            btnTimKiem.Enabled = iss;
            cboMaNguyenLieu.Enabled = !iss;
            nmSoLuong.Enabled = !iss;
        }
        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dtgv.Rows.Count == 0)
            {
                return;
            }
            save = false;
            boolcontrols(false);
            txtTenCongThuc.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dtgv.Rows.Count == 0)
            {
                return;
            }
            DialogResult dr = MessageBox.Show("Có chắc chắn xóa công thức này không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                try
                {
                    db.Delete(dtgv.Rows[dtgv.CurrentCell.RowIndex].Cells[0].Value.ToString());
                    MessageBox.Show("Xóa thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    HienThiCongThuc();
                    boolcontrols(true);
                }
                catch (Exception)
                {
                    MessageBox.Show("Xóa thất bại", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenCongThuc.Focus();
                    return;
                }
            }
            else
                return;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtTenCongThuc.Text == "")
            {
                MessageBox.Show("Tên công thức không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenCongThuc.Focus();
                return;
            }
            if (cboMaDoUong.Text == "")
            {
                MessageBox.Show("Mã đồ uống không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboMaDoUong.Focus();
                return;
            }
            if (save == true)
            {
                try
                {
                    db.Insert(txtMaCongThuc.Text.Trim(),cboMaDoUong.SelectedValue.ToString(), txtTenCongThuc.Text.Trim(), txtNoiDung.Text.Trim());
                    MessageBox.Show("Thêm thành công.");
                    HienThiCongThuc();
                    boolcontrols(true);
                }
                catch (Exception)
                {
                    MessageBox.Show("Mã công thức đã tồn tại, vui lòng tạo mã khác.", "Thông báo",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                try
                {
                    db.Update(txtMaCongThuc.Text.Trim(), cboMaDoUong.SelectedValue.ToString(), txtTenCongThuc.Text.Trim(), txtNoiDung.Text.Trim());
                    MessageBox.Show("Sửa thành công.");
                    HienThiCongThuc();
                    boolcontrols(true);
                }
                catch (Exception)
                {
                    MessageBox.Show("Mã công thức đã tồn tại, vui lòng tạo mã khác.", "Thông báo",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            txtSearch.Text = "";
            HienThiCongThuc();
            boolcontrols(true);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dtgv.Rows[e.RowIndex];
                txtMaCongThuc.Text = row.Cells[0].Value.ToString();
                txtMaCongThuccb.Text = row.Cells[0].Value.ToString();
                txtTenCongThuc.Text = row.Cells[1].Value.ToString();
                cboMaDoUong.SelectedValue = row.Cells[2].Value.ToString();
                txtNoiDung.Text = row.Cells[3].Value.ToString();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            HienThiCongThuc();
        }

        private void btnThemCT_Click(object sender, EventArgs e)
        {
            if (dtgv.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có công thức để thêm chi tiết, hãy thêm mới công thức trước", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenCongThuc.Focus();
                return;
            }
            txtMaChiTietCongThuc.Text = "CTCT_" + DateTime.Now.ToString("yyyyMMddhhmmss");
            boolcontrolsCT(false);
            saveCT = true;
        }

        private void dtgvCT_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dtgvCT.Rows[e.RowIndex];
                txtMaChiTietCongThuc.Text = row.Cells[0].Value.ToString();
                txtMaCongThuc.Text = row.Cells[1].Value.ToString();
                cboMaNguyenLieu.SelectedValue = row.Cells[2].Value.ToString();
                nmSoLuong.Text = row.Cells[3].Value.ToString();
            }
        }

        private void btnSuaCT_Click(object sender, EventArgs e)
        {
            if (dtgvCT.Rows.Count == 0)
            {
                return;
            }
            saveCT = false;
            boolcontrolsCT(false);
        }

        private void btnLuuCT_Click(object sender, EventArgs e)
        {
            if (txtMaChiTietCongThuc.Text == "")
            {
                MessageBox.Show("Mã chi tiết công thức không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaChiTietCongThuc.Focus();
                return;
            }
            if (txtMaCongThuccb.Text == "")
            {
                MessageBox.Show("Mã công thức không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaCongThuccb.Focus();
                return;
            }
            if (saveCT == true)
            {
                try
                {
                    db.InsertCT(txtMaChiTietCongThuc.Text.Trim(), txtMaCongThuc.Text.Trim(), cboMaNguyenLieu.SelectedValue.ToString(), nmSoLuong.Value.ToString());
                    MessageBox.Show("Thêm thành công.");
                    HienThiCongThuc();
                    boolcontrolsCT(true);
                }
                catch (Exception)
                {
                    MessageBox.Show("Mã ct đã tồn tại, vui lòng tạo mã khác.", "Thông báo",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                try
                {
                    db.UpdateCT(txtMaChiTietCongThuc.Text.Trim(), txtMaCongThuc.Text.Trim(), cboMaNguyenLieu.SelectedValue.ToString(), nmSoLuong.Value.ToString());
                    MessageBox.Show("Sửa thành công.");
                    HienThiCongThuc();
                    boolcontrolsCT(true);
                }
                catch (Exception)
                {
                    MessageBox.Show("Mã chi tiết đã tồn tại, vui lòng tạo mã khác.", "Thông báo",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void btnXoaCT_Click(object sender, EventArgs e)
        {
            if (dtgvCT.Rows.Count == 0)
            {
                return;
            }
            DialogResult dr = MessageBox.Show("Có chắc chắn xóa phòng ban này không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                db.DeleteCT(dtgvCT.Rows[dtgvCT.CurrentCell.RowIndex].Cells[0].Value.ToString());
                MessageBox.Show("Xóa thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                HienThiCongThuc();
                boolcontrolsCT(true);
            }
            else
                return;
        }

        private void btnHuyCT_Click(object sender, EventArgs e)
        {
            HienThiCongThuc();
            boolcontrolsCT(true);
        }

        private void btnThoatCT_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
