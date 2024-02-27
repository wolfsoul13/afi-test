using System.Security.Cryptography.X509Certificates;
using AFI.BusinessLogic.Entities;
using AFI.DataAccess.Repositories;
using AFI.Handlers.Services;
using AFI.Models;
using AFI.Models.Adapters;
using AFI.Models.Client;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;

namespace AFI.HandlerTests.Services
{
    public class PolicyHolderHandlerTests
    {
        private Mock<IPersonRepository> personRepository;
        private Mock<IPolicyHolderAdapter> policyHolderAdapter;
        private Mock<IValidator<Person>> policyHolderValidator;
        private PolicyHolderHandler handler;

        [SetUp]
        public void Setup()
        {
            personRepository = new Mock<IPersonRepository>();
            policyHolderAdapter = new Mock<IPolicyHolderAdapter>();
            policyHolderValidator = new Mock<IValidator<Person>>();

            handler = new PolicyHolderHandler(personRepository.Object, policyHolderAdapter.Object,
                policyHolderValidator.Object);
        }

        public Person NewPerson()
        {
            var person = new Person
            {
                FirstName = "Margareta",
                LastName = "Beze",
                DateOfBirth = new
                    DateTime(1926, 2, 23)
            };

            var contact = new Contact
            {
                ContactType = ContactType.Email,
                Value = "some@email.com"
            };

            person.Contacts.Add(contact);

            var policy = new Policy
            {
                PolicyNumber = "XX-666666"
            };

            person.Policies.Add(policy);

            return person;
        }

        public PolicyHolder NewPolicyHolder()
        {
            return new PolicyHolder
            {
                PolicyNumber = "some no",
                LastName = "last name",
                DateOfBirth = DateTime.Now,
                Email = "some@email.com",
                FirstName = "first name"
            };
        }
        //going to skip constructor tests for lack of time

        public class TheNewPolicyHolderMethod : PolicyHolderHandlerTests
        {
            [Test]
            public void ThowsForNullPolicyHolder()
            {
                PolicyHolder policyHolder = null;
                HandlerResult<int> result = null;

                Exception exception = null;

                exception = Assert.ThrowsAsync<ArgumentNullException>(async () =>
                {
                    result = await handler.NewPolicyHolder(policyHolder);
                });

                Assert.That(exception, Is.Not.Null);
                Assert.That(exception.Message.Contains(nameof(policyHolder)), Is.True);
            }

            [Test]
            public void CallsAdapterMethodToEntity()
            {
                PolicyHolder policyHolder = NewPolicyHolder();
                HandlerResult<int> result = null;
                var adapterResult = NewPerson();
                var validatorResult = new ValidationResult(new List<ValidationFailure>
                    { new ValidationFailure("FirstName", "First name is required") });

                policyHolderAdapter.Setup(x => x.ToEntity(policyHolder)).Returns(adapterResult).Verifiable();
                policyHolderValidator.Setup(x => x.Validate(adapterResult)).Returns(validatorResult).Verifiable();

                Assert.DoesNotThrowAsync(async () =>
                {
                    result = await handler.NewPolicyHolder(policyHolder);
                });

                policyHolderAdapter.Verify(x => x.ToEntity(policyHolder), Times.Once);
            }

            [Test]
            public void CallsValidatorMethodValidate()
            {
                PolicyHolder policyHolder = NewPolicyHolder();
                HandlerResult<int> result = null;
                var adapterResult = NewPerson();
                var validatorResult = new ValidationResult(new List<ValidationFailure>
                    { new ValidationFailure("FirstName", "First name is required") });

                policyHolderAdapter.Setup(x => x.ToEntity(policyHolder)).Returns(adapterResult).Verifiable();
                policyHolderValidator.Setup(x => x.Validate(adapterResult)).Returns(validatorResult).Verifiable();

                Assert.DoesNotThrowAsync(async () =>
                {
                    result = await handler.NewPolicyHolder(policyHolder);
                });

                policyHolderValidator.Verify(x => x.Validate(adapterResult), Times.Once);
            }

            [Test]
            public void ReturnsWithNoCallsToRepositoryForInvalidPolicyHolder()
            {
                PolicyHolder policyHolder = NewPolicyHolder();
                HandlerResult<int> result = null;
                var adapterResult = NewPerson();
                var insertResult = NewPerson();
                insertResult.Id = 456788;

                var validatorResult = new ValidationResult(new List<ValidationFailure>
                    { new ValidationFailure("FirstName", "First name is required") });

                policyHolderAdapter.Setup(x => x.ToEntity(policyHolder)).Returns(adapterResult).Verifiable();
                policyHolderValidator.Setup(x => x.Validate(adapterResult)).Returns(validatorResult).Verifiable();
                

                personRepository.Setup(x => x.Insert(adapterResult)).ReturnsAsync(() => insertResult).Verifiable();
                personRepository.Setup(x => x.Save()).Verifiable();

                Assert.DoesNotThrowAsync(async () =>
                {
                    result = await handler.NewPolicyHolder(policyHolder);
                });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.Result, Is.EqualTo(0));
                Assert.That(result.ValidationResult, Is.EqualTo(validatorResult));

                personRepository.Verify(x => x.Insert(adapterResult), Times.Never);
                personRepository.Verify(x => x.Save(), Times.Never);
            }

            [Test]
            public void CallsRepositoryForValidPolicyHolder()
            {
                PolicyHolder policyHolder = NewPolicyHolder();
                HandlerResult<int> result = null;
                var adapterResult = NewPerson();
                var insertResult = NewPerson();
                insertResult.Id = 456788;

                var validatorResult = new ValidationResult(new List<ValidationFailure> { });

                policyHolderAdapter.Setup(x => x.ToEntity(policyHolder)).Returns(adapterResult).Verifiable();
                policyHolderValidator.Setup(x => x.Validate(adapterResult)).Returns(validatorResult).Verifiable();


                personRepository.Setup(x => x.Insert(adapterResult)).ReturnsAsync(() => insertResult).Verifiable();
                personRepository.Setup(x => x.Save()).Verifiable();

                Assert.DoesNotThrowAsync(async () =>
                {
                    result = await handler.NewPolicyHolder(policyHolder);
                });

                Assert.That(result, Is.Not.Null);
                Assert.That(result.Result, Is.EqualTo(456788));
                Assert.That(result.ValidationResult, Is.EqualTo(validatorResult));

                personRepository.Verify(x => x.Insert(adapterResult), Times.Once);
                personRepository.Verify(x => x.Save(), Times.Once);
            }
        }

    }
}
