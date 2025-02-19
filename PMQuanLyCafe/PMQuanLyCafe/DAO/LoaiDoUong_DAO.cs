using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMQuanLyCafe.DAO
{
    public class LoaiDoUong_DAO
    {
        public DataTable GetAll(string TenDanhMuc)
        {
            string sql = "select * FROM tblDanhMucDoUong WHERE TenDanhMuc LIKE N'%"+ TenDanhMuc + "%'";
            return DataAccess.ThucThiQuery(sql);
        }
        //Phương thức thêm 
        public string Insert(string MaDanhMuc, string TenDanhMuc)
        {
            string sql = "insert into tblDanhMucDoUong values (N'" + MaDanhMuc + "',N'" + TenDanhMuc + "')";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Phương thức sửa 
        public string Update(string MaDanhMuc, string TenDanhMuc)
        {
            string sql = "update tblDanhMucDoUong set TenDanhMuc=N'" + TenDanhMuc + "' where  MaDanhMuc=N'" + MaDanhMuc + "'";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Phương thức xóa
        public string Delete(string MaDanhMuc)
        {
            string sql = "DELETE tblDanhMucDoUong WHERE MaDanhMuc = '"+ MaDanhMuc + "'";
            return DataAccess.ThucThiNonQuery(sql);
        }
    }
}
