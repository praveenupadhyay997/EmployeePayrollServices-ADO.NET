// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Praveen Kumar Upadhyay"/>
// --------------------------------------------------------------------------------------------------------------------
using System;

namespace EmployeePayrollServices
{
    class Program
    {
        public static void AddToDatabaseMethod()
        {
            /// Creating the employee repository class's instance
            EmployeeRepository repository = new EmployeeRepository();
            /// Creating the employee model class's instance
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.EmployeeName = "Vishwanathan";
            employeeModel.BasicPay = 50000;
            employeeModel.StartDate = Convert.ToDateTime("2019-02-03");
            employeeModel.PhoneNumber = 78451245;
            employeeModel.Address = "Sec-5";
            employeeModel.Department = "Accounts";
            employeeModel.Gender = "M";
            repository.AddDataToEmployeePayrollDB(employeeModel);
        }
        static void Main(string[] args)
        {
            ///// Creating the employee repository class's instance
            //EmployeeRepository repository = new EmployeeRepository();
            ///// UC1- Ensuring the database connection using the sql connection string
            //repository.EnsureDataBaseConnection();
            //Console.ReadKey();
            ///// UC2 -- Retrieving all the records from the employee payroll services table
            //repository.GetAllEmployeesRecords();
            //Console.ReadKey();
            ///// UC3-- Updating the basic pay for the particular employee in the table records
            //var result = repository.UpdateDataForEmployee("Terissa");
            //Console.WriteLine(result ? "Updated Successfully" : "Update Failed");
            //Console.WriteLine("Data After Updating...");
            //repository.GetAllEmployeesRecords();
            //Console.ReadKey();
            ///// UC4 -- Updating the data record using the stored procedure
            //var reultAfteSP = repository.UpdateEmployeeUsingStoredProcedure("Raj", 30000);
            //Console.WriteLine(reultAfteSP ? "Updated Successfully" : "Update Failed");
            //Console.WriteLine("Data After Updating...");
            //repository.GetAllEmployeesRecords();
            //Console.ReadKey();
            //Console.Clear();
            ///// UC5 -- Getting the detail of employee joining between the passeddate and current date of the system
            //Console.WriteLine("******************Data for the Joining in between date query*****************");
            //repository.GetDetailOfEmployeeStartingBetweenDate(Convert.ToDateTime("01 - 03 - 2019"));
            //Console.ReadKey();
            //Console.Clear();
            ///// UC6 -- Getting the detail of salary ofthe employee joining grouped by gender and searched for a particular gender
            //Console.WriteLine("******************Detail Of the Salary For the records grouped  by Gender*****************");
            //repository.GetTheDetailOfSalaryForPassedGender("F");
            /// UC6 -- Add to the address book payroll servies schema and then
            Console.WriteLine("******************Adding the detail for an employee to the Database*****************");
            AddToDatabaseMethod();
            Console.ReadKey();
        }
    }
}
