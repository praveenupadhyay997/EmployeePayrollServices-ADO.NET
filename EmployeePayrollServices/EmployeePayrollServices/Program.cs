﻿// --------------------------------------------------------------------------------------------------------------------
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
        static void Main(string[] args)
        {
            /// Creating the employee repository class's instance
            EmployeeRepository repository = new EmployeeRepository();
            /// UC1- Ensuring the database connection using the sql connection string
            repository.EnsureDataBaseConnection();
            /// UC2 -- Retrieving all the records from the employee payroll services table
            repository.GetAllEmployeesRecords();
            /// UC3-- Updating the basic pay for the particular employee in the table records
            var result = repository.UpdateDataForEmployee("Terissa");
            Console.WriteLine(result ? "Updated Successfully": "Update Failed");
            Console.WriteLine("Data After Updating...");
            repository.GetAllEmployeesRecords();
        }
    }
}