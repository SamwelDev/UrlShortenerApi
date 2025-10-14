using Microsoft.AspNetCore.Mvc;

namespace UrlShortener_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShortenerUrlController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateShortUrl([FromBody] string originalUrl)
        {

            return Ok();
        }
    }
}
