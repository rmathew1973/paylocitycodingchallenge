using System;
using CodingChallenge.Business.Models;

namespace CodingChallenge.Business.Helpers
{
    public static class PayAndBenifitCalculator
    {
        public static void PopulateEmployeePay(Employee employee)
        {
            if (employee.PayPerPeriod == 0)
            {
                employee.PayPerPeriod = AppConstants.DefaultPayPerPeriod;
            }

            employee.PayPerYear = employee.PayPerPeriod * AppConstants.NumberOfPayPeriods;

            employee.NetPayPerPeriod = employee.PayPerPeriod - employee.EmployeeAndDependentsTotalCostPerPayPeriod;

            employee.NetPayPerYear = employee.PayPerYear - employee.EmployeeAndDependentsTotalCostPerYear;
        }

        public static void PopulateBenifitCost(Employee employee)
        {
            employee.EmployeeAndDependentsTotalCostPerPayPeriod = 0;

            employee.EmployeeAndDependentsTotalCostPerYear = 0;

            foreach (var dependent in employee.Dependents)
            {
                dependent.DependentTotalCostPerYear = AppConstants.DependentCost;

                if (dependent.FirstName.StartsWith(AppConstants.FirstLetterDiscountLetter, StringComparison.OrdinalIgnoreCase))
                {
                    dependent.DependentTotalCostPerYear = AppConstants.DependentCost * AppConstants.FirstLetterDiscountAmount;
                }

                dependent.DependentTotalCostPerPayPeriod = dependent.DependentTotalCostPerYear / AppConstants.NumberOfPayPeriods;

                employee.EmployeeAndDependentsTotalCostPerPayPeriod += dependent.DependentTotalCostPerPayPeriod;

                employee.EmployeeAndDependentsTotalCostPerYear += dependent.DependentTotalCostPerYear;
            }

            employee.EmployeeTotalCostPerYear = AppConstants.EmployeeCost;

            if (employee.FirstName.StartsWith(AppConstants.FirstLetterDiscountLetter, StringComparison.OrdinalIgnoreCase))
            {
                employee.EmployeeTotalCostPerYear = AppConstants.EmployeeCost * AppConstants.FirstLetterDiscountAmount;
            }

            employee.EmployeeTotalCostPerPayPeriod = employee.EmployeeTotalCostPerYear / AppConstants.NumberOfPayPeriods;

            employee.EmployeeAndDependentsTotalCostPerPayPeriod += employee.EmployeeTotalCostPerPayPeriod;

            employee.EmployeeAndDependentsTotalCostPerYear += employee.EmployeeTotalCostPerYear;
        }
    }
}
