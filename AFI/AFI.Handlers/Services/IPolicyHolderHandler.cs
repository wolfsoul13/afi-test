using AFI.Models;
using AFI.Models.Client;

namespace AFI.Handlers.Services
{
    public interface IPolicyHolderHandler
    {
        Task<HandlerResult<int>> NewPolicyHolder(PolicyHolder policyHolder);
    }
}
