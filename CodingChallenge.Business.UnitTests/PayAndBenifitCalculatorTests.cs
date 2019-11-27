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

            var payPerPeriod = Math.Round(AppConstants.DefaultPayPerPeriod, 2, MidpointRounding.ToEven);

            var payPerYear = Math.Round(payPerPeriod * AppConstants.NumberOfPayPeriods, 2, MidpointRounding.ToEven);

            var dependentTotalCostPerYear = Math.Round(AppConstants.DependentCost, 2, MidpointRounding.ToEven);

            var employeeTotalCostPerYear = Math.Round(AppConstants.EmployeeCost, 2, MidpointRounding.ToEven);

            var employeeAndDependentsTotalCostPerYear = dependentTotalCostPerYear + employeeTotalCostPerYear;

            var employeeTotalCostPerPayPeriod = Math.Round(employeeTotalCostPerYear / AppConstants.NumberOfPayPeriods, 2, MidpointRounding.ToEven);

            var dependentTotalCostPerPeriod = Math.Round(dependentTotalCostPerYear / AppConstants.NumberOfPayPeriods, 2, MidpointRounding.ToEven);

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

            var payPerPeriod = Math.Round(AppConstants.DefaultPayPerPeriod, 2, MidpointRounding.ToEven);

            var payPerYear = Math.Round(payPerPeriod * AppConstants.NumberOfPayPeriods, 2, MidpointRounding.ToEven);

            var dependentTotalCostPerYear = Math.Round(AppConstants.DependentCost * AppConstants.FirstLetterDiscountAmount, 2, MidpointRounding.ToEven);

            var employeeTotalCostPerYear = Math.Round(AppConstants.EmployeeCost * AppConstants.FirstLetterDiscountAmount, 2, MidpointRounding.ToEven);

            var employeeAndDependentsTotalCostPerYear = dependentTotalCostPerYear + employeeTotalCostPerYear;

            var employeeTotalCostPerPayPeriod = Math.Round(employeeTotalCostPerYear / AppConstants.NumberOfPayPeriods, 2, MidpointRounding.ToEven);

            var dependentTotalCostPerPeriod = Math.Round(dependentTotalCostPerYear / AppConstants.NumberOfPayPeriods, 2, MidpointRounding.ToEven);

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
    }
}
