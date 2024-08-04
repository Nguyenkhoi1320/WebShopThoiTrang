using models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    public class don_hangService
    {
        public int ThemDonHang(don_hang donHang)
        {
            int idDonHangMoi = 0;
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "INSERT INTO don_hang (ngaytao,trangthai, khachhang_id) VALUES (@NgayTao,@trangthai, @KhachHangID); SELECT SCOPE_IDENTITY();";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@NgayTao", donHang.ngaytao);
                command.Parameters.AddWithValue("@KhachHangID", donHang.khachhang_id);
                command.Parameters.AddWithValue("@trangthai", donHang.trangthai);

                connection.Open();
                idDonHangMoi = Convert.ToInt32(command.ExecuteScalar());
            }
            return idDonHangMoi;
        }
        public int countsolandatphongtheokhachhangid(int khachhang_id)
        {
            int count = 0;
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM don_hang WHERE khachhang_id = @khachhang_id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@khachhang_id", khachhang_id);
                    count = (int)command.ExecuteScalar();
                }
            }
            return count;
        }
        public List<don_hang> LayDanhSachDonHang()
        {
            List<don_hang> danhSachDonHang = new List<don_hang>();
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "select * from don_hang";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    don_hang donHang = new don_hang
                    {
                        id = Convert.ToInt32(reader["id"]),
                        ngaytao = Convert.ToDateTime(reader["ngaytao"]),
                        khachhang_id = Convert.ToInt32(reader["khachhang_id"]),
                        trangthai = reader["trangthai"].ToString(),
                    };
                    danhSachDonHang.Add(donHang);
                }
            }
            return danhSachDonHang;
        }
        public don_hang GetById(int id)
        {
            don_hang donHang = null;

            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "SELECT * FROM don_hang WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    donHang = new don_hang
                    {
                        id = Convert.ToInt32(reader["id"]),
                        ngaytao = Convert.ToDateTime(reader["ngaytao"]),
                        khachhang_id = Convert.ToInt32(reader["khachhang_id"]),
                        trangthai = reader["trangthai"].ToString(),
                        // You can add more properties if needed
                    };
                }
            }

            return donHang;
        }
        public void Update(don_hang donHang)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "UPDATE don_hang SET ngaytao = @ngaytao, khachhang_id = @khachhang_id, trangthai = @trangthai WHERE id = @id";

                SqlCommand command = new SqlCommand(query, connection);

                // Add parameters to the SQL query
                command.Parameters.AddWithValue("@ngaytao", donHang.ngaytao);
                command.Parameters.AddWithValue("@khachhang_id", donHang.khachhang_id);
                command.Parameters.AddWithValue("@trangthai", donHang.trangthai);
                command.Parameters.AddWithValue("@id", donHang.id);

                connection.Open();

                // Execute the UPDATE query
                int rowsAffected = command.ExecuteNonQuery();

                // Optionally, you can check rowsAffected for logging or validation purposes
                // For void method, no need to return anything
            }
        }

        public List<don_hang> LayDanhSachDonHangtheoidkhachhang(int khachhang_id)
        {
            List<don_hang> danhSachDonHang = new List<don_hang>();
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "select * from don_hang where khachhang_id = @khachhang_id ORDER BY id DESC";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@khachhang_id", khachhang_id);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    don_hang donHang = new don_hang
                    {
                        id = Convert.ToInt32(reader["id"]),
                        ngaytao = Convert.ToDateTime(reader["ngaytao"]),
                        khachhang_id = Convert.ToInt32(reader["khachhang_id"]),
                        trangthai = reader["trangthai"].ToString(),
                    };
                    danhSachDonHang.Add(donHang);
                }
            }
            return danhSachDonHang;
        }
        public void GuiEmail(khach_hang khachHang, string content)
        {
            try
            {
                string fromEmail = "nguyenkhoi010300@gmail.com";
                string password = "fhjz hfjm echy xvda";
                string toEmail = khachHang.email;
                MailMessage message = new MailMessage(fromEmail, toEmail);
                message.Subject = "Bạn đã đặt hàng thành công";
                message.IsBodyHtml = true;
                message.Body = "<html><body><h2>Đơn đặt hàng của bạn:</h2>" + content + "</body></html>";
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(fromEmail, password);
                smtpClient.EnableSsl = true;
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi: " + ex.Message);
            }
        }
    }
}