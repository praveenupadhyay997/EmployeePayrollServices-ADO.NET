using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EmployeePayrollServices
{
    public class DBConnection
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetConnection()
        {
            /// <summary>
            /// Specifying the connection string from the sql server connection
            /// </summary>
            string connectionString = @"Data Source=LAPTOP-EIJJR8OV\TEW_SQLEXPRESS;Initial Catalog = payroll_services; User ID=PraveenUpadhyay;Password=aircel1234@;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            /// Using the underlying sql server to establish the connection
            SqlConnection dbConnection = new SqlConnection(connectionString);
            return dbConnection;
        }
    }
}
