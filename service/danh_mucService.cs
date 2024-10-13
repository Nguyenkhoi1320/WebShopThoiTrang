using models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace service
{
    public class danh_mucService
    {
        private string connectionString = "your_connection_string"; // Replace with your actual connection string

        // Create a new category (Insert)
        public void Create(danh_muc danhMuc)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "INSERT INTO danhmuc (tencategory, mota, created_at) VALUES (@tencategory, @mota, @created_at)";
                SqlCommand cmd = new SqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@tencategory", danhMuc.TenCategory);
                cmd.Parameters.AddWithValue("@mota", (object)danhMuc.MoTa ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@created_at", DateTime.Now);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Read all categories (Select)
        public List<danh_muc> GetAll()
        {
            List<danh_muc> danhMucList = new List<danh_muc>();

            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "SELECT id, tencategory, mota, created_at, updated_at FROM danhmuc";
                SqlCommand cmd = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    danh_muc danhMuc = new danh_muc
                    {
                        Id = (int)reader["id"],
                        TenCategory = reader["tencategory"].ToString(),
                        MoTa = reader["mota"] != DBNull.Value ? reader["mota"].ToString() : null,
                        CreatedAt = (DateTime)reader["created_at"],
                        UpdatedAt = reader["updated_at"] != DBNull.Value ? (DateTime?)reader["updated_at"] : null
                    };

                    danhMucList.Add(danhMuc);
                }
            }

            return danhMucList;
        }

        // Read a single category by ID (Select by ID)
        public danh_muc GetById(int id)
        {
            danh_muc danhMuc = null;

            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "SELECT id, tencategory, mota, created_at, updated_at FROM danhmuc WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id", id);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    danhMuc = new danh_muc
                    {
                        Id = (int)reader["id"],
                        TenCategory = reader["tencategory"].ToString(),
                        MoTa = reader["mota"] != DBNull.Value ? reader["mota"].ToString() : null,
                        CreatedAt = (DateTime)reader["created_at"],
                        UpdatedAt = reader["updated_at"] != DBNull.Value ? (DateTime?)reader["updated_at"] : null
                    };
                }
            }

            return danhMuc;
        }

        // Update an existing category (Update)
        public void Update(danh_muc danhMuc)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE danhmuc SET tencategory = @tencategory, mota = @mota, updated_at = @updated_at WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@tencategory", danhMuc.TenCategory);
                cmd.Parameters.AddWithValue("@mota", (object)danhMuc.MoTa ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@updated_at", DateTime.Now);
                cmd.Parameters.AddWithValue("@id", danhMuc.Id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Delete a category (Delete)
        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM danhmuc WHERE id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
