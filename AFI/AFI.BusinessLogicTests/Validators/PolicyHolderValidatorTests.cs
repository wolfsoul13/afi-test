using AFI.BusinessLogic.Entities;
using AFI.BusinessLogic.Validators;
using FluentValidation.Results;
using NUnit.Framework;

namespace AFI.BusinessLogicTests.Validators
{
    public class PolicyHolderValidatorTests
    {
        private readonly PolicyHolderValidator validator = new 
            PolicyHolderValidator();

        public class TheFirsNameRules : PolicyHolderValidatorTests
        {
            [Test]
            public void CanSignalNullString()
            {
                var person = new Person
                {
                    FirstName = null
                };
                ValidationResult result = null;

                Assert.DoesNotThrow(() => { result = validator.Validate(person); });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsValid, Is.Not.True);
                Assert.That(result.Errors.Count, Is.GreaterThanOrEqualTo(1));
                Assert.That(result.Errors.FirstOrDefault(x => x.ErrorMessage.Contains("The first name is required")), Is.Not.Null);
            }

            [Test]
            public void CanSignalEmptyString()
            {
                var person = new Person
                {
                    FirstName = string.Empty
                };
                ValidationResult result = null;

                Assert.DoesNotThrow(() => { result = validator.Validate(person); });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsValid, Is.Not.True);
                Assert.That(result.Errors.Count, Is.GreaterThanOrEqualTo(1));
                Assert.That(result.Errors.FirstOrDefault(x => x.ErrorMessage.Contains("The first name is required")), Is.Not.Null);
            }

