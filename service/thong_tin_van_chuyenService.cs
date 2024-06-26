using models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    public class thong_tin_van_chuyenService
    {
        public void ThemThongTinVanChuyen(thong_tin_van_chuyen thongTinVanChuyen)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "INSERT INTO thong_tin_van_chuyen (diachi, phivanchuyen, donhang_id) " +
                               "VALUES (@diachi, @phivanchuyen, @donhang_id)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@diachi", thongTinVanChuyen.diachi);
                command.Parameters.AddWithValue("@phivanchuyen", thongTinVanChuyen.phivanchuyen);
                command.Parameters.AddWithValue("@donhang_id", thongTinVanChuyen.donhang_id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void XoaThongTinVanChuyen(int id)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "DELETE FROM thong_tin_van_chuyen WHERE id = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void SuaThongTinVanChuyen(thong_tin_van_chuyen thongTinVanChuyen)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "UPDATE thong_tin_van_chuyen SET diachi = @diachi, phivanchuyen = @phivanchuyen, " +
                               "donhang_id = @donhang_id WHERE id = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@diachi", thongTinVanChuyen.diachi);
                command.Parameters.AddWithValue("@phivanchuyen", thongTinVanChuyen.phivanchuyen);
                command.Parameters.AddWithValue("@donhang_id", thongTinVanChuyen.donhang_id);
                command.Parameters.AddWithValue("@id", thongTinVanChuyen.id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public thong_tin_van_chuyen LayThongTinVanChuyenTheoID(int id)
        {
            thong_tin_van_chuyen thongTinVanChuyen = null;

            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "SELECT * FROM thong_tin_van_chuyen WHERE id = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    thongTinVanChuyen = new thong_tin_van_chuyen();
                    thongTinVanChuyen.id = Convert.ToInt32(reader["id"]);
                    thongTinVanChuyen.diachi = reader["diachi"].ToString();
                    thongTinVanChuyen.phivanchuyen = Convert.ToDecimal(reader["phivanchuyen"]);
                    thongTinVanChuyen.donhang_id = Convert.ToInt32(reader["donhang_id"]);
                }

                reader.Close();
            }

            return thongTinVanChuyen;
        }

        public List<thong_tin_van_chuyen> LayDanhSachThongTinVanChuyen()
        {
            List<thong_tin_van_chuyen> danhSachThongTinVanChuyen = new List<thong_tin_van_chuyen>();

            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "SELECT * FROM thong_tin_van_chuyen";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    thong_tin_van_chuyen thongTinVanChuyen = new thong_tin_van_chuyen();
                    thongTinVanChuyen.id = Convert.ToInt32(reader["id"]);
                    thongTinVanChuyen.diachi = reader["diachi"].ToString();
                    thongTinVanChuyen.phivanchuyen = Convert.ToDecimal(reader["phivanchuyen"]);
                    thongTinVanChuyen.donhang_id = Convert.ToInt32(reader["donhang_id"]);
                    danhSachThongTinVanChuyen.Add(thongTinVanChuyen);
                }

                reader.Close();
            }

            return danhSachThongTinVanChuyen;
        }
    }
}