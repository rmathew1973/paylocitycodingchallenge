using CodingChallenge.Business.Enums;

namespace CodingChallenge.Api.Models
{
    public class DependentInputModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? EmployeeId { get; set; }

        public DependentType Dependenttype { get; set; }
    }
}
