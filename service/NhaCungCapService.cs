using models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    public class NhaCungCapService
    {

        public List<nha_cung_cap> GetAllNhaCungCap()
        {
            var suppliers = new List<nha_cung_cap>();
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                var command = new SqlCommand("SELECT id, tennhancungcap, diachi, dienthoai FROM nha_cung_cap", connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        suppliers.Add(new nha_cung_cap
                        {
                            id = (int)reader["id"],
                            tennhancungcap = (string)reader["tennhancungcap"],
                            diachi = (string)reader["diachi"],
                            dienthoai = (string)reader["dienthoai"]
                        });
                    }
                }
            }
            return suppliers;
        }

        public nha_cung_cap GetById(int id)
        {
            nha_cung_cap supplier = null;
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                var command = new SqlCommand("SELECT id, tennhancungcap, diachi, dienthoai FROM nha_cung_cap WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        supplier = new nha_cung_cap
                        {
                            id = (int)reader["id"],
                            tennhancungcap = (string)reader["tennhancungcap"],
                            diachi = (string)reader["diachi"],
                            dienthoai = (string)reader["dienthoai"]
                        };
                    }
                }
            }
            return supplier;
        }

        public void Add(nha_cung_cap supplier)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())

            {
                var command = new SqlCommand("INSERT INTO nha_cung_cap (tennhancungcap, diachi, dienthoai) VALUES (@tennhancungcap, @diachi, @dienthoai)", connection);
                command.Parameters.AddWithValue("@tennhancungcap", supplier.tennhancungcap);
                command.Parameters.AddWithValue("@diachi", supplier.diachi);
                command.Parameters.AddWithValue("@dienthoai", supplier.dienthoai);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(nha_cung_cap supplier)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())

            {
                var command = new SqlCommand("UPDATE nha_cung_cap SET tennhancungcap = @tennhancungcap, diachi = @diachi, dienthoai = @dienthoai WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", supplier.id);
                command.Parameters.AddWithValue("@tennhancungcap", supplier.tennhancungcap);
                command.Parameters.AddWithValue("@diachi", supplier.diachi);
                command.Parameters.AddWithValue("@dienthoai", supplier.dienthoai);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                var command = new SqlCommand("DELETE FROM nha_cung_cap WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}

