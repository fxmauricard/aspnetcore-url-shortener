using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Data;
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public class ShortUrlService : IShortUrlService
    {
        private readonly UrlShortenerContext _context;
        private readonly IIntEncoder _intEncoder;

        public ShortUrlService(UrlShortenerContext context, IIntEncoder encoder)
        {
            _context = context;
            _intEncoder = encoder;
        }

        public async Task<ShortUrl> Create(string url)
        {
            var shortUrl = new ShortUrl
            {
                OriginalUrl = url
            };

            _context.ShortUrls.Add(shortUrl);
            await _context.SaveChangesAsync();
            
            shortUrl.Url = _intEncoder.Encode(shortUrl.Id);
            await _context.SaveChangesAsync();

            return shortUrl;
        }

        public async Task<ShortUrl> GetById(int id)
        {
            var shortUrl = await _context.ShortUrls.FindAsync(id);            
            return shortUrl;
        }

        public async Task<ShortUrl> GetByPath(string path)
        {
            var shortUrl = await _context.ShortUrls.FirstOrDefaultAsync(x => x.Url == path);
            return shortUrl;
        }

        public async Task<ShortUrl> GetByOriginalUrl(string originalUrl)
        {
            var shortUrl = await _context.ShortUrls.FirstOrDefaultAsync(x => x.OriginalUrl == originalUrl);
            return shortUrl;
        }
    }
}
