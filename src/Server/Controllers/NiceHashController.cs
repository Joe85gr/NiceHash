using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Server.Handlers;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NiceHashController : ControllerBase
    {
        private readonly INiceHashHandler _handler;
        private readonly ILogger<NiceHashController> _logger;
        
        public NiceHashController(INiceHashHandler handler, ILogger<NiceHashController> logger)
        {
            _handler = handler;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult> Get(CancellationToken cancellationToken = default)
        {
            _logger.LogCritical("Get: Retrieving NiceHash data");
            var result = await _handler.Handle(cancellationToken);

            return Ok(result);
        }
    }
}
