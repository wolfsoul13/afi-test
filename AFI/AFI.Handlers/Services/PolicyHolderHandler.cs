
using AFI.BusinessLogic.Entities;
using AFI.DataAccess.Repositories;
using AFI.Models.Adapters;
using AFI.Models.Client;
using FluentValidation;
using AFI.Models;

namespace AFI.Handlers.Services
{
    public class PolicyHolderHandler : IPolicyHolderHandler
    {
        private readonly IPersonRepository personRepository;
        private readonly IPolicyHolderAdapter policyHolderAdapter;
        private readonly IValidator<Person> policyHolderValidator;

        public PolicyHolderHandler(IPersonRepository personRepository, IPolicyHolderAdapter policyHolderAdapter, 
            IValidator<Person> policyHolderValidator)
        {
            this.personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            this.policyHolderAdapter = policyHolderAdapter ?? throw new ArgumentNullException(nameof(policyHolderAdapter));
            this.policyHolderValidator = policyHolderValidator ?? throw new ArgumentNullException(nameof(policyHolderValidator));
        }

        public async Task<HandlerResult<int>> NewPolicyHolder(PolicyHolder policyHolder)
        {
            if (policyHolder == null) throw new ArgumentNullException(nameof(policyHolder));

            var outcome = new HandlerResult<int>();

            var person = policyHolderAdapter.ToEntity(policyHolder);

            var validationResult = policyHolderValidator.Validate(person);

            outcome.ValidationResult = validationResult;

            if (!validationResult.IsValid)
                return outcome;

            var result = await personRepository.Insert(person);
            await personRepository.Save();

            outcome.Result = result.Id;

            return outcome;
        }
    }
}
