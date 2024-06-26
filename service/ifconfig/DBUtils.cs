using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace service
{
    public class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {
            string datasource = @"LAPCN-KhoiN\SQLEXPRESS";
            string database = "Quanlybanhang";
            string username = "sa";
            string password = "admin123";
            return DBSQLServerUtils.GetDBConnection(datasource, database, username, password);
        }
    }
}