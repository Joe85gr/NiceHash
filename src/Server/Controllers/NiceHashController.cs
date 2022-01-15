using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Server.Queries;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NiceHashController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public NiceHashController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> Get(CancellationToken cancellationToken)
        {
            var query = new NiceHashQuery();
            var result = await _mediator.Send(query, cancellationToken);

            return Ok(result);
        }
    }
}
