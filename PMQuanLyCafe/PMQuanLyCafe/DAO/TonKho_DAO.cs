using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMQuanLyCafe.DAO
{
    public class TonKho_DAO
    {
        public DataTable GetAll(string TenDoUong)
        {
            string sql = "select a.NgayNhap,a.MaDoUong,b.TenDoUong,a.SoLuongTon FROM tblTonKho a INNER JOIN tblDoUong b ON a.MaDoUong = b.MaDoUong where TenDoUong like N'%" + TenDoUong + "%'";
            return DataAccess.ThucThiQuery(sql);
        }
        //Phương thức thêm 
        public string Insert(string NgayNhap, string MaDoUong, string SoLuongTon)
        {
            string sql = "insert into tblTonKho values (N'" + NgayNhap + "',N'" + MaDoUong + "'," + SoLuongTon + ")";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Phương thức sửa 
        public string Update(DateTime NgayNhap, string MaDoUong, string SoLuongTon)
        {
            string sql = "update tblTonKho set SoLuongTon = " + SoLuongTon + " where  NgayNhap='" + NgayNhap.ToString("yyyyMMdd") + "' AND MaDoUong=N'" + MaDoUong + "'";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Phương thức xóa
        public string Delete(DateTime NgayNhap,string MaDoUong)
        {
            string sql = "DELETE tblTonKho WHERE NgayNhap = '" + NgayNhap.ToString("yyyyMMdd") + "' AND MaDoUong = '"+ MaDoUong + "'";
            return DataAccess.ThucThiNonQuery(sql);
        }
    }
}
