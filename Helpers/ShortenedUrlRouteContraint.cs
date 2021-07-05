using Microsoft.AspNetCore.Routing.Constraints;

namespace UrlShortener.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// Ref: https://www.hanselman.com/blog/adding-a-custom-inline-route-constraint-in-aspnet-core-10
    /// </remarks>
    public class ShortUrlRouteContraint : RegexRouteConstraint
    {
        public ShortUrlRouteContraint() : base(@"^[2-9bcdfghjkmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ\-_]+$")
        {
        }

    }
}