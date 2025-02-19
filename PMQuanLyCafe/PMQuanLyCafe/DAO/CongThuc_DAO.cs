using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMQuanLyCafe.DAO
{
    public class CongThuc_DAO
    {
        public DataTable GetAll(string TenCongThuc)
        {
            string sql = "select MaCongThuc,TenCongThuc,MaDoUong,NoiDung FROM tblCongThuc WHERE TenCongThuc LIKE N'%" + TenCongThuc + "%'";
            return DataAccess.ThucThiQuery(sql);
        }
        public DataTable ChiTietCongThuc(string MaCongThuc)
        {
            string sql = "select * FROM tblChiTietCongThuc WHERE MaCongThuc = N'" + MaCongThuc + "'";
            return DataAccess.ThucThiQuery(sql);
        }
        //Phương thức thêm 
        public string Insert(string MaCongThuc, string MaDoUong, string TenCongThuc,string NoiDung)
        {
            string sql = "insert into tblCongThuc values ('" + MaCongThuc + "','" + MaDoUong + "',N'" + TenCongThuc + "',N'"+ NoiDung + "')";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Phương thức sửa 
        public string Update(string MaCongThuc, string MaDoUong, string TenCongThuc, string NoiDung)
        {
            string sql = "update tblCongThuc set TenCongThuc=N'" + TenCongThuc + "',MaDoUong = N'" + MaDoUong + "',NoiDung = N'"+ NoiDung + "'  where  MaCongThuc='" + MaCongThuc + "'";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Phương thức xóa
        public string Delete(string MaCongThuc)
        {
            string sql = "DELETE tblChiTietCongThuc WHERE MaCongThuc = '"+ MaCongThuc + "' DELETE tblCongThuc WHERE MaCongThuc = '" + MaCongThuc + "'";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Phương thức thêm 
        public string InsertCT(string MaChiTietCongThuc, string MaCongThuc, string MaNguyenLieu, string SoLuong)
        {
            string sql = "insert into tblChiTietCongThuc values ('" + MaChiTietCongThuc + "','" + MaCongThuc + "',N'" + MaNguyenLieu + "'," + SoLuong + ")";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Phương thức sửa 
        public string UpdateCT(string MaChiTietCongThuc, string MaCongThuc, string MaNguyenLieu, string SoLuong)
        {
            string sql = "update tblChiTietCongThuc set MaCongThuc=N'" + MaCongThuc + "',MaNguyenLieu = N'" + MaNguyenLieu + "',SoLuong = " + SoLuong + "  where  MaChiTietCongThuc='" + MaChiTietCongThuc + "'";
            return DataAccess.ThucThiNonQuery(sql);
        }
        //Phương thức xóa
        public string DeleteCT(string MaChiTietCongThuc)
        {
            string sql = "DELETE tblChiTietCongThuc WHERE MaChiTietCongThuc = '" + MaChiTietCongThuc + "'";
            return DataAccess.ThucThiNonQuery(sql);
        }
    }
}
