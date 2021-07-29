using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    public class ShortUrlRedirectController : Controller
    {
        private readonly IShortUrlService _shortUrlService;

        public ShortUrlRedirectController(IShortUrlService service)
        {
            _shortUrlService = service;
        }

        [HttpGet("{path:shorturl}")]
        public async Task<IActionResult> RedirectTo(string path)
        {
            if (path == null)
            {
                return NotFound();
            }

            var shortUrl = await _shortUrlService.GetByPath(path);
            if (shortUrl == null)
            {
                return NotFound();
            }

            return Redirect(shortUrl.OriginalUrl);
        }
    }
}
