namespace CodingChallenge.Repository.Models
{
    public partial class Dependent
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? EmployeeId { get; set; }

        public decimal DependentTotalCostPerPayPeriod { get; set; }

        public decimal DependentTotalCostPerYear { get; set; }

        public int DependentType { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
