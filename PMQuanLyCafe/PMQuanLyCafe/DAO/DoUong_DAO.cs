using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMQuanLyCafe.DAO
{
    public class DoUong_DAO
    {
        public DataTable GetAll(string TenDoUong)
        {
            string sql = "select a.HinhAnh,a.MaDoUong,a.TenDoUong,a.MoTa,a.GiaTien,a.MaDanhMuc,b.TenDanhMuc FROM tblDoUong a INNER JOIN tblDanhMucDoUong b ON a.MaDanhMuc = b.MaDanhMuc WHERE TenDoUong like N'%" + TenDoUong + "%'";
            return DataAccess.ThucThiQuery(sql);
        }
        public DataTable HinhAnhDoUong(string MaDoUong)
        {
            string query = "select hinhAnh from tblDoUong where MaDoUong = '" + MaDoUong + "'";
            return DataAccess.ThucThiQuery(query);
        }
        //Phương thức thêm 
        public string Insert(string MaDoUong, string MaDanhMuc,string TenDoUong,string MoTa,string GiaTien,string HinhAnh)
        {
            string sql = "insert into tblDoUong values (N'" + MaDoUong + "',N'" + MaDanhMuc + "',N'"+ TenDoUong + "',N'"+ MoTa + "',"+ GiaTien + ",'"+ HinhAnh + "')";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Phương thức sửa 
        public string Update(string MaDoUong, string MaDanhMuc, string TenDoUong, string MoTa, string GiaTien,string HinhAnh)
        {
            string sql = "update tblDoUong set MaDanhMuc=N'" + MaDanhMuc + "',TenDoUong = N'"+ TenDoUong + "',MoTa = N'"+ MoTa + "',GiaTien = "+ GiaTien + ",HinhAnh = '"+ HinhAnh + "' where  MaDoUong=N'" + MaDoUong + "'";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Phương thức xóa
        public string Delete(string MaDoUong)
        {
            string sql = "DELETE tblDoUong WHERE MaDoUong = '" + MaDoUong + "'";
            return DataAccess.ThucThiNonQuery(sql);
        }
    }
}
