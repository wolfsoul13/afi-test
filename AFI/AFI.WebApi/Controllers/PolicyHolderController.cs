using AFI.BusinessLogic.Entities;
using AFI.DataAccess.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AFI.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PolicyHolderController : ControllerBase
    {
        private readonly ILogger<PolicyHolderController> _logger;
        private readonly IPersonRepository _personRepository;

        public PolicyHolderController(ILogger<PolicyHolderController> logger, IPersonRepository personRepository)
        {
            _logger = logger;
            _personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
        }

        [HttpGet(Name = "NewPolicyHolder")]
        public async Task<int> New()
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

            var result = await _personRepository.Insert(person);
            await _personRepository.Save();

            return 1;
        }
    }
}
