using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.RazorPages;

// https://github.com/aspnet/AspNetCore.Docs/issues/13436

namespace SampleApp.Pages
{
    public class HostModel : PageModel
    {
        public void OnGet()
        {
            HttpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(
                        new RequestCulture(
                            CultureInfo.CurrentCulture,
                            CultureInfo.CurrentUICulture)));
        }
    }
}
