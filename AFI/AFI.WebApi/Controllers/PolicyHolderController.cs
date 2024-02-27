using AFI.BusinessLogic.Entities;
using AFI.DataAccess.Repositories;
using AFI.Handlers.Services;
using AFI.Models.Client;
using Microsoft.AspNetCore.Mvc;

namespace AFI.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PolicyHolderController : ControllerBase
    {
        private readonly ILogger<PolicyHolderController> _logger;
        private readonly IPolicyHolderHandler policyHolderHandler;

        public PolicyHolderController(ILogger<PolicyHolderController> logger, IPolicyHolderHandler policyHolderHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.policyHolderHandler = policyHolderHandler ?? throw new ArgumentNullException(nameof(policyHolderHandler));
        }

   
        [HttpPost(Name = "NewPolicyHolder")]
        public async Task<IResult> New([FromBody] PolicyHolder policyHolder)
        {
            var result = await policyHolderHandler.NewPolicyHolder(policyHolder);


            return TypedResults.Created($"/policyHolder/{result}", policyHolder);

        }
    }
}
