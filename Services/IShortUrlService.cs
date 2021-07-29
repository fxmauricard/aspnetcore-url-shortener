using System.Threading.Tasks;
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public interface IShortUrlService
    {
        Task<ShortUrl> Create(string url);

        Task<ShortUrl> GetById(int id);

        Task<ShortUrl> GetByPath(string path);

        Task<ShortUrl> GetByOriginalUrl(string originalUrl);
    }
}