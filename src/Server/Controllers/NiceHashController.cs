using Microsoft.AspNetCore.Mvc;
using Library.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NiceHashController : ControllerBase
    {
        private readonly INiceHashService _niceHash;
        private CancellationTokenSource _cts = new CancellationTokenSource();
        public NiceHashController(INiceHashService niceHash)
        {
            _niceHash = niceHash;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var data = await _niceHash.GetAll(_cts.Token);

            return Ok(data);
        }
    }
}
