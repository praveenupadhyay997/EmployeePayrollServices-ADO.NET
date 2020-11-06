// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnitTestClass.cs" company="Bridgelabz">
//   Copyright © 2018 Company
// </copyright>
// <creator Name="Praveen Kumar Upadhyay"/>
// --------------------------------------------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeePayrollServices;

namespace UnitTestProjectForEmployeePAyroll
{
    /// <summary>
    /// Class to test the updated salary
    /// </summary>
    [TestClass]
    public class UnitTestClass
    {
        /// <summary>
        /// TC 1 - Read the updated value of the basic pay in the data base
        /// </summary>
        [TestMethod]
        public void GivenEmployeeName_ReturnUpdatedBasicPay()
        {
            //Arrange - Passing the inputs and initialising the instance of the executing class
            string employeeName = "Terissa";
            double basicPay = 30000;
            EmployeeRepository empRepository = new EmployeeRepository();
            //Act - Getting the expected returned value of the passed employee
            double expectedPay = empRepository.ReadUpdatedSalaryFromDatabase(employeeName);
            //Assert
            Assert.AreEqual(basicPay, expectedPay);
        }
    }
}
