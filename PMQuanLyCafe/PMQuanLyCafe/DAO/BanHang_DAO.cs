using PMQuanLyCafe.GUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMQuanLyCafe.DAO
{
    public class BanHang_DAO
    {
        public DataTable LayMaBanTheoTen(string TenBan)
        {
            string sql = "select TOP 1 SoBan FROM tblBan WHERE TenBan = N'"+ TenBan + "'";
            return DataAccess.ThucThiQuery(sql);
        }
        public DataTable LayViTriBanTheoTen(string TenBan)
        {
            string sql = "select TOP 1 ViTri FROM tblBan WHERE TenBan = N'" + TenBan + "'";
            return DataAccess.ThucThiQuery(sql);
        }
        public DataTable LayTenBanTheoMa(string SoBan)
        {
            string sql = "select TOP 1 TenBan FROM tblBan WHERE SoBan = " + SoBan + "";
            return DataAccess.ThucThiQuery(sql);
        }
        public DataTable BanTrong()
        {
            string sql = "select * FROM tblBan WHERE TrangThai = 'True'";
            return DataAccess.ThucThiQuery(sql);
        }
        public DataTable BanCoNguoi()
        {
            string sql = "select * FROM tblBan WHERE TrangThai = 'False'";
            return DataAccess.ThucThiQuery(sql);
        }
        public DataTable DanSachDoUong(string TenDoUong)
        {
            string sql = "select TOP 5 HinhAnh,MaDoUong,TenDoUong,GiaTien FROM tblDoUong WHERE TenDoUong LIKE N'%"+ TenDoUong + "%'";
            return DataAccess.ThucThiQuery(sql);
        }
        public DataTable DanSachChiTietHoaDonTheoBan(string SoBan)
        {
            string sql = "SELECT a.MaPhieuOrder,a.MaChiTietPhieuOrder, a.MaDoUong,b.TenDoUong,a.SoLuong,b.GiaTien,(a.SoLuong*b.GiaTien) AS ThanhTien,a.MaNV FROM tblChiPhieuOrder a LEFT JOIN tblDoUong b ON a.MaDoUong = b.MaDoUong left join tblPhieuOrder c ON a.MaPhieuOrder = c.MaPhieuOrder WHERE SoBan = " + SoBan + " AND TrangThai = 'FALSE'";
            return DataAccess.ThucThiQuery(sql);
        }
        public DataTable KiemTraTonKho(string MaDoUong,string NgayLap)
        {
            string sql = "SELECT SoLuongTon FROM tblTonKho WHERE NgayNhap = '"+NgayLap+ "' AND MaDoUong = '"+ MaDoUong + "'";
            return DataAccess.ThucThiQuery(sql);
        }
        public DataTable GetNhanVienGioiThieu()
        {
            string sql = "select MaNV,TenNV FROM tblNhanVien";
            return DataAccess.ThucThiQuery(sql);
        }
        public string TruSoLuongTon(string NgayLap,string MaDoUong,string SoLuongTon)
        {
            string sql = "UPDATE tblTonKho SET SoLuongTon = SoLuongTon - " + SoLuongTon + " WHERE NgayNhap = '"+ NgayLap + "' AND MaDoUong = '"+ MaDoUong + "'";
            return DataAccess.ThucThiNonQuery(sql);
        }
        public string CongSoLuongTon(string NgayLap, string MaDoUong, string SoLuongTon)
        {
            string sql = "UPDATE tblTonKho SET SoLuongTon = SoLuongTon + " + SoLuongTon + " WHERE NgayNhap = '" + NgayLap + "' AND MaDoUong = '" + MaDoUong + "'";
            return DataAccess.ThucThiNonQuery(sql);
        }
        public string ThemtblPhieuOrder(string MaPhieuOrder, string NgayOrder, string MaNV, string SoBan,string MoTa)
        {
            string sql = "insert into tblPhieuOrder values ('" + MaPhieuOrder + "','" + NgayOrder + "','" + MaNV + "',NULL,"+ SoBan + ",N'"+ MoTa + "','False',NULL,NULL,NULL)";
            return DataAccess.ThucThiNonQuery(sql);
        }
        public string ThemtblChiPhieuOrder(string MaChiTietPhieuOrder, string MaPhieuOrder, string MaDoUong , string TenDoUong, string SoLuong,string MaNVGT)
        {
            string sql = "insert into tblChiPhieuOrder values ('" + MaChiTietPhieuOrder + "','" + MaPhieuOrder + "','" + MaDoUong + "',N'" + TenDoUong + "'," + SoLuong + ",'"+ MaNVGT + "')";
            return DataAccess.ThucThiNonQuery(sql);
        }
        public string UpdateThanhBanCoNguoi(string SoBan)
        {
            string sql = "UPDATE tblBan SET TrangThai = 'False' WHERE SoBan = " + SoBan+"";
            return DataAccess.ThucThiNonQuery(sql);
        }
        public string UpdateThanhBanTrong(string SoBan)
        {
            string sql = "UPDATE tblBan SET TrangThai = 'True' WHERE SoBan = " + SoBan + "";
            return DataAccess.ThucThiNonQuery(sql);
        }
        public string XoaDoUongTrongChiTietHoaDon(string MaChiTietPhieuOrder)
        {
            string sql = "DELETE tblChiPhieuOrder WHERE MaChiTietPhieuOrder = '"+ MaChiTietPhieuOrder + "'";
            return DataAccess.ThucThiNonQuery(sql);
        }
        public string XoatblPhieuOrder(string MaPhieuOrder)
        {
            string sql = "DELETE tblPhieuOrder WHERE MaPhieuOrder = '" + MaPhieuOrder + "'";
            return DataAccess.ThucThiNonQuery(sql);
        }
        public string UpdateTrangThaiThanhToan(string MaPhieuOrder,string MaKH)
        {
            string sql = "UPDATE tblPhieuOrder SET TrangThai = 'True',MaKH = '" + MaKH+"' WHERE MaPhieuOrder = '" + MaPhieuOrder + "'";
            return DataAccess.ThucThiNonQuery(sql);
        }
        public string InserttblPhieuThanhToan(string MaPhieuOrder)
        {
            string sql = "DECLARE @SoLuong int,@TongTien int";
            sql += " SET @SoLuong = (SELECT SUM(SoLuong) FROM tblChiPhieuOrder WHERE MaPhieuOrder = '"+ MaPhieuOrder + "')";
            sql += " SET @TongTien = (SELECT SUM(b.GiaTien * a.SoLuong) FROM tblChiPhieuOrder a INNER JOIN tblDoUong b ON a.MaDoUong = b.MaDoUong WHERE MaPhieuOrder = '"+ MaPhieuOrder + "')";
            sql += " INSERT INTO tblPhieuThanhToan";
            sql += " SELECT 'TT_"+DateTime.Now.ToString("yyyyMMddhhmmss")+ "',MaPhieuOrder,MANV,MaKH,SoBan,NgayOrder,@SoLuong,@TongTien FROM tblPhieuOrder WHERE MaPhieuOrder = '"+ MaPhieuOrder + "'";       
            return DataAccess.ThucThiNonQuery(sql);
        }
        public string UpdateHoaDon(string MaPhieuOrder,string HinhThucThanhToan,string VAT,string HDD)
        {
            string str = "";
            VAT = new string(VAT.Where(char.IsDigit).ToArray());
            if (HDD == "1" && HinhThucThanhToan == "Thẻ")
            {
                str = "TongTien = TongTien + (TongTien * 5)/100";
            }    
            else
            {
                str = "TongTien = TongTien + (TongTien * "+ VAT + ")/100";
            }    
            string sql = "UPDATE tblPhieuOrder SET HinhThucThanhToan = N'"+ HinhThucThanhToan + "',VAT = " + VAT + ", HDD = "+ HDD + " WHERE MaPhieuOrder = '" + MaPhieuOrder + "'; ";
            sql += "UPDATE tblPhieuThanhToan SET "+ str + " WHERE MaPhieuOrder = '" + MaPhieuOrder + "'";
            return DataAccess.ThucThiNonQuery(sql);
        }
    }
}
