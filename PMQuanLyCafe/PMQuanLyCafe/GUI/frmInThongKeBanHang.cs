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
    public partial class frmInThongKeBanHang : Form
    {
        public frmInThongKeBanHang()
        {
            InitializeComponent();
        }

        private void frmInThongKeBanHang_Load(object sender, EventArgs e)
        {
            DataTable dt = frmThongKeBanHang.dtIn;

            reportViewer1.LocalReport.ReportEmbeddedResource = "PMQuanLyCafe.RpThongKeBanHang.rdlc";
            ReportDataSource rds = new ReportDataSource("DataSet1", dt);

            ReportParameter[] parameters = new ReportParameter[1];
            parameters[0] = new ReportParameter("TongTien", frmThongKeBanHang.tongtien, true);
            reportViewer1.LocalReport.SetParameters(parameters);
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.RefreshReport();
        }
    }
}
