using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using AFI.BusinessLogic.Entities;
using AFI.Models.Adapters;
using AFI.Models.Client;

namespace AFI.ModelsTests.Adapters
{
    public class PolicyHolderAdapterTest
    {
        public class TheToEntityMethod : PolicyHolderAdapterTest
        {
            private readonly IPolicyHolderAdapter adapter = new PolicyHolderAdapter();

            public PolicyHolder GetModel()
            {
                return new PolicyHolder
                {
                    FirstName = "George",
                    LastName = "Costanza",
                    Email = "email@email.com",
                    PolicyNumber = "some-policy-no",
                    DateOfBirth = DateTime.Now
                };
            }

            [Test]
            public void ThrowsForNullModel()
            {
                PolicyHolder model = null;
                Person entity = null;
                Exception exception = null;

                exception = Assert.Throws<ArgumentNullException>(() =>
                {
                    entity = adapter.ToEntity(model, entity);
                });

                Assert.That(entity, Is.Null);
                Assert.That(exception, Is.Not.Null);
                Assert.That(exception.Message.Contains(nameof(model)), Is.True);
            }

            [Test]
            public void ThrowsForNullEntity()
            {
                PolicyHolder model = GetModel();
                Person entity = null;
                Exception exception = null;

                exception = Assert.Throws<ArgumentNullException>(() =>
                {
                    entity = adapter.ToEntity(model, entity);
                });

                Assert.That(entity, Is.Null);
                Assert.That(exception, Is.Not.Null);
                Assert.That(exception.Message.Contains(nameof(entity)), Is.True);
            }

            [Test]
            public void CanPopulateFirstName()
            {
                PolicyHolder model = GetModel();
                Person entity = new Person();

                Assert.DoesNotThrow(() =>
                {
                    entity = adapter.ToEntity(model, entity);
                });

                Assert.That(entity, Is.Not.Null);
                Assert.That(entity.FirstName, Is.EqualTo(model.FirstName));
            }

            [Test]
            public void CanPopulateLastName()
            {
                PolicyHolder model = GetModel();
                Person entity = new Person();

                Assert.DoesNotThrow(() =>
                {
                    entity = adapter.ToEntity(model, entity);
                });

                Assert.That(entity, Is.Not.Null);
                Assert.That(entity.LastName, Is.EqualTo(model.LastName));
            }

            [Test]
            public void CanPopulateDateOfBirth()
            {
                PolicyHolder model = GetModel();
                Person entity = new Person();

                Assert.DoesNotThrow(() =>
                {
                    entity = adapter.ToEntity(model, entity);
                });

                Assert.That(entity, Is.Not.Null);
                Assert.That(entity.DateOfBirth, Is.EqualTo(model.DateOfBirth));
            }

            [Test]
            public void CanHandleExistingPolicy()
            {
                PolicyHolder model = GetModel();
                Person entity = new Person();
                entity.Policies.Add(new Policy{ PolicyNumber = model.PolicyNumber});

                Assert.DoesNotThrow(() =>
                {
                    entity = adapter.ToEntity(model, entity);
                });

                Assert.That(entity, Is.Not.Null);
                Assert.That(entity.Policies.Count(), Is.EqualTo(1));
            }

            [Test]
            public void CanAddPolicy()
            {
                PolicyHolder model = GetModel();
                Person entity = new Person();

                Assert.DoesNotThrow(() =>
                {
                    entity = adapter.ToEntity(model, entity);
                });

                Assert.That(entity, Is.Not.Null);
                Assert.That(entity.Policies.Count(), Is.EqualTo(1));
                Assert.That(entity.Policies.First().PolicyNumber, Is.EqualTo(model.PolicyNumber));
            }

            public void CanHandleExistingContact()
            {
                PolicyHolder model = GetModel();
                Person entity = new Person();
                entity.Contacts.Add(new Contact { ContactType = ContactType.Email, Value = model.Email });

                Assert.DoesNotThrow(() =>
                {
                    entity = adapter.ToEntity(model, entity);
                });

                Assert.That(entity, Is.Not.Null);
                Assert.That(entity.Contacts.Count(), Is.EqualTo(1));
            }

            public void CanAddContact()
            {
                PolicyHolder model = GetModel();
                Person entity = new Person();

                Assert.DoesNotThrow(() =>
                {
                    entity = adapter.ToEntity(model, entity);
                });

                Assert.That(entity, Is.Not.Null);
                Assert.That(entity.Contacts.Count(), Is.EqualTo(1));
                Assert.That(entity.Contacts.First().ContactType, Is.EqualTo(ContactType.Email));
                Assert.That(entity.Contacts.First().Value, Is.EqualTo(model.Email));
            }
        }

    }
}
