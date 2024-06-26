using models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    public class lich_su_thanh_toanService
    {
        public void ThemLichSuThanhToan(lich_su_thanh_toan lichSuThanhToan)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string sqlQuery = @"INSERT INTO lich_su_thanh_toan (ngaythanhtoan, sotienthanhtoan, hinhthucthanhtoan, trangthai, phantramgiamgia, donhang_id) 
                                    VALUES (@NgayThanhToan, @SoTienThanhToan, @HinhThucThanhToan, @TrangThai, @PhanTramGiamGia, @DonHangId)";

                SqlCommand command = new SqlCommand(sqlQuery, connection);
                command.Parameters.AddWithValue("@NgayThanhToan", lichSuThanhToan.ngaythanhtoan);
                command.Parameters.AddWithValue("@SoTienThanhToan", lichSuThanhToan.sotienthanhtoan);
                command.Parameters.AddWithValue("@HinhThucThanhToan", lichSuThanhToan.hinhthucthanhtoan);
                command.Parameters.AddWithValue("@TrangThai", lichSuThanhToan.trangthai);
                command.Parameters.AddWithValue("@PhanTramGiamGia", lichSuThanhToan.phantramgiamgia);
                command.Parameters.AddWithValue("@DonHangId", lichSuThanhToan.donhang_id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
