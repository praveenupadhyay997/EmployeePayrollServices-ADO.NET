// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmployeeRepository.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Praveen Kumar Upadhyay"/>
// --------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
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
        public static SqlConnection connection = new SqlConnection(connectionString);
        /// <summary>
        /// UC1--Checking for the validity of the connection
        /// </summary>
        public void EnsureDataBaseConnection()
        {
            connection.Open();
            using (connection)
            {
                Console.WriteLine("The Connection is created");
            }
           connection.Close();
        }
        /// <summary>
        /// UC2-- Getting all the stored records in the employee payroll services table by fetching all the records
        /// </summary>
        public void GetAllEmployeesRecords()
        {
            /// Creating the employee model class object
            EmployeeModel employeeObject = new EmployeeModel();
            try
            {
                using (connection)
                {
                    /// Query to get all the data from the table
                    string query = @"select * from dbo.employee_payroll_services";
                    /// Impementing the command on the connection fetched database table
                    SqlCommand command = new SqlCommand(query, connection);
                    /// Opening the connection to start mapping
                    connection.Open();
                    /// executing the sql data reader to fetch the records
                    SqlDataReader reader = command.ExecuteReader();
                    /// executing for not null
                    if (reader.HasRows)
                    {
                        /// Moving to the next record from the table
                        /// Mapping the data to the employee model class object
                        while (reader.Read())
                        {
                            employeeObject.EmployeeID = reader.GetInt32(0);
                            employeeObject.EmployeeName = reader.GetString(1);
                            employeeObject.BasicPay = reader.GetDouble(2);
                            employeeObject.StartDate = reader.GetDateTime(3);
                            employeeObject.PhoneNumber = reader.GetInt64(4);
                            employeeObject.Address = reader.GetString(5);
                            employeeObject.Department = reader.GetString(6);
                            employeeObject.Gender = reader.GetString(7);
                            employeeObject.Deductions = reader.GetDouble(8);
                            employeeObject.TaxablePay = reader.GetDouble(9);
                            employeeObject.Tax = reader.GetDouble(10);
                            employeeObject.NetPay = reader.GetDouble(11);
                            Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}",
                                employeeObject.EmployeeID, employeeObject.EmployeeName, employeeObject.Gender,
                                employeeObject.Address, employeeObject.BasicPay, employeeObject.StartDate, 
                                employeeObject.PhoneNumber, employeeObject.Address, employeeObject.Department, 
                                employeeObject.Deductions, employeeObject.TaxablePay, employeeObject.Tax, employeeObject.NetPay);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found");
                    }
                    reader.Close();
                }
            }
            /// Catching the null record exception
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            /// Alway ensuring the closing of the connection
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// Function to update the basic pay of the employee
        /// </summary>
        /// <param name="empName"></param>
        /// <returns></returns>
        public bool UpdateDataForEmployee(string empName)
        {
            try
            {
                /// Using the connection established
                using (connection)
                {
                    /// Opening the connection
                    connection.Open();
                    /// Update query  for the table and binding with the parameter passed
                    string query = @"update dbo.employee_payroll_services set BasicPay= 30000 where EmployeeName = @parameter";
                    /// Impementing the command on the connection fetched database table
                    SqlCommand command = new SqlCommand(query, connection);
                    /// Binding the parameter to the formal parameters
                    command.Parameters.AddWithValue("@parameter", empName);
                    /// Storing the result of the executed query
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    if (result != 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            /// Catching any type of exception generated during the run time
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
