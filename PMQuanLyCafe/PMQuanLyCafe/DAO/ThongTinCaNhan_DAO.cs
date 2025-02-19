using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMQuanLyCafe.DAO
{
    public class ThongTinCaNhan_DAO
    {
        public DataTable ThongTinCaNhan(string MaNV)
        {
            string sql = "select * FROM tblNhanVien WHERE MaNV = N'" + MaNV + "'";
            return DataAccess.ThucThiQuery(sql);
        }
        public string DoiMatKhau(string MatKhau, string MaNV)
        {
            string sql = "UPDATE tblNhanVien SET MatKhau = '"+ MatKhau + "' WHERE MaNV = '"+ MaNV + "'";
            return DataAccess.ThucThiNonQuery(sql);
        }
    }
}
