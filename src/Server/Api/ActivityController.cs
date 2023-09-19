using System.Threading;
using System.Threading.Tasks;
using Domain.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Server.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly INiceHashHandler _handler;
        private readonly ILogger<ActivityController> _logger;
        
        public ActivityController(INiceHashHandler handler, ILogger<ActivityController> logger)
        {
            _handler = handler;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> Get(CancellationToken cancellationToken = default)
        {
            var result = await _handler.Handle(cancellationToken);

            return Ok(result);
        }
    }
}
