using AFI.BusinessLogic.Entities;
using AFI.DataAccess.Repositories;
using NUnit.Framework;

namespace AFI.DataAccessTests.Integration
{
    
    public class PersonRepositoryIntegrationTest : TestDatabaseFixture
    {
        private IPersonRepository personRepository;
       
        [SetUp]
        public void Setup()
        {
            var dbContext = CreateContext();
            personRepository = new PersonRepository(dbContext);
        }

        [TearDown]
        public void Clear()
        {
            personRepository.Dispose();
        }

        [Test]
        public void CanCreatePolicyHolder()
        {
            var person = NewPolicyHolder();
            Person result = null;

            Assert.DoesNotThrowAsync(() =>
            {
                result = personRepository.Insert(person).Result;
                return personRepository.Save();
            });

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.Not.EqualTo(0));

            var savedPerson = personRepository.GetById(result.Id);
            Assert.That(savedPerson, Is.Not.Null);
        }


        public Person NewPolicyHolder()
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
    }
}
