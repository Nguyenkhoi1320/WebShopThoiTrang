using models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    public class chi_tiet_don_hangService
    {
        public void ThemChiTietDonHang(chi_tiet_don_hang chi_Tiet_Don_Hang)
        {
            string query = "INSERT INTO chi_tiet_don_hang (donhang_id, sanpham_id, soluongmua, thanhtien) " +
                           "VALUES (@DonHangId, @SanPhamId, @SoLuongMua, @ThanhTien)";

            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DonHangId", chi_Tiet_Don_Hang.donhang_id);
                    command.Parameters.AddWithValue("@SanPhamId", chi_Tiet_Don_Hang.sanpham_id);
                    command.Parameters.AddWithValue("@SoLuongMua", chi_Tiet_Don_Hang.soluongmua);
                    command.Parameters.AddWithValue("@ThanhTien", chi_Tiet_Don_Hang.thanhtien);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }
        }
        public List<chi_tiet_don_hang> LayDanhSachChiTietDonHangTheoDonHangId(int donHangId)
        {
            List<chi_tiet_don_hang> chiTietDonHangList = new List<chi_tiet_don_hang>();
            string query = "SELECT * FROM chi_tiet_don_hang WHERE donhang_id = @DonHangId";

            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@DonHangId", donHangId);

                    try
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                chi_tiet_don_hang chiTietDonHang = new chi_tiet_don_hang
                                {
                                    id = Convert.ToInt32(reader["id"]),
                                    donhang_id = Convert.ToInt32(reader["donhang_id"]),
                                    sanpham_id = Convert.ToInt32(reader["sanpham_id"]),
                                    soluongmua = Convert.ToInt32(reader["soluongmua"]),
                                    thanhtien = Convert.ToDecimal(reader["thanhtien"])
                                    // Map other properties accordingly
                                };

                                chiTietDonHangList.Add(chiTietDonHang);
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
            }

            return chiTietDonHangList;
        }
    }
}
