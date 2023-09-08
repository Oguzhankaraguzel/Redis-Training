using Microsoft.AspNetCore.Mvc;
using Redis_Sentinel_Example.Services;

namespace Redis_Sentinel_Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Redis : ControllerBase
    {
        [HttpGet("[action]/{key}/{value}")]
        public async Task<IActionResult> SetValue(string key, string value)
        {
            var redis = await RedisService.RedisMasterDatabase();
            try
            {
                await redis.StringSetAsync(key, value);
                return CreatedAtAction(nameof(SetValue), new { Key = key, Value = value });
            }
            catch (Exception ex)
            {
                return BadRequest("Error : " + ex.Message);
            }


        }
        [HttpGet("[action]/{key}")]
        public async Task<IActionResult> GetValue(string key)
        {
            var redis = await RedisService.RedisMasterDatabase();
            var data = await redis.StringGetAsync(key);
            return Ok(new { key = data.ToString() });
        }
    }
}
