using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMQuanLyCafe.DAO
{
    public class Ban_DAO
    {
        public DataTable GetAll(string TenBan)
        {
            string sql = "select * FROM tblBan WHERE TenBan LIKE N'%"+ TenBan + "%'";
            return DataAccess.ThucThiQuery(sql);
        }
        //Phương thức thêm 
        public string Insert(string SoBan, string TenBan, string ViTri)
        {
            string sql = "insert into tblBan values (" + SoBan + ",N'" + TenBan + "',N'" + ViTri + "','True')";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Phương thức sửa 
        public string Update(string SoBan, string TenBan, string ViTri)
        {
            string sql = "update tblBan set TenBan=N'" + TenBan + "',ViTri = N'" + ViTri + "'  where  SoBan=" + SoBan + "";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Phương thức xóa
        public string Delete(string SoBan)
        {
            string sql = "DELETE tblBan WHERE SoBan = '" + SoBan + "'";
            return DataAccess.ThucThiNonQuery(sql);
        }
    }
}
