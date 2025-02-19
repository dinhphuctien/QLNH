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
    public partial class frmLichSuHoaDon : Form
    {
        public frmLichSuHoaDon()
        {
            InitializeComponent();
        }
        BaoCao_DAO db = new BaoCao_DAO();
        public static string tongtien;
        public static DataTable dtIn;
        private void btnLoc_Click(object sender, EventArgs e)
        {
            GetData();
        }
        private void GetData()
        {
            dtgv.DataSource = db.LichSuHoaDon(dt1.Value.ToString("yyyyMMdd"), dt2.Value.ToString("yyyyMMdd"));
            frmMain.SetupDataGridView(dtgv);
            dtgv.Columns[0].HeaderText = "STT";
            dtgv.Columns[1].HeaderText = "Ngày hóa đơn";
            dtgv.Columns[2].HeaderText = "Mã phiếu";
            dtgv.Columns[3].HeaderText = "Mã phiếu chi tiết";
            dtgv.Columns[4].HeaderText = "Mã NV";
            dtgv.Columns[5].HeaderText = "Mã KH";
            dtgv.Columns[6].HeaderText = "Mã đồ uống";
            dtgv.Columns[7].HeaderText = "Tên đồ uống";
            dtgv.Columns[8].HeaderText = "Số lượng";
            dtgv.Columns[9].HeaderText = "Đơn giá";
            dtgv.Columns[10].HeaderText = "Thành tiền";
            dtgv.Columns[11].HeaderText = "Trạng thái";
            TongTien();
        }
        public void TongTien()
        {
            if (dtgv.Rows.Count == 0)
            {
                lblTongTien.Text = "0 VNĐ";
                return;
            }
            int tien = dtgv.Rows.Count;
            decimal thanhtien = 0;
            for (int i = 0; i < tien; i++)
            {
                thanhtien += decimal.Parse(dtgv.Rows[i].Cells["ThanhTien"].Value.ToString());
            }
            lblTongTien.Text = thanhtien.ToString("#,###") + " VNĐ";
            if (lblTongTien.Text == " VNĐ")
            {
                lblTongTien.Text = "0 VNĐ";
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            app.Visible = true;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            for (int i = 1; i < dtgv.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dtgv.Columns[i - 1].HeaderText;
            }
            for (int i = 0; i < dtgv.Rows.Count; i++)
            {
                for (int j = 0; j < dtgv.Columns.Count; j++)
                {
                    if (dtgv.Rows[i].Cells[j].Value != null)
                    {
                        worksheet.Cells[i + 2, j + 1] = dtgv.Rows[i].Cells[j].Value.ToString();
                    }
                    else
                    {
                        worksheet.Cells[i + 2, j + 1] = "";
                    }
                }
            }
        }

        private void frmLichSuHoaDon_Load(object sender, EventArgs e)
        {
            
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            if (dtgv.Rows.Count == 0)
            {
                MessageBox.Show("Bấm lọc dữ liệu trước khi bấm In báo cáo");
                return;
            }
            dtIn = db.LichSuHoaDon(dt1.Value.ToString("yyyyMMdd"), dt2.Value.ToString("yyyyMMdd"));
            tongtien = lblTongTien.Text;
            frmInLichSuHoaDon frm = new frmInLichSuHoaDon();
            frm.ShowDialog();
        }
    }
}
