using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PMQuanLyCafe.DAO
{
    public class BaoCao_DAO
    {
        public DataTable InHoaDon(string MaPhieuOrder)
        {
            string strSQL = "SELECT b.TenDoUong as TenHH,SoLuong,GiaTien as DonGia,(SoLuong*GiaTien) as ThanhTien FROM  tblChiPhieuOrder a LEFT JOIN tblDoUong b ON a.MaDoUong = b.MaDoUong WHERE a.MaPhieuOrder = '" + MaPhieuOrder + "'";
            return DataAccess.ThucThiQuery(strSQL);
        }
        public DataTable InHoaDonThongTinChung(string MaPhieuOrder)
        {
            string strSQL = "SELECT TenKH,TenNV as TenTK_NV,CONVERT(varchar,NgayOrder,103) as NgayHoaDon FROM  tblPhieuOrder a LEFT JOIN tblKhachHang b ON a.MaKH = b.MaKH LEFT JOIN tblNhanVien c ON a.MaNV = c.MaNV WHERE a.MaPhieuOrder = '" + MaPhieuOrder + "'";
            return DataAccess.ThucThiQuery(strSQL);
        }
        public DataTable ThongKeDoanhThuTheoNgay(string dt1, string dt2)
        {
            string sql = "SELECT CONVERT(VARCHAR,ROW_NUMBER() OVER ( ORDER BY NgayThanhToan )) as STT,CONVERT(varchar,NgayThanhToan,103) as NgayThanhToan,SUM(SoLuong) as SoLuong,SUM(TongTien) as ThanhTien FROM tblPhieuThanhToan WHERE NgayThanhToan BETWEEN '" + dt1+"' AND '"+dt2+"' GROUP BY NgayThanhToan";
            return DataAccess.ThucThiQuery(sql);
        }
        public DataTable ThongKeDoanhThuTheoNhanVien(string dt1, string dt2)
        {
            string sql = "SELECT a.MaNV,TenNV,SUM(SoLuong) as SoLuong,SUM(TongTien) as ThanhTien into #tmp FROM tblPhieuThanhToan a INNER JOIN tblNhanVien b ON a.MaNV = b.MaNV WHERE NgayThanhToan BETWEEN '" + dt1 + "' AND '" + dt2 + "' GROUP BY a.MaNV,TenNV select CONVERT(VARCHAR,ROW_NUMBER() OVER ( ORDER BY MaNV )) as STT,* FROM #tmp";
            return DataAccess.ThucThiQuery(sql);
        }
        public DataTable ThongKeBanHang(string dt1, string dt2)
        {
            string sql = "SELECT CONVERT(VARCHAR,ROW_NUMBER() OVER ( ORDER BY NgayOrder )) as STT,CONVERT(varchar,NgayOrder,103) as NgayOrder, b.MaDoUong,b.TenDoUong,SUM(SoLuong) as ThanhTien FROM tblPhieuOrder a INNER JOIN tblChiPhieuOrder b ON a.MaPhieuOrder = b.MaPhieuOrder WHERE a.TrangThai = 'True' AND NgayOrder BETWEEN '" + dt1 + "' AND '" + dt2 + "' GROUP BY NgayOrder,b.MaDoUong,b.TenDoUong";
            return DataAccess.ThucThiQuery(sql);
        }
        public DataTable LichSuHoaDon(string dt1, string dt2)
        {
            string sql = "SELECT CONVERT(VARCHAR,ROW_NUMBER() OVER ( ORDER BY a.NgayOrder )) as STT,CONVERT(varchar,a.NgayOrder,103) as NgayOrder,a.MaPhieuOrder,b.MaChiTietPhieuOrder,a.MaNV,a.MaKH,b.MaDoUong,b.TenDoUong,b.SoLuong,c.GiaTien,(b.SoLuong*c.GiaTien) as ThanhTien,CASE WHEN a.TrangThai = 'True' THEN N'Đã thanh toán' ELSE N'chưa thanh toán' end as TrangThai FROM tblPhieuOrder a INNER JOIN tblChiPhieuOrder b ON a.MaPhieuOrder = b.MaPhieuOrder INNER JOIN tblDoUong c ON b.MaDoUong = c.MaDoUong WHERE NgayOrder BETWEEN '" + dt1 + "' AND '" + dt2 + "'";
            return DataAccess.ThucThiQuery(sql);
        }
    }
}
