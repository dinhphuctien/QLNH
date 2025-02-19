using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMQuanLyCafe.DAO
{
    public class KhachHang_DAO
    {
        public DataTable GetAll(string TenKH)
        {
            string sql = "select * FROM tblKhachHang where TenKH like N'%"+ TenKH + "%'";
            return DataAccess.ThucThiQuery(sql);
        }
        public DataTable GeSearch(string MaKH,string TenKH,string SDT)
        {
            string sql = "select * FROM tblKhachHang WHERE MaKH LIKE '%"+ MaKH + "%' AND TenKH LIKE N'%"+ TenKH + "%' AND SDT LIKE '%"+ SDT + "%'";
            return DataAccess.ThucThiQuery(sql);
        }
        //Phương thức thêm 
        public string Insert(string MaKH, string TenKH,string GioiTinh,string SDT)
        {
            string sql = "insert into tblKhachHang values (N'" + MaKH + "',N'" + TenKH + "',N'"+ GioiTinh + "','"+ SDT + "')";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Phương thức sửa 
        public string Update(string MaKH, string TenKH, string GioiTinh, string SDT)
        {
            string sql = "update tblKhachHang set TenKH=N'" + TenKH + "',GioiTinh = N'"+ GioiTinh + "',SDT = '"+ SDT + "'  where  MaKH=N'" + MaKH + "'";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Phương thức xóa
        public string Delete(string MaKH)
        {
            string sql = "DELETE tblKhachHang WHERE MaKH = '" + MaKH + "'";
            return DataAccess.ThucThiNonQuery(sql);
        }
    }
}
