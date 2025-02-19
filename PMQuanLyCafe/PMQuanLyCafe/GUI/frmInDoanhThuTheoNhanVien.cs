using Microsoft.Reporting.WinForms;
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
    public partial class frmInDoanhThuTheoNhanVien : Form
    {
        public frmInDoanhThuTheoNhanVien()
        {
            InitializeComponent();
        }

        private void frmInDoanhThuTheoNhanVien_Load(object sender, EventArgs e)
        {
            DataTable dt = frmDoanhThuTheoNhanVien.dtIn;

            reportViewer1.LocalReport.ReportEmbeddedResource = "PMQuanLyCafe.RpDoanhThuTheoNhanVien.rdlc";
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);
            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("TongTien", frmDoanhThuTheoNhanVien.tongtien, true);
            reportViewer1.LocalReport.SetParameters(parameters);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.RefreshReport();
        }
    }
}
