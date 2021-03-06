﻿using System.Collections.Generic;

namespace CodingChallenge.Business.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal EmployeeTotalCostPerPayPeriod { get; set; }

        public decimal EmployeeTotalCostPerYear { get; set; }

        public decimal EmployeeAndDependentsTotalCostPerPayPeriod { get; set; }

        public decimal EmployeeAndDependentsTotalCostPerYear { get; set; }

        public decimal PayPerPeriod { get; set; }

        public decimal PayPerYear { get; set; }

        public decimal NetPayPerPeriod { get; set; }

        public decimal NetPayPerYear { get; set; }

        public decimal LessCostForLastPayPeriod { get; set; }

        public virtual ICollection<Dependent> Dependents { get; set; }
    }
}
