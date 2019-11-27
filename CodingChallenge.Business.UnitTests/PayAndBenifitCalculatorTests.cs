using System;
using NUnit.Framework;
using CodingChallenge.Business.Helpers;
using CodingChallenge.Business.Models;
using System.Collections.Generic;
using FluentAssertions;
using System.Linq;

namespace CodingChallenge.Business.UnitTests
{
    public class PayAndBenifitCalculatorTests
    {
        [Test]
        public void PopulateEmployeePayShouldSucceed()
        {
            var employee = GetEmployee(1);

            PayAndBenifitCalculator.PopulateEmployeePay(employee);

            employee.PayPerPeriod.Should().Be(AppConstants.DefaultPayPerPeriod);
        }

        [Test]
        public void PopulateBenefitCostShouldSucceed()
        {
            var employee = GetEmployee(1);

            var payPerPeriod = round(AppConstants.DefaultPayPerPeriod);

            var payPerYear = round(payPerPeriod * AppConstants.NumberOfPayPeriods);

            var dependentTotalCostPerYear = round(AppConstants.DependentCost);

            var employeeTotalCostPerYear = round(AppConstants.EmployeeCost);

            var employeeAndDependentsTotalCostPerYear = dependentTotalCostPerYear + employeeTotalCostPerYear;

            var employeeTotalCostPerPayPeriod = round(employeeTotalCostPerYear / AppConstants.NumberOfPayPeriods);

            var dependentTotalCostPerPeriod = round(dependentTotalCostPerYear / AppConstants.NumberOfPayPeriods);

            var employeeAndDependentsTotalCostPerPayPeriod = employeeTotalCostPerPayPeriod + dependentTotalCostPerPeriod;

            var netPayPerYear = payPerYear - employeeAndDependentsTotalCostPerYear;

            var netPayPerPeriod = payPerPeriod - employeeAndDependentsTotalCostPerPayPeriod;

            PayAndBenifitCalculator.PopulateEmployeePay(employee);

            PayAndBenifitCalculator.PopulateBenifitCost(employee);

            employee.PayPerPeriod.Should().Be(payPerPeriod);

            employee.PayPerYear.Should().Be(payPerYear);

            employee.EmployeeAndDependentsTotalCostPerYear.Should().Be(employeeAndDependentsTotalCostPerYear);

            employee.EmployeeAndDependentsTotalCostPerPayPeriod.Should().Be(employeeAndDependentsTotalCostPerPayPeriod);

            employee.EmployeeTotalCostPerPayPeriod.Should().Be(employeeTotalCostPerPayPeriod);

            employee.EmployeeTotalCostPerYear.Should().Be(employeeTotalCostPerYear);

            employee.NetPayPerPeriod.Should().Be(netPayPerPeriod);

            employee.NetPayPerYear.Should().Be(netPayPerYear);

            employee.Dependents.First().DependentTotalCostPerPayPeriod.Should().Be(dependentTotalCostPerPeriod);

            employee.Dependents.First().DependentTotalCostPerYear.Should().Be(dependentTotalCostPerYear);
        }

        [Test]
        public void PopulateBenefitCostWithDiscountShouldSucceed()
        {
            var employee = GetEmployee(1);

            employee.FirstName = AppConstants.FirstLetterDiscountLetter;

            employee.Dependents.First().FirstName = AppConstants.FirstLetterDiscountLetter;

            var payPerPeriod = round(AppConstants.DefaultPayPerPeriod);

            var payPerYear = round(payPerPeriod * AppConstants.NumberOfPayPeriods);

            var dependentTotalCostPerYear = round(AppConstants.DependentCost * AppConstants.FirstLetterDiscountAmount);

            var employeeTotalCostPerYear = round(AppConstants.EmployeeCost * AppConstants.FirstLetterDiscountAmount);

            var employeeAndDependentsTotalCostPerYear = dependentTotalCostPerYear + employeeTotalCostPerYear;

            var employeeTotalCostPerPayPeriod = round(employeeTotalCostPerYear / AppConstants.NumberOfPayPeriods);

            var dependentTotalCostPerPeriod = round(dependentTotalCostPerYear / AppConstants.NumberOfPayPeriods);

            var employeeAndDependentsTotalCostPerPayPeriod = employeeTotalCostPerPayPeriod + dependentTotalCostPerPeriod;

            var netPayPerYear = payPerYear - employeeAndDependentsTotalCostPerYear;

            var netPayPerPeriod = payPerPeriod - employeeAndDependentsTotalCostPerPayPeriod;

            PayAndBenifitCalculator.PopulateEmployeePay(employee);

            PayAndBenifitCalculator.PopulateBenifitCost(employee);

            employee.PayPerPeriod.Should().Be(payPerPeriod);

            employee.PayPerYear.Should().Be(payPerYear);

            employee.EmployeeAndDependentsTotalCostPerYear.Should().Be(employeeAndDependentsTotalCostPerYear);

            employee.EmployeeAndDependentsTotalCostPerPayPeriod.Should().Be(employeeAndDependentsTotalCostPerPayPeriod);

            employee.EmployeeTotalCostPerPayPeriod.Should().Be(employeeTotalCostPerPayPeriod);

            employee.EmployeeTotalCostPerYear.Should().Be(employeeTotalCostPerYear);

            employee.NetPayPerPeriod.Should().Be(netPayPerPeriod);

            employee.NetPayPerYear.Should().Be(netPayPerYear);

            employee.Dependents.First().DependentTotalCostPerPayPeriod.Should().Be(dependentTotalCostPerPeriod);

            employee.Dependents.First().DependentTotalCostPerYear.Should().Be(dependentTotalCostPerYear);
        }

        private Employee GetEmployee(int id)
        {
            return new Employee
            {
                Id = id,
                FirstName = "Test",
                LastName = "User",
                PayPerYear = 0,
                PayPerPeriod = 0,
                NetPayPerYear = 0,
                NetPayPerPeriod = 0,
                EmployeeAndDependentsTotalCostPerPayPeriod = 0,
                EmployeeAndDependentsTotalCostPerYear = 0,
                EmployeeTotalCostPerPayPeriod = 0,
                EmployeeTotalCostPerYear = 0,
                Dependents = new List<Dependent>
                                {
                                    new Dependent
                                    {
                                        Id = id,
                                        FirstName = "Test",
                                        LastName = "User",
                                        DependentTotalCostPerPayPeriod = 0,
                                        DependentTotalCostPerYear = 0,
                                        DependentType = Enums.DependentType.Spouse,
                                        EmployeeId = id
                                    }
                                }
            };
        }

        private decimal round(decimal amount)
        {
            return Math.Round(amount, 2, MidpointRounding.ToEven);
        }
    }
}
