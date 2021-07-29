using System.ComponentModel.DataAnnotations;

namespace UrlShortener.ViewMosdels
{
    public class CreateShortUrlViewModel
    {
        [Required, StringLength(128), Url]
        public string Url { get; init; }
    }
}