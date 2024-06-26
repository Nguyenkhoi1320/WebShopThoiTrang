using models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    public class quy_dinh_giam_giaService
    {
        public void ThemQuyDinh(quy_dinh_giam_gia quydinh)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "INSERT INTO quy_dinh_giam_gia (solandathang, tongtientoithieu, phantramgiamgia, ngaythemquydinh) " +
                               "VALUES (@solandathang, @tongtientoithieu, @phantramgiamgia, @ngaythemquydinh)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@solandathang", quydinh.solandathang);
                command.Parameters.AddWithValue("@tongtientoithieu", quydinh.tongtientoithieu);
                command.Parameters.AddWithValue("@phantramgiamgia", quydinh.phantramgiamgia);
                command.Parameters.AddWithValue("@ngaythemquydinh", quydinh.ngaythemquydinh);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public List<quy_dinh_giam_gia> LayDanhSachQuyDinh()
        {
            List<quy_dinh_giam_gia> danhSachQuyDinh = new List<quy_dinh_giam_gia>();

            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "SELECT * FROM quy_dinh_giam_gia"; // Thay thế "TenBangQuyDinh" bằng tên bảng của bạn

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    quy_dinh_giam_gia quydinh = new quy_dinh_giam_gia();
                    quydinh.id = Convert.ToInt32(reader["id"]);
                    quydinh.solandathang = Convert.ToInt32(reader["solandathang"]);
                    quydinh.tongtientoithieu = Convert.ToDecimal(reader["tongtientoithieu"]);
                    quydinh.phantramgiamgia = Convert.ToDecimal(reader["phantramgiamgia"]);
                    quydinh.ngaythemquydinh = Convert.ToDateTime(reader["ngaythemquydinh"]);

                    danhSachQuyDinh.Add(quydinh);
                }

                reader.Close();
            }

            return danhSachQuyDinh;
        }
        public void XoaQuyDinh(int id)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "DELETE FROM quy_dinh_giam_gia WHERE id = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void SuaQuyDinh(quy_dinh_giam_gia quydinh)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "UPDATE quy_dinh_giam_gia SET solandathang = @solandathang, " +
                               "tongtientoithieu = @tongtientoithieu, phantramgiamgia = @phantramgiamgia, " +
                               "ngaythemquydinh = @ngaythemquydinh WHERE id = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@solandathang", quydinh.solandathang);
                command.Parameters.AddWithValue("@tongtientoithieu", quydinh.tongtientoithieu);
                command.Parameters.AddWithValue("@phantramgiamgia", quydinh.phantramgiamgia);
                command.Parameters.AddWithValue("@ngaythemquydinh", quydinh.ngaythemquydinh);
                command.Parameters.AddWithValue("@id", quydinh.id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public quy_dinh_giam_gia LayQuyDinhTheoID(int id)
        {
            quy_dinh_giam_gia quydinh = null;

            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "SELECT * FROM quy_dinh_giam_gia WHERE id = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    quydinh = new quy_dinh_giam_gia();
                    quydinh.id = Convert.ToInt32(reader["id"]);
                    quydinh.solandathang = Convert.ToInt32(reader["solandathang"]);
                    quydinh.tongtientoithieu = Convert.ToDecimal(reader["tongtientoithieu"]);
                    quydinh.phantramgiamgia = Convert.ToDecimal(reader["phantramgiamgia"]);
                    quydinh.ngaythemquydinh = Convert.ToDateTime(reader["ngaythemquydinh"]);
                }

                reader.Close();
            }

            return quydinh;
        }
        public quy_dinh_giam_gia GetQuyDinhGiasolandatphong(int solandathang)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                connection.Open();
                string query = "SELECT * FROM quy_dinh_giam_gia WHERE solandathang = @solandathang";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@solandathang", solandathang);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    quy_dinh_giam_gia quyDinhGiamGia = new quy_dinh_giam_gia()
                    {
                        id = Convert.ToInt32(reader["id"]),
                        solandathang = Convert.ToInt32(reader["solandathang"]),
                        tongtientoithieu = Convert.ToInt32(reader["tongtientoithieu"]),
                        phantramgiamgia = Convert.ToDecimal(reader["phantramgiamgia"]),
                        ngaythemquydinh = Convert.ToDateTime(reader["ngaythemquydinh"]),
                    };
                    return quyDinhGiamGia;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
