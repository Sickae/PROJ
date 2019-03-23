using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PROJ.Web.Helpers
{
    public static class HtmlExtensions
    {
        public static (string HeaderName, string RequestToken) GetAntiforgeryToken(this IHtmlHelper helper)
        {
            var httpContext = helper.ViewContext.HttpContext;
            var antiForgery = (IAntiforgery)httpContext.RequestServices.GetService(typeof(IAntiforgery));
            var tokenSet = antiForgery.GetAndStoreTokens(httpContext);

            string headerName = tokenSet.HeaderName;
            string requestToken = tokenSet.RequestToken;

            return (headerName, requestToken);
        }
    }
}
