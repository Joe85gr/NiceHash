using System.Threading;
using System.Threading.Tasks;
using Domain.Handlers;
using Microsoft.AspNetCore.Mvc;

namespace Server.Api;

[Route("api/[controller]")]
[ApiController]
public class ActivityController : ControllerBase
{
    private readonly INiceHashHandler _handler;
    
    public ActivityController(INiceHashHandler handler)
    {
        _handler = handler;
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken = default)
    {
        var result = await _handler.Handle(cancellationToken);

        return result.IsSuccess 
            ? Ok(result.Value) 
            : BadRequest(result.Errors[0].Message);
    }
}