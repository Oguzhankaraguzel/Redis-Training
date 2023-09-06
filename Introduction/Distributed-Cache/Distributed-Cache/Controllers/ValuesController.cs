using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;

namespace Distributed_Cache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IDistributedCache _distributedCache;
        public ValuesController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Set(string name, string sirName)
        {
            try
            {
                await _distributedCache.SetStringAsync("name", name, options: new()
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                    SlidingExpiration = TimeSpan.FromSeconds(5)
                });
                await _distributedCache.SetAsync("sirName", Encoding.UTF8.GetBytes(sirName), options: new()
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                    SlidingExpiration = TimeSpan.FromSeconds(5)
                });
                return CreatedAtAction(nameof(Set), new { Name = name, SirName = sirName });
            }
            catch (Exception ex)
            {
                return BadRequest("Error : " + ex.Message);
            }


        }
        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            try
            {
                string name = await _distributedCache.GetStringAsync("name");
                byte[] sirNameBinary = await _distributedCache.GetAsync("sirName");
                string sirName = Encoding.UTF8.GetString(sirNameBinary);
                string fullName = name + " " + sirName;
                return Ok(new
                {
                    name,
                    sirName,
                    fullName
                });
            }
            catch (Exception)
            {
                return NotFound();
            }

        }
    }
}