            [Test]
            public void CanSignalTooShortString()
            {
                var person = new Person
                {
                    FirstName = "xx"
                };
                ValidationResult result = null;

                Assert.DoesNotThrow(() => { result = validator.Validate(person); });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsValid, Is.Not.True);
                Assert.That(result.Errors.Count, Is.GreaterThanOrEqualTo(1));
                Assert.That(result.Errors.FirstOrDefault(x => x.ErrorMessage.Contains("The first name must have at least 3 characters")), Is.Not.Null);
            }

            [Test]
            public void CanSignalTooLongString()
            {
                var person = new Person
                {
                    FirstName = "xxxxx ttttt yyyyy uuuuu iiiii ooooo ppppp ooooo ppppp wwwww"
                };
                ValidationResult result = null;

                Assert.DoesNotThrow(() => { result = validator.Validate(person); });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsValid, Is.Not.True);
                Assert.That(result.Errors.Count, Is.GreaterThanOrEqualTo(1));
                Assert.That(result.Errors.FirstOrDefault(x => x.ErrorMessage.Contains("The first name can have a maximum of 50 characters")), Is.Not.Null);
            }

            [Test]
            public void CanValidateCorrectString()
            {
                var person = new Person
                {
                    FirstName = "some first name"
                };
                ValidationResult result = null;

                Assert.DoesNotThrow(() => { result = validator.Validate(person); });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsValid, Is.Not.True);
                Assert.That(result.Errors.Any(x => x.ErrorMessage.Contains("first name")), Is.False);
            }
        }

        public class TheLastsNameRules : PolicyHolderValidatorTests
        {
            [Test]
            public void CanSignalNullString()
            {
                var person = new Person
                {
                    LastName = null
                };
                ValidationResult result = null;

                Assert.DoesNotThrow(() => { result = validator.Validate(person); });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsValid, Is.Not.True);
                Assert.That(result.Errors.Count, Is.GreaterThanOrEqualTo(1));
                Assert.That(result.Errors.FirstOrDefault(x => x.ErrorMessage.Contains("The last name is required")), Is.Not.Null);
            }

            [Test]
            public void CanSignalEmptyString()
            {
                var person = new Person
                {
                    LastName = string.Empty
                };
                ValidationResult result = null;

                Assert.DoesNotThrow(() => { result = validator.Validate(person); });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsValid, Is.Not.True);
                Assert.That(result.Errors.Count, Is.GreaterThanOrEqualTo(1));
                Assert.That(result.Errors.FirstOrDefault(x => x.ErrorMessage.Contains("The last name is required")), Is.Not.Null);
            }

            [Test]
            public void CanSignalTooShortString()
            {
                var person = new Person
                {
                    LastName = "xx"
                };
                ValidationResult result = null;

                Assert.DoesNotThrow(() => { result = validator.Validate(person); });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsValid, Is.Not.True);
                Assert.That(result.Errors.Count, Is.GreaterThanOrEqualTo(1));
                Assert.That(result.Errors.FirstOrDefault(x => x.ErrorMessage.Contains("The last name must have at least 3 characters")), Is.Not.Null);
            }

            [Test]
            public void CanSignalTooLongString()
            {
                var person = new Person
                {
                    LastName = "xxxxx ttttt yyyyy uuuuu iiiii ooooo ppppp ooooo ppppp wwwww"
                };
                ValidationResult result = null;

                Assert.DoesNotThrow(() => { result = validator.Validate(person); });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsValid, Is.Not.True);
                Assert.That(result.Errors.Count, Is.GreaterThanOrEqualTo(1));
                Assert.That(result.Errors.FirstOrDefault(x => x.ErrorMessage.Contains("The last name can have a maximum of 50 characters")), Is.Not.Null);
            }

            [Test]
            public void CanValidateCorrectString()
            {
                var person = new Person
                {
                    LastName = "some first name"
                };
                ValidationResult result = null;

                Assert.DoesNotThrow(() => { result = validator.Validate(person); });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsValid, Is.Not.True);
                Assert.That(result.Errors.Any(x => x.ErrorMessage.Contains("last name")), Is.False);
            }
        }

        public class ThePolicyNumberRules : PolicyHolderValidatorTests
        {
            [Test]
            public void CanSignalNoPolicy()
            {
                var person = new Person();
                
                ValidationResult result = null;

                Assert.DoesNotThrow(() => { result = validator.Validate(person); });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsValid, Is.Not.True);
                Assert.That(result.Errors.Count, Is.GreaterThanOrEqualTo(1));
                Assert.That(result.Errors.FirstOrDefault(x => x.ErrorMessage.Contains("The policy number is required")), Is.Not.Null);
            }

            [Test]
            public void CanSignalNullPolicyNumber()
            {
                var person = new Person();
                person.Policies.Add(new Policy
                {
                    PolicyNumber = null
                });

                ValidationResult result = null;

                Assert.DoesNotThrow(() => { result = validator.Validate(person); });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsValid, Is.Not.True);
                Assert.That(result.Errors.Count, Is.GreaterThanOrEqualTo(1));
                Assert.That(result.Errors.FirstOrDefault(x => x.ErrorMessage.Contains("The policy number is required")), Is.Not.Null);
            }

            [Test]
            public void CanSignalEmptyPolicyNumber()
            {
                var person = new Person();
                person.Policies.Add(new Policy
                {
                    PolicyNumber = string.Empty
                });

                ValidationResult result = null;

                Assert.DoesNotThrow(() => { result = validator.Validate(person); });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsValid, Is.Not.True);
                Assert.That(result.Errors.Count, Is.GreaterThanOrEqualTo(1));
                Assert.That(result.Errors.FirstOrDefault(x => x.ErrorMessage.Contains("The policy number is required")), Is.Not.Null);
            }

            [Test]
            public void CanSignalIncorrectFormatForPolicyNumber()
            {
                var person = new Person();
                person.Policies.Add(new Policy
                {
                    PolicyNumber = "some policy no"
                });

                ValidationResult result = null;

                Assert.DoesNotThrow(() => { result = validator.Validate(person); });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsValid, Is.Not.True);
                Assert.That(result.Errors.Count, Is.GreaterThanOrEqualTo(1));
                var expectedMessage =
                    "Policy Reference number is required and should match the following format XX-999999, where XX are any capitalised alpha character followed by a hyphen and 6 numbers.";
                Assert.That(result.Errors.FirstOrDefault(x => x.ErrorMessage.Contains(expectedMessage)), Is.Not.Null);
            }

            [Test]
            public void CanValidateCorrectFormatForPolicyNumber()
            {
                var person = new Person();
                person.Policies.Add(new Policy
                {
                    PolicyNumber = "XX-123456"
                });

                ValidationResult result = null;

                Assert.DoesNotThrow(() => { result = validator.Validate(person); });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsValid, Is.Not.True);
                Assert.That(result.Errors.Count, Is.GreaterThanOrEqualTo(1));
                Assert.That(result.Errors.Any(x => x.ErrorMessage.Contains("policy number")), Is.False);
            }
        }

        public class TheCombinedFieldsRule : PolicyHolderValidatorTests
        {
            [Test]
            public void CanSignalBothDobAndEmailMissing()
            {
                var person = new Person();

                ValidationResult result = null;

                Assert.DoesNotThrow(() => { result = validator.Validate(person); });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsValid, Is.Not.True);
                Assert.That(result.Errors.Count, Is.GreaterThanOrEqualTo(1));
                Assert.That(result.Errors.FirstOrDefault(x => x.ErrorMessage.Contains("Either the policy holder's DOB OR the policy holder’s email are required")), Is.Not.Null);
            }
        }

        public class TheDateOfBirthRules : PolicyHolderValidatorTests
        {
            [Test]
            public void CanSignalPersonYoungerThan18()
            {
                var person = new Person();

                person.DateOfBirth = new DateTime(2006, 5, 28);

                ValidationResult result = null;

                Assert.DoesNotThrow(() => { result = validator.Validate(person); });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsValid, Is.Not.True);
                Assert.That(result.Errors.Count, Is.GreaterThanOrEqualTo(1));
                var expectedMessage =
                    "If supplied the policy holders DOB should mean the customer is at least 18 years old at the point of registering";
                Assert.That(result.Errors.FirstOrDefault(x => x.ErrorMessage.Contains(expectedMessage)), Is.Not.Null);
            }

            [Test]
            public void CanCanValidatePersonOlder18()
            {
                var person = new Person();

                person.DateOfBirth = new DateTime(2006, 1, 28);

                ValidationResult result = null;

                Assert.DoesNotThrow(() => { result = validator.Validate(person); });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsValid, Is.Not.True);
                Assert.That(result.Errors.Count, Is.GreaterThanOrEqualTo(1));
                var expectedMessage =
                    "If supplied the policy holders DOB should mean the customer is at least 18 years old at the point of registering";
                Assert.That(result.Errors.Any(x => x.ErrorMessage.Contains(expectedMessage)), Is.False);
            }
        }

        public class TheEmailRules : PolicyHolderValidatorTests
        {
            [Test]
            public void CanSignalLackOfAtChar()
            {
                var person = new Person();
                person.Contacts.Add(new Contact
                {
                    ContactType = ContactType.Email,
                    Value = "some non email"
                });

                ValidationResult result = null;

                Assert.DoesNotThrow(() => { result = validator.Validate(person); });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsValid, Is.Not.True);
                Assert.That(result.Errors.Count, Is.GreaterThanOrEqualTo(1));
                var expectedMessage =
                    "If supplied the policy holders email address should contain a string of at least 4 alpha numeric chars followed by an ‘@’ sign and then another string of at least 2 alpha numeric chars. The email address should end in either ‘.com’ or ‘.co.uk’.";
                Assert.That(result.Errors.FirstOrDefault(x => x.ErrorMessage.Contains(expectedMessage)), Is.Not.Null);
            }

            [Test]
            public void CanSignalLessThan4AlphaNumericCharsPriorAtChar()
            {
                var person = new Person();
                person.Contacts.Add(new Contact
                {
                    ContactType = ContactType.Email,
                    Value = "so--m@ss.om"
                });

                ValidationResult result = null;

                Assert.DoesNotThrow(() => { result = validator.Validate(person); });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsValid, Is.Not.True);
                Assert.That(result.Errors.Count, Is.GreaterThanOrEqualTo(1));
                var expectedMessage =
                    "If supplied the policy holders email address should contain a string of at least 4 alpha numeric chars followed by an ‘@’ sign and then another string of at least 2 alpha numeric chars. The email address should end in either ‘.com’ or ‘.co.uk’.";
                Assert.That(result.Errors.FirstOrDefault(x => x.ErrorMessage.Contains(expectedMessage)), Is.Not.Null);
            }

            [Test]
            public void CanSignalLessThan2AlphaNumericCharsPriorAtChar()
            {
                var person = new Person();
                person.Contacts.Add(new Contact
                {
                    ContactType = ContactType.Email,
                    Value = "some@s."
                });

                ValidationResult result = null;

                Assert.DoesNotThrow(() => { result = validator.Validate(person); });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsValid, Is.Not.True);
                Assert.That(result.Errors.Count, Is.GreaterThanOrEqualTo(1));
                var expectedMessage =
                    "If supplied the policy holders email address should contain a string of at least 4 alpha numeric chars followed by an ‘@’ sign and then another string of at least 2 alpha numeric chars. The email address should end in either ‘.com’ or ‘.co.uk’.";
                Assert.That(result.Errors.FirstOrDefault(x => x.ErrorMessage.Contains(expectedMessage)), Is.Not.Null);
            }

            [Test]
            public void CanSignalNotEndingInDesiredFormat()
            {
                var person = new Person();
                person.Contacts.Add(new Contact
                {
                    ContactType = ContactType.Email,
                    Value = "some@gmail.ro"
                });

                ValidationResult result = null;

                Assert.DoesNotThrow(() => { result = validator.Validate(person); });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsValid, Is.Not.True);
                Assert.That(result.Errors.Count, Is.GreaterThanOrEqualTo(1));
                var expectedMessage =
                    "If supplied the policy holders email address should contain a string of at least 4 alpha numeric chars followed by an ‘@’ sign and then another string of at least 2 alpha numeric chars. The email address should end in either ‘.com’ or ‘.co.uk’.";
                Assert.That(result.Errors.FirstOrDefault(x => x.ErrorMessage.Contains(expectedMessage)), Is.Not.Null);
            }

            [Test]
            public void CanValidateCorrectEmail()
            {
                var person = new Person();
                person.Contacts.Add(new Contact
                {
                    ContactType = ContactType.Email,
                    Value = "some@gmail.com"
                });

                ValidationResult result = null;

                Assert.DoesNotThrow(() => { result = validator.Validate(person); });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsValid, Is.Not.True);
                Assert.That(result.Errors.Count, Is.GreaterThanOrEqualTo(1));
                var expectedMessage =
                    "If supplied the policy holders email address should contain a string of at least 4 alpha numeric chars followed by an ‘@’ sign and then another string of at least 2 alpha numeric chars. The email address should end in either ‘.com’ or ‘.co.uk’.";
                Assert.That(result.Errors.Any(x => x.ErrorMessage.Contains(expectedMessage)), Is.False);
            }

            [Test]
            public void CanValidateCorrectEmail2()
            {
                var person = new Person();
                person.Contacts.Add(new Contact
                {
                    ContactType = ContactType.Email,
                    Value = "some@gmail.co.uk"
                });

                ValidationResult result = null;

                Assert.DoesNotThrow(() => { result = validator.Validate(person); });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsValid, Is.Not.True);
                Assert.That(result.Errors.Count, Is.GreaterThanOrEqualTo(1));
                var expectedMessage =
                    "If supplied the policy holders email address should contain a string of at least 4 alpha numeric chars followed by an ‘@’ sign and then another string of at least 2 alpha numeric chars. The email address should end in either ‘.com’ or ‘.co.uk’.";
                Assert.That(result.Errors.Any(x => x.ErrorMessage.Contains(expectedMessage)), Is.False);
            }

        }
    }
}