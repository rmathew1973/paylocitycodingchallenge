using CodingChallenge.Business.Models;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace CodingChallenge.Business.Validators
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        const string NameLengthMessage = "Please enter a first name with a length between 1 and 255 characters";
        const string PayPerPeriodMessage = "Please enter a valid value for pay per period.";
        const string MaxSpousesExceededMessage = "Please include only one spouse.";

        public EmployeeValidator()
        {
            RuleFor(x => x.FirstName)
                .NotNull()
                .Length(1,255)
                .WithMessage(NameLengthMessage);

            RuleFor(x => x.LastName)
                .NotNull()
                .Length(1,255)
                .WithMessage(NameLengthMessage);

            RuleFor(x => x.PayPerPeriod)
                .NotEmpty()
                .WithMessage(PayPerPeriodMessage);

            RuleForEach(x => x.Dependents)
                .SetValidator(new DependentValidator());

            RuleFor(x => x.Dependents)
                .Must(CheckSpouse)
                .WithMessage(MaxSpousesExceededMessage);
        }

        private bool CheckSpouse(ICollection<Dependent> arg)
        {
            if(arg.Where(x => x.DependentType == Enums.DependentType.Spouse).Count() > 1)
            {
                return false;
            }
            return true;
        }
    }
}
