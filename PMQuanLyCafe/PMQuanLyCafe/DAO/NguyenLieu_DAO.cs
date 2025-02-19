using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMQuanLyCafe.DAO
{
    public class NguyenLieu_DAO
    {
        public DataTable GetAll(string TenNguyenLieu)
        {
            string sql = "select * FROM tblNguyenLieu where TenNguyenLieu like N'%"+ TenNguyenLieu + "%'";
            return DataAccess.ThucThiQuery(sql);
        }
        //Phương thức thêm 
        public string Insert(string MaNguyenLieu, string TenNguyenLieu,string SoLuong)
        {
            string sql = "insert into tblNguyenLieu values (N'" + MaNguyenLieu + "',N'" + TenNguyenLieu + "',"+ SoLuong + ")";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Phương thức sửa 
        public string Update(string MaNguyenLieu, string TenNguyenLieu,string SoLuong)
        {
            string sql = "update tblNguyenLieu set TenNguyenLieu=N'" + TenNguyenLieu + "',SoLuong = "+ SoLuong + " where  MaNguyenLieu=N'" + MaNguyenLieu + "'";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Phương thức xóa
        public string Delete(string MaNguyenLieu)
        {
            string sql = "DELETE tblNguyenLieu WHERE MaNguyenLieu = '" + MaNguyenLieu + "'";
            return DataAccess.ThucThiNonQuery(sql);
        }
    }
}
