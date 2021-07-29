using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UrlShortener.Services;
using UrlShortener.ViewMosdels;

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
            return View(new CreateShortUrlViewModel());
        }

        [HttpPost("short-urls/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateShortUrlViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var shortUrl = await _shortUrlService.Create(model.Url);

            return RedirectToAction(actionName: nameof(Show), routeValues: new { id = shortUrl.Id });

        }

        [Route("short-urls/{id:int}")]
        public async Task<IActionResult> Show(int id)
        {
            var shortUrl = await _shortUrlService.GetById(id);
            if (shortUrl == null) 
            {
                return NotFound();
            }

            ViewData["Path"] = shortUrl.Url;

            return View(shortUrl);
        }

        
    }
}
