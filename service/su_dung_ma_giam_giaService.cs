using models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    public class su_dung_ma_giam_giaService
    {
        public void Themsu_dung_ma_giam_gia(su_dung_ma_giam_gia su_dung_ma_giam_gia)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "INSERT INTO su_dung_ma_giam_gia (magiamgia_id, donhang_id, ngaysudung) VALUES (@magiamgia_id, @donhang_id, @ngaysudung)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@magiamgia_id", su_dung_ma_giam_gia.magiamgia_id);
                command.Parameters.AddWithValue("@donhang_id", su_dung_ma_giam_gia.donhang_id);
                command.Parameters.AddWithValue("@ngaysudung", su_dung_ma_giam_gia.ngaysudung);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Xoasu_dung_ma_giam_gia(int id)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "DELETE FROM su_dung_ma_giam_gia WHERE id = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void CapNhatsu_dung_ma_giam_gia(su_dung_ma_giam_gia su_dung_ma_giam_gia)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "UPDATE su_dung_ma_giam_gia SET magiamgia_id = @magiamgia_id, donhang_id = @donhang_id, ngaysudung = @ngaysudung WHERE id = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@magiamgia_id", su_dung_ma_giam_gia.magiamgia_id);
                command.Parameters.AddWithValue("@donhang_id", su_dung_ma_giam_gia.donhang_id);
                command.Parameters.AddWithValue("@ngaysudung", su_dung_ma_giam_gia.ngaysudung);
                command.Parameters.AddWithValue("@id", su_dung_ma_giam_gia.id);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public List<su_dung_ma_giam_gia> GetAllsu_dung_ma_giam_gia()
        {
            List<su_dung_ma_giam_gia> listsu_dung_ma_giam_gia = new List<su_dung_ma_giam_gia>();

            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "SELECT * FROM su_dung_ma_giam_gia";

                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        su_dung_ma_giam_gia su_dung_ma_giam_gia = new su_dung_ma_giam_gia
                        {
                            id = Convert.ToInt32(reader["id"]),
                            magiamgia_id = Convert.ToInt32(reader["magiamgia_id"]),
                            donhang_id = Convert.ToInt32(reader["donhang_id"]),
                            ngaysudung = Convert.ToDateTime(reader["ngaysudung"])
                        };

                        listsu_dung_ma_giam_gia.Add(su_dung_ma_giam_gia);
                    }
                }
            }

            return listsu_dung_ma_giam_gia;
        }

        public su_dung_ma_giam_gia Getsu_dung_ma_giam_giaById(int id)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "SELECT * FROM su_dung_ma_giam_gia WHERE id = @id";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        su_dung_ma_giam_gia su_dung_ma_giam_gia = new su_dung_ma_giam_gia
                        {
                            id = Convert.ToInt32(reader["id"]),
                            magiamgia_id = Convert.ToInt32(reader["magiamgia_id"]),
                            donhang_id = Convert.ToInt32(reader["donhang_id"]),
                            ngaysudung = Convert.ToDateTime(reader["ngaysudung"])
                        };

                        return su_dung_ma_giam_gia;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public su_dung_ma_giam_gia Getsu_dung_ma_giam_giaBydonhang_id(int donhang_id)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "SELECT * FROM su_dung_ma_giam_gia WHERE donhang_id  = @donhang_id  ";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@donhang_id ", donhang_id);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        su_dung_ma_giam_gia su_dung_ma_giam_gia = new su_dung_ma_giam_gia
                        {
                            id = Convert.ToInt32(reader["id"]),
                            magiamgia_id = Convert.ToInt32(reader["magiamgia_id"]),
                            donhang_id = Convert.ToInt32(reader["donhang_id"]),
                            ngaysudung = Convert.ToDateTime(reader["ngaysudung"])
                        };

                        return su_dung_ma_giam_gia;
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
