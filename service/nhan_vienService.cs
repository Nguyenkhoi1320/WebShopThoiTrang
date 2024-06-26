using models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    public class nhan_vienService
    {
        public List<nhan_vien> LayDanhSachNhanVien()
        {
            List<nhan_vien> danhSachNhanVien = new List<nhan_vien>();

            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "SELECT * FROM nhan_vien"; // Replace "nhan_vien" with your actual table name

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    danhSachNhanVien.Add(new nhan_vien
                    {
                        id = reader.GetInt32(0),
                        hovaten = reader.GetString(1),
                        dienthoai = reader.GetString(2),
                        email = reader.GetString(3),
                        matkhau = reader.GetString(4),
                        vaitro = reader.GetString(5),
                        diachi = reader.GetString(6),
                        luong = (float)reader.GetDecimal(7) // Cast Decimal to Float
                    });
                }

                reader.Close();
            }

            return danhSachNhanVien;
        }
        public void AddNhanVien(nhan_vien nhanVien)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "INSERT INTO nhan_vien (hovaten, dienthoai, email, matkhau, vaitro, diachi, luong) " +
                               "VALUES (@hovaten, @dienthoai, @email, @matkhau, @vaitro, @diachi, @luong); " +
                               "SELECT SCOPE_IDENTITY();";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@hovaten", nhanVien.hovaten);
                command.Parameters.AddWithValue("@dienthoai", nhanVien.dienthoai);
                command.Parameters.AddWithValue("@email", nhanVien.email);
                command.Parameters.AddWithValue("@matkhau", nhanVien.matkhau);
                command.Parameters.AddWithValue("@vaitro", nhanVien.vaitro);
                command.Parameters.AddWithValue("@diachi", nhanVien.diachi);
                command.Parameters.AddWithValue("@luong", nhanVien.luong);

                connection.Open();
                var result = command.ExecuteScalar();

                // Optionally, if you need the ID of the newly inserted record
                int newId = Convert.ToInt32(result);
            }
        }
        public nhan_vien nhanvienbyid(int id)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                connection.Open();
                string CheckThongTinDangNhap = "select * from nhan_vien where id = @id";
                using (SqlCommand command = new SqlCommand(CheckThongTinDangNhap, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nhan_vien khach_Hang = new nhan_vien()
                            {
                                id = Convert.ToInt32(reader["id"]),
                                hovaten = reader["hovaten"].ToString(),
                                diachi = reader["diachi"].ToString(),
                                dienthoai = reader["dienthoai"].ToString(),
                                email = reader["email"].ToString(),
                                matkhau = reader["matkhau"].ToString(),
                                luong = (float)reader.GetDecimal(7) // Cast Decimal to Float
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
        public void updatenhanvien(nhan_vien nhan_Vien)
        {
                using (SqlConnection connection = DBUtils.GetDBConnection())
                {
                    string query = "UPDATE nhan_vien SET hovaten = @hovaten, " +
                                   "dienthoai = @dienthoai, email = @email, " +
                                   "vaitro = @vaitro, "+
                                   "diachi = @diachi,"+
                                   "luong = @luong WHERE id = @id";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@hovaten", nhan_Vien.hovaten);
                    command.Parameters.AddWithValue("@dienthoai", nhan_Vien.dienthoai);
                    command.Parameters.AddWithValue("@email", nhan_Vien.email);
                    command.Parameters.AddWithValue("@vaitro", nhan_Vien.vaitro);
                    command.Parameters.AddWithValue("@diachi", nhan_Vien.diachi);
                    command.Parameters.AddWithValue("@luong", nhan_Vien.luong);
                    command.Parameters.AddWithValue("@id", nhan_Vien.id);

                connection.Open();
                    command.ExecuteNonQuery();
                }
            
        }
        public void deletenhanvien(int id)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "DELETE FROM nhan_vien WHERE id = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public nhan_vien check_thongtin_login(string email, string matkhau)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                connection.Open();
                string CheckThongTinDangNhap = "select * from nhan_vien where email = @email and matkhau = @matkhau ";
                using (SqlCommand command = new SqlCommand(CheckThongTinDangNhap, connection))
                {
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@matkhau", matkhau);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nhan_vien khach_Hang = new nhan_vien()
                            {
                                id = Convert.ToInt32(reader["id"]),
                                hovaten = reader["hovaten"].ToString(),
                                diachi = reader["diachi"].ToString(),
                                dienthoai = reader["dienthoai"].ToString(),
                                email = reader["email"].ToString(),
                                matkhau = reader["matkhau"].ToString(),
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
    }
}
