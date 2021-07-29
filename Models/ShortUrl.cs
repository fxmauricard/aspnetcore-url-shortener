using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models
{
    public class ShortUrl
    {
        public int Id { get; init; }
        
        [Required, StringLength(128)]
        public string OriginalUrl { get; init; }

        [StringLength(16)]
        public string Url { get; set; }
    }
}