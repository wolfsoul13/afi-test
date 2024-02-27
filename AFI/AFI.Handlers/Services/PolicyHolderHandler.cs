
using AFI.BusinessLogic.Entities;
using AFI.DataAccess.Repositories;

namespace AFI.Handlers.Services
{
    public class PolicyHolderHandler : IPolicyHolderHandler
    {
        private readonly IPersonRepository personRepository;

        public PolicyHolderHandler(IPersonRepository personRepository)
        {
            this.personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }

        public async Task<int> NewPolicyHolder()
        {
            var person = new Person
            {
                FirstName = "Ikkuma",
                LastName = "Uqiok",
                DateOfBirth = new
                    DateTime(2006, 2, 26)
            };

            var contact = new Contact
            {
                ContactType = ContactType.Email,
                Value = "soe@e."
            };

            person.Contacts.Add(contact);

            var policy = new Policy
            {
                PolicyNumber = "XC-364363"
            };

            person.Policies.Add(policy);

            var result = await personRepository.Insert(person);
            await personRepository.Save();

            return result.Id;
        }
    }
}
