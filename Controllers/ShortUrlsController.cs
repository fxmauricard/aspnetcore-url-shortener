using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    [Authorize]
    public class ShortUrlsController : Controller
    {
        private readonly IShortUrlService _shortUrlService;

        public ShortUrlsController(IShortUrlService service)
        {
            _shortUrlService = service;
        }

        [Route("short-urls/create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("short-urls/create")]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string originalUrl)
        {
            var shortUrl = new ShortUrl
            {
                OriginalUrl = originalUrl
            };

            TryValidateModel(shortUrl);
            if (ModelState.IsValid)
            {
                _shortUrlService.Save(shortUrl);

                return RedirectToAction(actionName: nameof(Show), routeValues: new { id = shortUrl.Id });
            }

            return View(shortUrl);
        }

        [Route("short-urls/{id:int}")]
        public IActionResult Show(int id)
        {
            var shortUrl = _shortUrlService.GetById(id);
            if (shortUrl == null) 
            {
                return NotFound();
            }

            ViewData["Path"] = shortUrl.Url;

            return View(shortUrl);
        }

        
    }
}
