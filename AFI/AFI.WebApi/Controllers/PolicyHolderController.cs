using Microsoft.AspNetCore.Mvc;

namespace AFI.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PolicyHolderController : ControllerBase
    {
        private readonly ILogger<PolicyHolderController> _logger;

        public PolicyHolderController(ILogger<PolicyHolderController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "NewPolicyHolder")]
        public async Task<int> New()
        {
            return 1;
        }
    }
}
