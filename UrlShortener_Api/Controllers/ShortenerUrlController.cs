using Microsoft.AspNetCore.Mvc;
using UrlShortener_Application.Application_DTOs.DTOs_Othres;
using UrlShortener_Application.Application_Services.Generic_Service;
using UrlShortener_Domain.Domain_Models;

namespace UrlShortener_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShortenerUrlController : ControllerBase
    {
        private readonly IGenericService<ShortUrlModel> _urlService;
        private readonly ILogger<ShortenerUrlController> _logger;

        public ShortenerUrlController(IGenericService<ShortUrlModel> urlService, ILogger<ShortenerUrlController> logger)
        {
            _urlService = urlService;
            _logger = logger;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _urlService.GetAllAsync();
            return Ok(response);
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _urlService.GetByIdAsync(id);
            return Ok(response);
        }

        [HttpGet("{code}")]
        public async Task<IActionResult> RedirectToOriginal(string code)
        {
            var response = await _urlService.FindAsync(x => x.ShortCode == code);

            if (response.Data == null || !response.Data.Any())
                return NotFound(ResponseHelper.SetNotFound<object>(message: "Short code of URL not found."));

            var originalUrl = response.Data.First().OriginalUrl;
            return Redirect(originalUrl);
        }

        [HttpPost("shorten")]
        public async Task<IActionResult> ShortenUrl([FromBody] string originalUrl)
        {
            if (string.IsNullOrWhiteSpace(originalUrl))
                return BadRequest(ResponseHelper.SetBadRequest<object>(message: "Original URL cannot be empty."));

            try
            {
                string shortCode = GenerateShortCode();
                var shortUrl = new ShortUrlModel
                {
                    OriginalUrl = originalUrl,
                    ShortCode = shortCode
                };

                var response = await _urlService.AddAsync(shortUrl);

                string fullShortUrl = $"{Request.Scheme}://{Request.Host}/api/ShortenerUrl/{shortCode}";

                return Ok(ResponseHelper.SetSuccess(new
                {
                    ShortUrl = fullShortUrl,
                    OriginalUrl = originalUrl
                }, message: "URL shortened successfully."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to shorten URL");
                return StatusCode(500, ResponseHelper.SetInternalServerError<object>(message: "Error shortening URL."));
            }
        }

        private string GenerateShortCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

}


