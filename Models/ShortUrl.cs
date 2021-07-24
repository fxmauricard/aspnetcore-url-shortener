using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortener.Models
{
    public class ShortUrl
    {
        public int Id { get; init; }
        
        [Required]
        public string OriginalUrl { get; init; }

        [NotMapped]
        public object Url { get; set; }
    }
}