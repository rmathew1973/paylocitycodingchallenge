using System.Collections.Generic;

namespace CodingChallenge.Api.Models
{
    public class EmployeeInputModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal PayPerPeriod { get; set; }

        public virtual ICollection<DependentInputModel> Dependents { get; set; }
    }
}
