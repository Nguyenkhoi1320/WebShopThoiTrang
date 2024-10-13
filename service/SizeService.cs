using models;
using System;
using System.Data.SqlClient;
using System.Drawing;

namespace service
{
    public class SizeService
    {
        // CREATE a new Size
        public async Task<bool> CreateSizeAsync(sizes size)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "INSERT INTO Size (tenSize, sanPhamId) VALUES (@tenSize, @sanPhamId)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@tenSize", size.tenSize);
                command.Parameters.AddWithValue("@sanPhamId", size.sanPhamId);

                connection.Open();
                int result = await command.ExecuteNonQueryAsync();
                return result > 0;
            }
        }

        // READ: Get all sizes
        public async Task<List<sizes>> GetAllSizesAsync()
        {
            List<sizes> sizes = new List<sizes>();

            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "SELECT * FROM Size";
                SqlCommand command = new SqlCommand(query, connection);

                connection.Open();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    sizes.Add(new sizes
                    {
                        id = (int)reader["id"],
                        tenSize = reader["tenSize"].ToString(),
                        sanPhamId = (int)reader["sanPhamId"]
                    });
                }
            }

            return sizes;
        }

        // READ: Get a size by id
        public async Task<sizes> GetSizeByIdAsync(int id)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "SELECT * FROM Size WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                SqlDataReader reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    return new sizes
                    {
                        id = (int)reader["id"],
                        tenSize = reader["tenSize"].ToString(),
                        sanPhamId = (int)reader["sanPhamId"]
                    };
                }

                return null;
            }
        }

        // UPDATE: Update an existing Size
        public async Task<bool> UpdateSizeAsync(sizes size)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "UPDATE Size SET tenSize = @tenSize, sanPhamId = @sanPhamId WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", size.id);
                command.Parameters.AddWithValue("@tenSize", size.tenSize);
                command.Parameters.AddWithValue("@sanPhamId", size.sanPhamId);

                connection.Open();
                int result = await command.ExecuteNonQueryAsync();
                return result > 0;
            }
        }

        // DELETE: Delete a size by id
        public async Task<bool> DeleteSizeAsync(int id)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string query = "DELETE FROM Size WHERE id = @id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                connection.Open();
                int result = await command.ExecuteNonQueryAsync();
                return result > 0;
            }
        }
    }
}
