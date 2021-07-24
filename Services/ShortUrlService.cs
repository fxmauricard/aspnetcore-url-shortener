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

        public ShortUrl GetById(int id)
        {
            var shortUrl = _context.ShortUrls.Find(id);
            shortUrl.Url = _intEncoder.Encode(id);
            return shortUrl;
        }

        public ShortUrl GetByPath(string path)
        {
            var urlId = _intEncoder.Decode(path);
            return GetById(urlId);
        }

        public ShortUrl GetByOriginalUrl(string originalUrl)
        {
            foreach (var shortUrl in _context.ShortUrls) {
                if (shortUrl.OriginalUrl == originalUrl) {
                    return shortUrl;
                }
            }

            return null;
        }

        public int Save(ShortUrl shortUrl)
        {
            _context.ShortUrls.Add(shortUrl);
            _context.SaveChanges();

            return shortUrl.Id;
        }
    }
}
