using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part2
{


    public class EmployeeRecord
    {
        //private fields
        private int ID;
        private string name;
        private string stateCode;
        private decimal hoursWorked;
        private decimal hourlyRate;

        //public properties
        public string Name
        {
            get => name;
            set => name = value;
        }
        public decimal HourWorked
        {
            get => hoursWorked;
            set => hoursWorked = value;
        }
        public string StateCode
        {
            get => stateCode;
            set => stateCode = value;
        }
        public decimal HourlyRate
        {
            get => hourlyRate;
            set => hourlyRate = value;
        }
        public int id
        {
            get => ID;
            set => ID = value;
        }
        // create an employee Record with public properties
        //    ID                        a number to identify an employee
        //    Name                      the employee name
        //    StateCode                 the state collecting taxes for this employee
        //    HoursWorkedInTheYear      the total number of hours worked in the entire year (including fractions of an hour)
        //    HourlyRate                the rate the employee is paid for each hour worked
        //                                  assume no changes to the rate throughout the year.
        public EmployeeRecord(string csv)
        {

        }
        //    provide a constructor that takes a csv and initializes the employeerecord
        //       do all error checking and throw appropriate exceptions

        //    provide an additional READ ONLY property called  YearlyPay that will compute the total income for the employee
        //        by multiplying their hours worked by their hourly rate

        //    provide an additional READONLY property that will compute the total tax due by:
        //        calling into the taxcalculator providing the statecode and the yearly income computed in the YearlyPay property

        //    provide an override of toString to output the record : including the YearlyPay and the TaxDue
        public override string ToString()
        {
            return $"";
        }
    }

    static class EmployeesList
    {

        // create an EmployeeList class that will read all the employees form the Employees.csv file
        // the logic is similar to the way the taxcalculator read its taxrecords

        // Create a List of employee records.  The employees are arranged into a LIST not a DICTIONARY
        //   because we are not accessing the employees by state,  we are accessing the employees sequentially as a list

        // create a static constructor to load the list from the file
        //   be sure to include try/catch to display messages


    }




    class Program
    {

        // loop over all the employees in the EmployeeList and print them
        // If the ToString() in the employee record is correct, all the data will print out.

        public static void Main()
        {

            try
            {

                // write your logic here
            }



            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}