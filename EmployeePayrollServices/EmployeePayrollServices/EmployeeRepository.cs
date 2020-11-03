// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmployeeRepository.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Praveen Kumar Upadhyay"/>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EmployeePayrollServices
{
    /// <summary>
    /// Class to execute the ado.net query implementation on the underlying sql database
    /// Using the Data.SqlClient package to establish connections
    /// Using Sql Connection as records are limited in number
    /// </summary>
    public class EmployeeRepository
    {
        /// <summary>
        /// Specifying the connection string from the sql server connection
        /// </summary>
        public static string connectionString = @"Data Source=LAPTOP-EIJJR8OV\TEW_SQLEXPRESS;Initial Catalog = payroll_services; User ID=PraveenUpadhyay;Password=aircel1234@;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        /// <summary>
        /// Establishing the connection using the Sql Connection
        /// </summary>
        public SqlConnection connection = new SqlConnection(connectionString);
        /// <summary>
        /// Checking for the validity of the connection
        /// </summary>
        public void EnsureDataBaseConnection()
        {
            using(connection)
            {
                Console.WriteLine("The Connection is created");
            }
        }
    }
}
