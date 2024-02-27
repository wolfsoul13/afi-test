
using AFI.BusinessLogic.Entities;
using AFI.DataAccess.Repositories;
using AFI.Models.Adapters;
using AFI.Models.Client;

namespace AFI.Handlers.Services
{
    public class PolicyHolderHandler : IPolicyHolderHandler
    {
        private readonly IPersonRepository personRepository;
        private readonly IPolicyHolderAdapter policyHolderAdapter;

        public PolicyHolderHandler(IPersonRepository personRepository, IPolicyHolderAdapter policyHolderAdapter)
        {
            this.personRepository = personRepository ?? throw new ArgumentNullException(nameof(personRepository));
            this.policyHolderAdapter = policyHolderAdapter ?? throw new ArgumentNullException(nameof(policyHolderAdapter));
        }

        public async Task<int> NewPolicyHolder(PolicyHolder policyHolder)
        {
            if (policyHolder == null) throw new ArgumentNullException(nameof(policyHolder));

            var entity = policyHolderAdapter.ToEntity(policyHolder);

            var result = await personRepository.Insert(entity);
            await personRepository.Save();

            return result.Id;
        }
    }
}
