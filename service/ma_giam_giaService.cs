using System.Data.SqlClient;
using System.Collections.Generic;
using models;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace service
{
    public class ma_giam_giaService
    {
        public void ThemMaGiamGia(ma_giam_gia maGiamGia)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "INSERT INTO ma_giam_gia (magiamgia, mota, phantramgiamgia, solansudungtoida, solandasudung, ngaybatdau, ngayketthuc, quydinggiamgia_id) " +
                               "VALUES (@magiamgia, @mota, @phantramgiamgia, @solansudungtoida, @solandasudung, @ngaybatdau, @ngayketthuc, @quydinggiamgia_id)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@magiamgia", maGiamGia.magiamgia);
                command.Parameters.AddWithValue("@mota", maGiamGia.mota);
                command.Parameters.AddWithValue("@phantramgiamgia", maGiamGia.phantramgiamgia);
                command.Parameters.AddWithValue("@solansudungtoida", maGiamGia.solansudungtoida);
                command.Parameters.AddWithValue("@solandasudung", maGiamGia.solandasudung);
                command.Parameters.AddWithValue("@ngaybatdau", maGiamGia.ngaybatdau);
                command.Parameters.AddWithValue("@ngayketthuc", maGiamGia.ngayketthuc);
                command.Parameters.AddWithValue("@quydinggiamgia_id", maGiamGia.quydinggiamgia_id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void XoaMaGiamGia(int id)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "DELETE FROM ma_giam_gia WHERE id = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void SuaMaGiamGia(ma_giam_gia maGiamGia)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "UPDATE ma_giam_gia SET magiamgia = @magiamgia, mota = @mota, phantramgiamgia = @phantramgiamgia, " +
                               "solansudungtoida = @solansudungtoida, solandasudung = @solandasudung, ngaybatdau = @ngaybatdau, " +
                               "ngayketthuc = @ngayketthuc, quydinggiamgia_id = @quydinggiamgia_id WHERE id = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@magiamgia", maGiamGia.magiamgia);
                command.Parameters.AddWithValue("@mota", maGiamGia.mota);
                command.Parameters.AddWithValue("@phantramgiamgia", maGiamGia.phantramgiamgia);
                command.Parameters.AddWithValue("@solansudungtoida", maGiamGia.solansudungtoida);
                command.Parameters.AddWithValue("@solandasudung", maGiamGia.solandasudung);
                command.Parameters.AddWithValue("@ngaybatdau", maGiamGia.ngaybatdau);
                command.Parameters.AddWithValue("@ngayketthuc", maGiamGia.ngayketthuc);
                command.Parameters.AddWithValue("@quydinggiamgia_id", maGiamGia.quydinggiamgia_id);
                command.Parameters.AddWithValue("@id", maGiamGia.id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public ma_giam_gia LayMaGiamGiaTheoID(int id)
        {
            ma_giam_gia maGiamGia = null;

            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "SELECT * FROM ma_giam_gia WHERE id = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    maGiamGia = new ma_giam_gia();
                    maGiamGia.id = Convert.ToInt32(reader["id"]);
                    maGiamGia.magiamgia = reader["magiamgia"].ToString();
                    maGiamGia.mota = reader["mota"].ToString();
                    maGiamGia.phantramgiamgia = Convert.ToDecimal(reader["phantramgiamgia"]);
                    maGiamGia.solansudungtoida = Convert.ToInt32(reader["solansudungtoida"]);
                    maGiamGia.solandasudung = Convert.ToInt32(reader["solandasudung"]);
                    maGiamGia.ngaybatdau = Convert.ToDateTime(reader["ngaybatdau"]);
                    maGiamGia.ngayketthuc = Convert.ToDateTime(reader["ngayketthuc"]);
                    maGiamGia.quydinggiamgia_id = Convert.ToInt32(reader["quydinggiamgia_id"]);
                }

                reader.Close();
            }

            return maGiamGia;
        }

        public List<ma_giam_gia> LayDanhSachMaGiamGia()
        {
            List<ma_giam_gia> danhSachMaGiamGia = new List<ma_giam_gia>();

            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "SELECT * FROM ma_giam_gia";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ma_giam_gia maGiamGia = new ma_giam_gia();
                    maGiamGia.id = Convert.ToInt32(reader["id"]);
                    maGiamGia.magiamgia = reader["magiamgia"].ToString();
                    maGiamGia.mota = reader["mota"].ToString();
                    maGiamGia.phantramgiamgia = Convert.ToDecimal(reader["phantramgiamgia"]);
                    maGiamGia.solansudungtoida = Convert.ToInt32(reader["solansudungtoida"]);
                    maGiamGia.solandasudung = Convert.ToInt32(reader["solandasudung"]);
                    maGiamGia.ngaybatdau = Convert.ToDateTime(reader["ngaybatdau"]);
                    maGiamGia.ngayketthuc = Convert.ToDateTime(reader["ngayketthuc"]);
                    maGiamGia.quydinggiamgia_id = Convert.ToInt32(reader["quydinggiamgia_id"]);

                    danhSachMaGiamGia.Add(maGiamGia);
                }

                reader.Close();
            }

            return danhSachMaGiamGia;
        }
        public int countkhachhangsu_dung_ma_giam_gia(int khachhang_id, int magiamgia_id)
        {
            int count = 0;
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM su_dung_ma_giam_gia WHERE magiamgia_id = @magiamgia_id AND donhang_id IN (SELECT id FROM don_hang WHERE khachhang_id = @khachhang_id)\r\n";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@khachhang_id", khachhang_id);
                    command.Parameters.AddWithValue("@magiamgia_id", magiamgia_id);
                    count = (int)command.ExecuteScalar();
                }
            }
            return count;
        }
        public ma_giam_gia GetMaGiamGiaByMaGiamGia(string magiamgia)
        {
            ma_giam_gia maGiamGia = null;

            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "SELECT * FROM ma_giam_gia WHERE magiamgia = @magiamgia";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@magiamgia", magiamgia);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    maGiamGia = new ma_giam_gia();
                    maGiamGia.id = Convert.ToInt32(reader["id"]);
                    maGiamGia.magiamgia = reader["magiamgia"].ToString();
                    maGiamGia.mota = reader["mota"].ToString();
                    maGiamGia.phantramgiamgia = Convert.ToDecimal(reader["phantramgiamgia"]);
                    maGiamGia.solansudungtoida = Convert.ToInt32(reader["solansudungtoida"]);
                    maGiamGia.solandasudung = Convert.ToInt32(reader["solandasudung"]);
                    maGiamGia.ngaybatdau = Convert.ToDateTime(reader["ngaybatdau"]);
                    maGiamGia.ngayketthuc = Convert.ToDateTime(reader["ngayketthuc"]);
                    maGiamGia.quydinggiamgia_id = Convert.ToInt32(reader["quydinggiamgia_id"]);
                }

                reader.Close();
            }

            return maGiamGia;
        }
        public void GuiEmail(khach_hang khachHang, string magiamgia)
        {
            try
            {
                string fromEmail = "vvc132003@gmail.com";
                string password = "bzcaumaekzuvwlia";
                string toEmail = khachHang.email;
                MailMessage message = new MailMessage(fromEmail, toEmail);
                message.Subject = "Bạn đã đặt hàng thành công";
                StringBuilder bodyBuilder = new StringBuilder();
                bodyBuilder.AppendLine($"Mã giảm giá: {magiamgia}");
                message.Body = bodyBuilder.ToString();
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
