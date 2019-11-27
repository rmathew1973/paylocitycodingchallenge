using System;
using CodingChallenge.Business.Models;
using FluentValidation;

namespace CodingChallenge.Business.Validators
{
    public class DependentValidator : AbstractValidator<Dependent>
    {
        const string NameLengthMessage = "Please enter a first name with a length between 1 and 255 characters";
        const string DependentTypeMessage = "Dependent type must be either Spouse or Dependent";

        public DependentValidator()
        {
            RuleFor(x => x.FirstName).NotNull().Length(1, 255)
               .WithMessage(NameLengthMessage);

            RuleFor(x => x.LastName).NotNull().Length(1, 255)
                .WithMessage(NameLengthMessage);

            RuleFor(x => x.DependentType).NotEmpty()
                .WithMessage(DependentTypeMessage);
        }
    }
}
