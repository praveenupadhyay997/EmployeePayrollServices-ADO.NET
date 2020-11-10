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
        /// <summary>
        /// Method to define the properties to add the data to the database
        /// </summary>
        public static void AddToDatabaseMethod()
        {
            /// Creating the employee repository class's instance
            EmployeeRepository repository = new EmployeeRepository();
            /// Creating the employee model class's instance
            EmployeeModel employeeModel = new EmployeeModel();
            /// Creating the ER Diagram repository class's instance
            ERDiagramRepository diagramRepository = new ERDiagramRepository();
            employeeModel.EmployeeName = "Rajesh";
            employeeModel.BasicPay = 40000;
            employeeModel.StartDate = Convert.ToDateTime("2020-02-03");
            employeeModel.PhoneNumber = 72061245;
            employeeModel.Address = "Sec-8";
            employeeModel.Department = "IT";
            employeeModel.Gender = "M";
            /// Adding to the unified database
            repository.AddDataToEmployeePayrollDB(employeeModel);
            /// Adding to the ER- Diagram implementing Database Schema
            diagramRepository.AddToMultipleTableAndPayrollTableAtOnce(employeeModel);
        }
        /// <summary>
        /// Method to retrieve the entire data from the database in ER Model
        /// </summary>
        public static void RetrieveAllDataFromDatabase()
        {
            /// Creating the ER Diagram repository class's instance
            ERDiagramRepository diagramRepository = new ERDiagramRepository();
            diagramRepository.RetrieveAllTheRecordsFromTheDataBase();
            /// UC10 -- Ensuring the other Test Case Working Properly
            diagramRepository.EnsuringOtherCasesWorkProperly();
        }
        static void Main(string[] args)
        {
            /// Creating the employee repository class's instance
            EmployeeRepository repository = new EmployeeRepository();
            /// UC1- Ensuring the database connection using the sql connection string
            repository.EnsureDataBaseConnection();
            Console.ReadKey();
            /// UC2 -- Retrieving all the records from the employee payroll services table
            repository.GetAllEmployeesRecords();
            Console.ReadKey();
            /// UC3-- Updating the basic pay for the particular employee in the table records
            var result = repository.UpdateDataForEmployee("Terissa");
            Console.WriteLine(result ? "Updated Successfully" : "Update Failed");
            Console.WriteLine("Data After Updating...");
            repository.GetAllEmployeesRecords();
            Console.ReadKey();
            /// UC4 -- Updating the data record using the stored procedure
            var reultAfteSP = repository.UpdateEmployeeUsingStoredProcedure("Raj", 30000);
            Console.WriteLine(reultAfteSP ? "Updated Successfully" : "Update Failed");
            Console.WriteLine("Data After Updating...");
            repository.GetAllEmployeesRecords();
            Console.ReadKey();
            Console.Clear();
            /// UC5 -- Getting the detail of employee joining between the passeddate and current date of the system
            Console.WriteLine("******************Data for the Joining in between date query*****************");
            repository.GetDetailOfEmployeeStartingBetweenDate(Convert.ToDateTime("01 - 03 - 2019"));
            Console.ReadKey();
            Console.Clear();
            /// UC6 -- Getting the detail of salary ofthe employee joining grouped by gender and searched for a particular gender
            Console.WriteLine("******************Detail Of the Salary For the records grouped  by Gender*****************");
            repository.GetTheDetailOfSalaryForPassedGender("F");
            /// UC6 -- Add to the address book payroll servies schema and then
            Console.WriteLine("******************Adding the detail for an employee to the Database*****************");
            AddToDatabaseMethod();
            /// UC9 -- Retrieving all the records from the employee payroll services table using the ER Model
            RetrieveAllDataFromDatabase();
            Console.ReadKey();
        }
    }
}
