using CodingChallenge.Business.Enums;

namespace CodingChallenge.Api.Models
{
    public class DependentViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? EmployeeId { get; set; }

        public decimal DependentTotalCostPerPayPeriod { get; set; }

        public decimal DependentTotalCostPerYear { get; set; }

        public DependentType Dependenttype { get; set; }
    }
}
