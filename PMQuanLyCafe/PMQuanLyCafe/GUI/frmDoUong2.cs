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
    public partial class frmDoUong2 : Form
    {
        public frmDoUong2()
        {
            InitializeComponent();
        }
        DoUong_DAO db = new DoUong_DAO();
        LoaiDoUong_DAO dbLDU = new LoaiDoUong_DAO();
        private void txtGiaTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void frmDoUong2_Load(object sender, EventArgs e)
        {
            if (frmDoUong.save == true)
            {
                txtMaDoUong.Select();
                txtMaDoUong.Text = "";
                txtTenDoUong.Text = "";
                txtMoTa.Text = "";
                txtGiaTien.Text = "";
                DanhSachLoaiDoUong();
            }
            else
            {
                DanhSachLoaiDoUong();
                txtMaDoUong.Select();
                txtMaDoUong.Enabled = false;
                cboMaDanhMuc.SelectedValue = frmDoUong.MaDanhMuc;

                DataTable dtimg = new DataTable();
                dtimg = db.HinhAnhDoUong(txtMaDoUong.Text.Trim());
                if (dtimg.Rows.Count > 0)
                {
                    if (dtimg.Rows[0]["HinhAnh"].ToString() != "")
                    {
                        pchinhAnh.Image = ConvertStringtoImage(dtimg.Rows[0]["HinhAnh"].ToString());
                        ImageConverter converter = new ImageConverter();
                        var bytes = (byte[])converter.ConvertTo((Bitmap)pchinhAnh.Image, typeof(byte[]));
                        dtHinhAnh = Convert.ToBase64String(bytes);
                    }
                    else
                    {
                        dtHinhAnh = "";
                        pchinhAnh.Image = null;
                    }
                }
            }
        }
        public System.Drawing.Image ConvertStringtoImage(string commands)
        {
            byte[] photoarray = Convert.FromBase64String(commands);
            MemoryStream ms = new MemoryStream(photoarray, 0, photoarray.Length);
            ms.Write(photoarray, 0, photoarray.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
            return image;
        }
        private void DanhSachLoaiDoUong()
        {
            DataTable dt = dbLDU.GetAll("");
            cboMaDanhMuc.DataSource = dt;
            cboMaDanhMuc.DisplayMember = "TenDanhMuc";
            cboMaDanhMuc.ValueMember = "MaDanhMuc";
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaDoUong.Text == "")
            {
                MessageBox.Show("Mã đồ uống không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaDoUong.Focus();
                return;
            }
            if (txtTenDoUong.Text == "")
            {
                MessageBox.Show("Tên đồ uống không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTenDoUong.Focus();
                return;
            }
            if (cboMaDanhMuc.Text == "")
            {
                MessageBox.Show("Loại đồ uống không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboMaDanhMuc.Focus();
                return;
            }
            if (txtGiaTien.Text == "")
            {
                MessageBox.Show("Giá tiền không được trống", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtGiaTien.Focus();
                return;
            }
            if (frmDoUong.save == true)
            {
                try
                {
                    db.Insert(txtMaDoUong.Text.Trim(),cboMaDanhMuc.SelectedValue.ToString(), txtTenDoUong.Text.Trim(), txtMoTa.Text, txtGiaTien.Text,dtHinhAnh);
                    MessageBox.Show("Thêm thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Mã đồ uống đã tồn tại ,hãy tạo mã khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenDoUong.Focus();
                    return;
                }
            }
            else
            {
                try
                {
                    db.Update(txtMaDoUong.Text.Trim(), cboMaDanhMuc.SelectedValue.ToString(), txtTenDoUong.Text.Trim(), txtMoTa.Text, txtGiaTien.Text, dtHinhAnh);
                    MessageBox.Show("Sửa thành công ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    this.Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Mã đồ uống đã tồn tại ,hãy tạo mã khác", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtTenDoUong.Focus();
                    return;
                }
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private Boolean choseImage = false;
        public static System.Drawing.Image img2;
        private string dtHinhAnh;
        private void btnChonHinh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog OpenFile = new OpenFileDialog();
            string filename;
            OpenFile.Multiselect = false;
            OpenFile.Filter = "Images (*.png, *.gif, *.tif, *.tiff, *.bmp, *.jpg, *.jpeg, *.jpe, *.jfif)|*.png;*.gif;*.tif;*.tiff;*.bmp;*.jpg;*.jpeg;*.jpe;*.jfif";
            if (OpenFile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = OpenFile.FileName;
                OpenFile.Dispose();
                if (filename != "")
                {
                    choseImage = true;
                    System.Drawing.Image img;
                    try
                    {
                        img = System.Drawing.Image.FromFile(filename);
                        if (img != null)
                        {
                            pchinhAnh.Image = img;
                            img2 = img;
                            ImageConverter converter = new ImageConverter();
                            var bytes = (byte[])converter.ConvertTo((Bitmap)pchinhAnh.Image, typeof(byte[]));
                            dtHinhAnh = Convert.ToBase64String(bytes);
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
        }

        private void btnXoaHinh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            choseImage = true;
            pchinhAnh.Image = null;
            img2 = null;
            dtHinhAnh = "";
        }
    }
}
