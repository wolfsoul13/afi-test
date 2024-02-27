using AFI.BusinessLogic.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace AFI.DataAccessTests.Repositories
{
    public class RepositoryTests
    {
        private TestImplementationOfRepository repository;
        private Mock<DbContext> mockedDbContext;
        private Mock<DbSet<Person>> mockedDbSet;

        [SetUp]
        public void Setup()
        {
            mockedDbSet = new Mock<DbSet<Person>>();
            mockedDbContext = new Mock<DbContext>();
            mockedDbContext.Setup(x => x.Set<Person>()).Returns(mockedDbSet.Object).Verifiable();
            repository = new TestImplementationOfRepository(mockedDbContext.Object);
        }

        [TearDown]
        public void Clear()
        {
            repository.Dispose();
        }

        public class TheConstructor : RepositoryTests
        {
            [Test]
            public void ExecutesSuccessfully()
            {
                TestImplementationOfRepository repository = null;

                mockedDbContext.Invocations.Clear();

                Assert.DoesNotThrow(() =>
                {
                    repository = new TestImplementationOfRepository(mockedDbContext.Object);
                });

                Assert.That(repository, Is.Not.Null);
                Assert.That(repository.ExposedDbContext, Is.Not.Null);
                Assert.That(repository.ExposedDbContext, Is.EqualTo(mockedDbContext.Object));
                Assert.That(repository.ExposedDbSet, Is.Not.Null);
                Assert.That(repository.ExposedDbSet, Is.EqualTo(mockedDbSet.Object));
                mockedDbContext.Verify(x => x.Set<Person>(), Times.Once);
            }
        }

        public class TheInsertMethod : RepositoryTests
        {
            [Test]
            public void ExecutesSuccessfully()
            {
                mockedDbSet.Invocations.Clear();

                var person = NewPolicyHolder();
                
                mockedDbSet.Setup(x => x.AddAsync(person, It.IsAny<CancellationToken>())).Verifiable();
                Person result = null;

                Assert.DoesNotThrowAsync(async () =>
                {
                    result = await repository.Insert(person);
                });

                Assert.That(result, Is.Not.Null);
                mockedDbSet.Verify(x => x.AddAsync(person, It.IsAny<CancellationToken>()), Times.Once);
            }
        }

        public class TheGetByIdMethod : RepositoryTests
        {
            [Test]
            public void ExecutesSuccessfully()
            {
                mockedDbSet.Invocations.Clear();

                var person = NewPolicyHolder();
                var returnedPerson = NewPolicyHolder();
                returnedPerson.Id = 45;
                ValueTask<Person> returnTaks = new ValueTask<Person>(returnedPerson);
                
                mockedDbSet.Setup(x => x.FindAsync(It.IsAny<int>())).Returns(returnTaks).Verifiable();
                Person result = null;

                Assert.DoesNotThrowAsync(async () =>
                {
                    result = await repository.GetById(45);
                });

                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.EqualTo(returnedPerson));
                mockedDbSet.Verify(x => x.FindAsync(45), Times.Once);
            }
        }

        public class TheSaveMethod : RepositoryTests
        {
            [Test]
            public void ExecutesSuccessfully()
            {
                mockedDbContext.Invocations.Clear();
                
                mockedDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).Verifiable();

                Assert.DoesNotThrowAsync(async () =>
                {
                    await repository.Save();
                });

                mockedDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            }
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