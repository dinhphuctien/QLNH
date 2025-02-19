using Microsoft.Reporting.WinForms;
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
    public partial class frmInBill : Form
    {
        public frmInBill()
        {
            InitializeComponent();
        }
        BaoCao_DAO db = new BaoCao_DAO();
        private void frmInBill_Load(object sender, EventArgs e)
        {
            DataTable dt = db.InHoaDon(frmMain.MaHDThanhToan);
            DataTable dt2 = db.InHoaDonThongTinChung(frmMain.MaHDThanhToan);
            string tenkh ="";
            if (dt2.Rows[0][0].ToString() == "")
            {
                tenkh = "Khách vãng lai";
            }
            else
                tenkh = dt2.Rows[0][0].ToString();

            reportViewer1.LocalReport.ReportEmbeddedResource = "PMQuanLyCafe.Report1.rdlc";
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            ReportParameter[] parameters = new ReportParameter[5];
            parameters[0] = new ReportParameter("TenKH", tenkh, true);
            parameters[1] = new ReportParameter("TenTK_NV", dt2.Rows[0][1].ToString(), true);
            parameters[2] = new ReportParameter("NgayHD", dt2.Rows[0][2].ToString(), true);
            parameters[3] = new ReportParameter("TongTien", frmMain.TongTienInHD, true);
            parameters[4] = new ReportParameter("ViTri", frmMain.ViTriBanInHD, true);
            reportViewer1.LocalReport.SetParameters(parameters);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.RefreshReport();
        }
    }
}
