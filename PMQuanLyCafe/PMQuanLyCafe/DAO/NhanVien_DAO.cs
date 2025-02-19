using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMQuanLyCafe.DAO
{
    public class NhanVien_DAO
    {
        public DataTable GetAll(string TenNV)
        {
            string sql = "select MaNV,TenNV,GioiTinh,SDT,CONVERT(varchar,NgayVaoLam,103) as NgayVaoLam,MatKhau,Quyen  FROM tblNhanVien where TenNV like N'%" + TenNV + "%'";
            return DataAccess.ThucThiQuery(sql);
        }
        public DataTable DangNhap(string TenDangNhap,string MatKhau,string Quyen)
        {
            string sql = "select * FROM tblNhanVien WHERE MaNV = '"+ TenDangNhap + "' AND MatKhau = '"+ MatKhau + "' AND Quyen = N'"+ Quyen + "'";
            return DataAccess.ThucThiQuery(sql);
        }
   
        //Phương thức thêm 
        public string Insert(string MaNV, string TenNV, string GioiTinh, string SDT,string NgayVaoLam, string MatKhau,string Quyen)
        {
            string sql = "insert into tblNhanVien values (N'" + MaNV + "',N'" + TenNV + "',N'" + GioiTinh + "','" + SDT + "','"+NgayVaoLam+"','"+MatKhau+"',N'"+Quyen +"')";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Phương thức sửa 
        public string Update(string MaNV, string TenNV, string GioiTinh, string SDT, string NgayVaoLam, string MatKhau, string Quyen)
        {
            string sql = "update tblNhanVien set TenNV=N'" + TenNV + "',GioiTinh = N'" + GioiTinh + "',SDT = '" + SDT + "',NgayVaoLam = '"+ NgayVaoLam + "', MatKhau = '"+ MatKhau + "',Quyen = N'"+ Quyen + "' where  MaNV=N'" + MaNV + "'";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Phương thức xóa
        public string Delete(string MaNV)
        {
            string sql = "DELETE tblNhanVien WHERE MaNV = '" + MaNV + "'";
            return DataAccess.ThucThiNonQuery(sql);
        }
    }
}
