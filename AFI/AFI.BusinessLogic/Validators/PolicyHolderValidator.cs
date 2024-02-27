using AFI.BusinessLogic.Entities;
using FluentValidation;

namespace AFI.BusinessLogic.Validators
{
    public class PolicyHolderValidator : AbstractValidator<Person>
    {
        public PolicyHolderValidator()
        {
            RuleFor(person => person.FirstName)
                .NotEmpty()
                .WithMessage("The first name is required")
                .MinimumLength(3)
                .WithMessage("The first name must have at least 3 characters")
                .MaximumLength(50)
                .WithMessage("The first name can have a maximum of 50 characters");

            RuleFor(person => person.LastName).NotEmpty()
                .WithMessage("The last name is required")
                .MinimumLength(3)
                .WithMessage("The last name must have at least 3 characters")
                .MaximumLength(50)
                .WithMessage("The last name can have a maximum of 50 characters");


            RuleFor(person => person.Policies).NotNull().NotEmpty().WithMessage("The policy number is required");
            RuleFor(person => person.Policies.First().PolicyNumber)
                .NotEmpty()
                .WithMessage("The policy number is required")
                .Matches(@"[a-zA-Z]{2}-\d{6}")
                .WithMessage("Policy Reference number is required and should match the following format XX-999999, where XX are any capitalised alpha character followed by a hyphen and 6 numbers.");

            RuleFor(person => person).Must(p =>
                    p.DateOfBirth.HasValue ||
                    (p.Contacts.Count() >= 1 && !string.IsNullOrEmpty(p.Contacts.First(c => c.ContactType == ContactType.Email).Value)))
                .WithMessage("Either the policy holder's DOB OR the policy holder’s email are required");

            RuleFor(person => person.DateOfBirth)
                .Must(dob => !dob.HasValue || DateTime.Now.AddDays(-365 * 18 - 4) >= dob.Value)
                .WithMessage("If supplied the policy holders DOB should mean the customer is at least 18 years old at the point of registering");


            //  RuleFor(person => person.Contacts
        }
    }
}

