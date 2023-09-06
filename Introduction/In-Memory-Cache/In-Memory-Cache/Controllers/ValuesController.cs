using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace In_Memory_Cache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IMemoryCache _memoryCache;
        public ValuesController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet("[action]/{name}")]
        public IActionResult SetName(string name)
        {
            try
            {
                _memoryCache.Set("name", name);
                return Ok("done");
            }
            catch (Exception ex)
            {
                return BadRequest("There is an error : " + ex.Message);
            }

        }

        [HttpGet("[action]")]
        public string GetName()
        {
            if (_memoryCache.TryGetValue<string>("name", out string name))
            {
                return name.Substring(3);
            }
            return "null";
        }

        [HttpGet("[action]")]
        public void SetDate()
        {
            _memoryCache.Set<DateTime>("date", DateTime.Now, options: new()
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                SlidingExpiration = TimeSpan.FromSeconds(5)
            });
        }

        [HttpGet("[action]")]
        public DateTime GetDate()
        {
            if (_memoryCache.TryGetValue<DateTime>("date", out DateTime date))
            {
                return date;
            }
            return DateTime.MinValue;
        }
    }
}
