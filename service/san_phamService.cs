using models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace service
{
    public class san_phamService
    {
        public List<san_pham> GetAllSanPham()
        {
            List<san_pham> productList = new List<san_pham>();
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string sqlQuery = "SELECT * FROM san_pham ORDER BY giaban DESC";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            san_pham product = new san_pham
                            {
                                id = Convert.ToInt32(reader["id"]),
                                tensanpham = reader["tensanpham"].ToString(),
                                giaban = Convert.ToDecimal(reader["giaban"]),
                                mota = reader["mota"].ToString(),
                                anh = reader["anh"].ToString(),
                                soluongcon = Convert.ToInt32(reader["soluongcon"]),
                                nhacungcap_id = Convert.ToInt32(reader["nhacungcap_id"])
                            };
                            productList.Add(product);
                        }
                    }
                }
            }
            return productList;
        }

        public san_pham GetSanPhamById(int productId)
        {
            san_pham product = null;

            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string sqlQuery = "SELECT * FROM san_pham WHERE id = @ProductId";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            product = new san_pham
                            {
                                id = Convert.ToInt32(reader["id"]),
                                tensanpham = reader["tensanpham"].ToString(),
                                giaban = Convert.ToDecimal(reader["giaban"]),
                                mota = reader["mota"].ToString(),
                                anh = reader["anh"].ToString(),
                                soluongcon = Convert.ToInt32(reader["soluongcon"]),
                                nhacungcap_id = Convert.ToInt32(reader["nhacungcap_id"])
                            };
                        }
                    }
                }
            }

            return product;
        }

        public void AddSanPham(san_pham product)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string sqlQuery = "INSERT INTO san_pham (tensanpham, giaban, mota, anh, soluongcon, nhacungcap_id) " +
                                  "VALUES (@tensanpham, @giaban, @mota, @anh, @soluongcon, @nhacungcap_id)";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@tensanpham", product.tensanpham);
                    command.Parameters.AddWithValue("@giaban", product.giaban);
                    command.Parameters.AddWithValue("@mota", product.mota);
                    command.Parameters.AddWithValue("@anh", product.anh);
                    command.Parameters.AddWithValue("@soluongcon", product.soluongcon);
                    command.Parameters.AddWithValue("@nhacungcap_id", product.nhacungcap_id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateSanPham(san_pham product)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string sqlQuery = "UPDATE san_pham SET " +
                                  "tensanpham = @tensanpham, giaban = @giaban, mota = @mota, anh = @anh, soluongcon = @soluongcon, nhacungcap_id = @nhacungcap_id " +
                                  "WHERE id = @id";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@id", product.id);
                    command.Parameters.AddWithValue("@tensanpham", product.tensanpham);
                    command.Parameters.AddWithValue("@giaban", product.giaban);
                    command.Parameters.AddWithValue("@mota", product.mota);
                    command.Parameters.AddWithValue("@anh", product.anh);
                    command.Parameters.AddWithValue("@soluongcon", product.soluongcon);
                    command.Parameters.AddWithValue("@nhacungcap_id", product.nhacungcap_id);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteSanPham(int productId)
        {
            using (SqlConnection connection = DBUtils.GetDBConnection())
            {
                string sqlQuery = "DELETE FROM san_pham WHERE id = @ProductId";

                using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                {
                    command.Parameters.AddWithValue("@ProductId", productId);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
