// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ERDiagramRepository.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Praveen Kumar Upadhyay"/>
// --------------------------------------------------------------------------------------------------------------------
namespace EmployeePayrollServices
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;
    /// <summary>
    /// Class in ADO.Net Assignment to implement the Entity relationship based database actions
    /// </summary>
    public class ERDiagramRepository
    {
        /// <summary>
        /// For ensuring the established connection using the Sql Connection specifying the property
        /// </summary>
        public static SqlConnection connectionToServer { get; set; }
        /// <summary>
        /// UC8 -- Adding to the multiple tables at once using the stored procedure
        /// Implementing the schema condition for computation of employee wage breakout
        /// </summary>
        /// <param name="employeeModel"></param>
        public void AddToMultipleTableAndPayrollTableAtOnce(EmployeeModel employeeModel)
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
                    SqlCommand command = new SqlCommand("dbo.AddEmployeeDetailsMultipleTable", connectionToServer);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmpName", employeeModel.EmployeeName);
                    command.Parameters.AddWithValue("@basic_pay", employeeModel.BasicPay);
                    command.Parameters.AddWithValue("@start_date", employeeModel.StartDate);
                    command.Parameters.AddWithValue("@PhoneNumber", employeeModel.PhoneNumber);
                    command.Parameters.AddWithValue("@address", employeeModel.Address);
                    command.Parameters.AddWithValue("@department", employeeModel.Department);
                    command.Parameters.AddWithValue("@gender", employeeModel.Gender);

                    /// Opening the connection
                    connectionToServer.Open();
                    command.ExecuteNonQuery();
                    connectionToServer.Close();
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
        /// UC9 -- Retrieved all the record from the database using the ER-Model database
        /// </summary>
        public void RetrieveAllTheRecordsFromTheDataBase()
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
                    /// Query to get all the data from the table from the multiple table ER-Model
                    string query = @"select emp.employee_id, emp.employee_name, dept.basic_pay, emp.start_date, emp.phoneNumber, emp.address, 
                                    dept.department, emp.gender, pay.deductions, pay.taxable_pay, pay.income_tax, pay.net_pay
                                    from employee_payroll emp, employee_department dept, payroll pay
                                    where emp.employee_id = dept.employee_id and dept.basic_pay = pay.basic_pay;";
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
                            employeeObject.BasicPayAsIntegral = reader.GetInt32(2);
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
                                employeeObject.Address, employeeObject.BasicPay, employeeObject.StartDate.ToString("dd/MM/yyyy"),
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
        /// UC9 -- Ensuring few of the test cases work properly so that we could establish the intergrity of ER Model
        /// </summary>
        public void EnsuringOtherCasesWorkProperly()
        {
            Console.WriteLine("Enter the choice you want to ensure case for ===>");
            Console.WriteLine("1.Get data between specific date.");
            Console.WriteLine("2.Get the average, count and sum of salary for females.");
            int choice = Convert.ToInt32(Console.ReadLine());

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
                    if (choice == 1)
                    {
                        /// Query to get all the data from the table from the multiple table ER-Model
                        string query = @"select emp.employee_id, emp.employee_name, dept.basic_pay, emp.start_date, emp.phoneNumber, emp.address, 
                                    dept.department, emp.gender, pay.deductions, pay.taxable_pay, pay.income_tax, pay.net_pay
                                    from employee_payroll emp, employee_department dept, payroll pay
                                    where emp.employee_id = dept.employee_id and dept.basic_pay = pay.basic_pay and 
                                    emp.start_date between CAST('2018-01-01' as date) and CAST(getdate() as date);";
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
                                employeeObject.BasicPayAsIntegral = reader.GetInt32(2);
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
                                    employeeObject.Address, employeeObject.BasicPay, employeeObject.StartDate.ToString("dd/MM/yyyy"),
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
                    else
                    {
                        /// Query to get all the data from the table from the multiple table ER-Model
                        string query = @"select emp.gender, SUM(dept.basic_pay), MIN(dept.basic_pay), 
                                         MAX(dept.basic_pay), COUNT(distinct(emp.employee_name)),
                                         AVG(dept.basic_pay) from employee_payroll emp, payroll pay, employee_department dept
                                          where emp.employee_id = dept.employee_id and dept.basic_pay = pay.basic_pay group by emp.gender;";
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
                                string gender = reader.GetString(0);
                                var sumOfSalary = reader.GetInt32(1);
                                var minOfSalary = reader.GetInt32(2);
                                var maxOfSalary = reader.GetInt32(3);
                                int count = reader.GetInt32(4);
                                var avgOfSalary = reader.GetInt32(5);
                                Console.WriteLine($"Gender = {gender}\nSum Of Salary = {sumOfSalary}\nMinimum Salary = {minOfSalary}\n" +
                                    $"Maximum Salary = {maxOfSalary}\nNumber Of Employee = {count}\nAverage Salary = {avgOfSalary}");
                                Console.WriteLine("\n\n");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No data found");
                        }
                        reader.Close();
                    }
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
    }
}
