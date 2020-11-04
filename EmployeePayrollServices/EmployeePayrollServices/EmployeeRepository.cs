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
        /// For ensuring the established connection using the Sql Connection specifying the property
        /// </summary>
        public static SqlConnection connectionToServer { get; set; }
        /// <summary>
        /// UC1--Checking for the validity of the connection
        /// </summary>
        public void EnsureDataBaseConnection()
        {
            /// Creates a new connection for every method to avoid "ConnectionString property not initialized" exception
            DBConnection dbc = new DBConnection();
            /// Calling the Get connection method to establish the connection to the Sql Server
            connectionToServer = dbc.GetConnection();
            using (connectionToServer)
            {
                Console.WriteLine("The Connection is created");
            }
           connectionToServer.Close();
        }
        /// <summary>
        /// UC2-- Getting all the stored records in the employee payroll services table by fetching all the records
        /// </summary>
        public void GetAllEmployeesRecords()
        {
            /// Creates a new connection for every method to avoid "ConnectionString property not initialized" exception
            DBConnection dbc = new DBConnection();
            /// Calling the Get connection method to establish the connection to the Sql Server
            connectionToServer = dbc.GetConnection();
            /// Creating the employee model class object
            EmployeeModel employeeObject = new EmployeeModel();
            try
            {
                using (connectionToServer)
                {
                    /// Query to get all the data from the table
                    string query = @"select * from dbo.employee_payroll_services";
                    /// Impementing the command on the connection fetched database table
                    SqlCommand command = new SqlCommand(query, connectionToServer);
                    /// Opening the connection to start mapping
                    connectionToServer.Open();
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
                connectionToServer.Close();
            }
        }
        /// <summary>
        /// Function to update the basic pay of the employee
        /// </summary>
        /// <param name="empName"></param>
        /// <returns></returns>
        public bool UpdateDataForEmployee(string empName)
        {
            /// Creates a new connection for every method to avoid "ConnectionString property not initialized" exception
            DBConnection dbc = new DBConnection();
            /// Calling the Get connection method to establish the connection to the Sql Server
            connectionToServer = dbc.GetConnection();
            try
            {
                /// Using the connection established
                using (connectionToServer)
                {
                    /// Opening the connection
                    connectionToServer.Open();
                    /// Update query  for the table and binding with the parameter passed
                    string query = @"update dbo.employee_payroll_services set BasicPay= 30000 where EmployeeName = @parameter";
                    /// Impementing the command on the connection fetched database table
                    SqlCommand command = new SqlCommand(query, connectionToServer);
                    /// Binding the parameter to the formal parameters
                    command.Parameters.AddWithValue("@parameter", empName);
                    /// Storing the result of the executed query
                    var result = command.ExecuteNonQuery();
                    connectionToServer.Close();
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
                connectionToServer.Close();
            }
        }
        /// <summary>
        /// UC4 -- Update the employee payroll data record using a stored procedure
        /// </summary>
        /// <param name="name"></param>
        /// <param name="newBasicPay"></param>
        /// <returns></returns>
        public bool UpdateEmployeeUsingStoredProcedure(string name, int newBasicPay)
        {
            /// Creates a new connection for every method to avoid "ConnectionString property not initialized" exception
            DBConnection dbc = new DBConnection();
            /// Calling the Get connection method to establish the connection to the Sql Server
            connectionToServer = dbc.GetConnection();
            try
            {
                /// Using the connection established
                using (connectionToServer)
                {
                    /// Implementing the stored procedure
                    SqlCommand command = new SqlCommand("spUpdateSalary", connectionToServer);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@salary", newBasicPay);
                    command.Parameters.AddWithValue("@name", name);
                    /// Opening the connection
                    connectionToServer.Open();
                    var result = command.ExecuteNonQuery();
                    connectionToServer.Close();
                    /// Return the result of the transaction i.e. the dml operation to update data
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
                connectionToServer.Close();
            }
        }
        public void GetDetailOfEmployeeStartingBetweenDate(DateTime date)
        {
            /// Creates a new connection for every method to avoid "ConnectionString property not initialized" exception
            DBConnection dbc = new DBConnection();
            /// Calling the Get connection method to establish the connection to the Sql Server
            connectionToServer = dbc.GetConnection();
            try
            {
                /// Using the connection established
                using (connectionToServer)
                {
                    /// Query to get the data from the table
                    string query = @"select * from dbo.employee_payroll_services where StartDate between CAST(@parameter as date) 
                                    and CAST(getdate() as date)";
                    /// Impementing the command on the connection fetched database table
                    SqlCommand command = new SqlCommand(query, connectionToServer);
                    /// Binding the parameter to the formal parameters
                    command.Parameters.AddWithValue("@parameter", date);
                    /// Opening the connection to start mapping
                    connectionToServer.Open();
                    /// executing the sql data reader to fetch the records
                    SqlDataReader reader = command.ExecuteReader();
                    /// executing for not null
                    if (reader.HasRows)
                    {
                        EmployeeModel employeeObject = new EmployeeModel();
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
            /// Catching any type of exception generated during the run time
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connectionToServer.Close();
            }
        }
    }
}
