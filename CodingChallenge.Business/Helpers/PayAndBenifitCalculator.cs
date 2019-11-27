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
            employee.PayPerPeriod = Math.Round(employee.PayPerPeriod, 2, MidpointRounding.ToEven);

            employee.PayPerYear = Math.Round(employee.PayPerPeriod * AppConstants.NumberOfPayPeriods, 2, MidpointRounding.ToEven);
        }

        public static void PopulateBenifitCost(Employee employee)
        {
            employee.EmployeeAndDependentsTotalCostPerPayPeriod = 0;

            employee.EmployeeAndDependentsTotalCostPerYear = 0;

            foreach (var dependent in employee.Dependents)
            {
                dependent.DependentTotalCostPerYear = Math.Round(AppConstants.DependentCost, 2, MidpointRounding.ToEven);

                if (dependent.FirstName.StartsWith(AppConstants.FirstLetterDiscountLetter, StringComparison.OrdinalIgnoreCase))
                {
                    dependent.DependentTotalCostPerYear = Math.Round(AppConstants.DependentCost * AppConstants.FirstLetterDiscountAmount, 2, MidpointRounding.ToEven);
                }

                dependent.DependentTotalCostPerPayPeriod = Math.Round(dependent.DependentTotalCostPerYear / AppConstants.NumberOfPayPeriods, 2, MidpointRounding.ToEven);

                employee.EmployeeAndDependentsTotalCostPerPayPeriod += Math.Round(dependent.DependentTotalCostPerPayPeriod, 2, MidpointRounding.ToEven);

                employee.EmployeeAndDependentsTotalCostPerYear += Math.Round(dependent.DependentTotalCostPerYear, 2, MidpointRounding.ToEven);
            }

            employee.EmployeeTotalCostPerYear = Math.Round(AppConstants.EmployeeCost, 2, MidpointRounding.ToEven);

            if (employee.FirstName.StartsWith(AppConstants.FirstLetterDiscountLetter, StringComparison.OrdinalIgnoreCase))
            {
                employee.EmployeeTotalCostPerYear = Math.Round(AppConstants.EmployeeCost * AppConstants.FirstLetterDiscountAmount, 2, MidpointRounding.ToEven);
            }

            employee.EmployeeTotalCostPerPayPeriod = Math.Round(employee.EmployeeTotalCostPerYear / AppConstants.NumberOfPayPeriods, 2, MidpointRounding.ToEven);

            employee.EmployeeAndDependentsTotalCostPerPayPeriod = Math.Round(employee.EmployeeTotalCostPerPayPeriod + employee.EmployeeAndDependentsTotalCostPerPayPeriod, 2, MidpointRounding.ToEven);

            employee.EmployeeAndDependentsTotalCostPerYear = Math.Round(employee.EmployeeTotalCostPerYear + employee.EmployeeAndDependentsTotalCostPerYear, 2, MidpointRounding.ToEven);

            employee.NetPayPerPeriod = Math.Round(employee.PayPerPeriod - employee.EmployeeAndDependentsTotalCostPerPayPeriod, 2, MidpointRounding.ToEven);

            employee.NetPayPerYear = Math.Round(employee.PayPerYear - employee.EmployeeAndDependentsTotalCostPerYear, 2, MidpointRounding.ToEven);
        }
    }
}
