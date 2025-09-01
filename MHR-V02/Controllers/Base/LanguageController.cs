using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

public class LanguageController : Controller
{
    public IActionResult SetLanguage(string culture, string returnUrl)
    {
        // زبان جدید را در کوکی ذخیره کنید
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );

        // بازگشت به صفحه قبلی
        return LocalRedirect(returnUrl);
    }


}
