using models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    public class khach_hangService
    {
        public khach_hang check_thongtin_login(string email, string matkhau)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                connection.Open();
                string CheckThongTinDangNhap = "select * from khach_hang where email = @email and matkhau = @matkhau ";
                using (SqlCommand command = new SqlCommand(CheckThongTinDangNhap, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@matkhau", matkhau);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            khach_hang khach_Hang = new khach_hang()
                            {
                                id = Convert.ToInt32(reader["id"]),
                                hovaten = reader["hovaten"].ToString(),
                                diachi = reader["diachi"].ToString(),
                                sodienthoai = reader["sodienthoai"].ToString(),
                                email = reader["email"].ToString(),
                                matkhau = reader["matkhau"].ToString(),
                                cccd = reader["cccd"].ToString(),
                                anh = reader["anh"].ToString(),

                            };
                            return khach_Hang;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
        }
        public khach_hang LayThongTinKhachHangTheoID(int id)
        {
            khach_hang khachHang = null;

            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                connection.Open();
                string query = "SELECT * FROM khach_hang WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            khachHang = new khach_hang()
                            {
                                id = Convert.ToInt32(reader["id"]),
                                hovaten = reader["hovaten"].ToString(),
                                diachi = reader["diachi"].ToString(),
                                sodienthoai = reader["sodienthoai"].ToString(),
                                email = reader["email"].ToString(),
                                matkhau = reader["matkhau"].ToString(),
                                cccd = reader["cccd"].ToString(),
                                anh = reader["anh"].ToString(),
                            };
                        }
                    }
                }
            }

            return khachHang;
        }
        public void RegisterKhachHang(khach_hang khachHang)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                connection.Open();
                string insertQuery = "INSERT INTO khach_hang (hovaten, sodienthoai, email, matkhau) " +
                                     "VALUES (@hovaten, @sodienthoai, @email, @matkhau)";
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@hovaten", khachHang.hovaten);
                    command.Parameters.AddWithValue("@sodienthoai", khachHang.sodienthoai);
                    command.Parameters.AddWithValue("@email", khachHang.email);
                    command.Parameters.AddWithValue("@matkhau", khachHang.matkhau);                 
                    command.ExecuteNonQuery();
                }
            }
        }
        public void UpdateKhachHang(khach_hang khachHang)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                connection.Open();
                string updateQuery = "UPDATE khach_hang SET hovaten = @hovaten, diachi = @diachi, sodienthoai = @sodienthoai, " +
                                     "email = @email, matkhau = @matkhau, cccd = @cccd, anh = @anh WHERE id = @id";
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", khachHang.id);
                    command.Parameters.AddWithValue("@hovaten", khachHang.hovaten);
                    command.Parameters.AddWithValue("@diachi", khachHang.diachi);
                    command.Parameters.AddWithValue("@sodienthoai", khachHang.sodienthoai);
                    command.Parameters.AddWithValue("@email", khachHang.email);
                    command.Parameters.AddWithValue("@matkhau", khachHang.matkhau);
                    command.Parameters.AddWithValue("@cccd", khachHang.cccd);
                    command.Parameters.AddWithValue("@anh", khachHang.anh);
                    command.ExecuteNonQuery();
                }
            }
        }
        public khach_hang GetKhachHangById(int id)
        {
            khach_hang khachHang = null;

            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                connection.Open();
                string query = "SELECT * FROM khach_hang WHERE id = @id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            khachHang = new khach_hang()
                            {
                                id = Convert.ToInt32(reader["id"]),
                                hovaten = reader["hovaten"].ToString(),
                                diachi = reader["diachi"].ToString(),
                                sodienthoai = reader["sodienthoai"].ToString(),
                                email = reader["email"].ToString(),
                                matkhau = reader["matkhau"].ToString(),
                                cccd = reader["cccd"].ToString(),
                                anh = reader["anh"].ToString(),
                            };
                        }
                    }
                }
            }

            return khachHang;
        }

    }
}