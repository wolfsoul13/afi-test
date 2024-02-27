using AFI.Models.Client;

namespace AFI.Handlers.Services
{
    public interface IPolicyHolderHandler
    {
        Task<int> NewPolicyHolder(PolicyHolder policyHolder);
    }
}
