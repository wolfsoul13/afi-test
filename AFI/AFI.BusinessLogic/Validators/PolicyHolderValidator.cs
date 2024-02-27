using AFI.BusinessLogic.Entities;
using FluentValidation;
using System.Text.RegularExpressions;

namespace AFI.BusinessLogic.Validators
{
    public class PolicyHolderValidator : AbstractValidator<Person>
    {
        public PolicyHolderValidator()
        {
            RuleFor(person => person.FirstName)
                .NotNull()
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


            RuleFor(person => person.Policies)
                .NotNull().NotEmpty()
                .WithMessage("The policy number is required")
                .DependentRules(() =>
                {
                    RuleFor(person => person.Policies.First().PolicyNumber)
                        .NotNull()
                        .NotEmpty()
                        .WithMessage("The policy number is required")
                        .Matches(@"[a-zA-Z]{2}-\d{6}")
                        .WithMessage("Policy Reference number is required and should match the following format XX-999999, where XX are any capitalised alpha character followed by a hyphen and 6 numbers.");

                });
           
            RuleFor(person => person).Must(p =>
                    p.DateOfBirth.HasValue ||
                    (p.Contacts.Any() && !string.IsNullOrEmpty(p.Contacts.First(c => c.ContactType == ContactType.Email).Value)))
                .WithMessage("zEither the policy holder's DOB OR the policy holder’s email are required")
                .DependentRules(() =>
                {
                    When(person => person.DateOfBirth.HasValue, () =>
                    {
                        RuleFor(person => person.DateOfBirth)
                            .Must(dob => DateTime.Now.AddDays(-365 * 18 - 4) >= dob.Value)
                            .WithMessage(
                                "If supplied the policy holders DOB should mean the customer is at least 18 years old at the point of registering");
                    });

                    When(person => person.Contacts != null && person.Contacts.Any(), () =>
                    {
                        RuleFor(b => b.Contacts)
                            .Must(contacts => HasValidEmailAddress(contacts.First().Value))
                            .WithMessage(
                                "If supplied the policy holders email address should contain a string of at least 4 alpha numeric chars followed by an ‘@’ sign and then another string of at least 2 alpha numeric chars. The email address should end in either ‘.com’ or ‘.co.uk’.");
                    });
                });

        }

        public bool HasValidEmailAddress(string email)
        {
            if (string.IsNullOrEmpty(email)) return false;

            var emailParts = email.Split('@');

            if (emailParts.Length != 2) return false;

            if (CountAlphaNumericCharsInString(emailParts[0]) < 4) return false;

            if (CountAlphaNumericCharsInString(emailParts[1]) < 2) return false;

            if (!emailParts[1].Contains(".com") && !emailParts[1].Contains(".co.uk")) return false;

            var emailSubpart = emailParts[1].Contains(".com")
                ? emailParts[1].Split(".com")
                : emailParts[1].Split(".co.uk");

            return emailSubpart[1] == string.Empty;
        }

        public int CountAlphaNumericCharsInString(string text)
        {
            var alphanumericCharacterCount = 0;

            return string.IsNullOrEmpty(text)
                ? alphanumericCharacterCount
                : text.Count(character => Regex.IsMatch(character.ToString(), "^[a-zA-Z0-9]$"));
        }
    }
}

