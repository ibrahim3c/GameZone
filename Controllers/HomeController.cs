using GameZone.Models;
using GameZone.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Diagnostics;

namespace GameZone.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGamesService gamesService;
        private readonly IStringLocalizer<HomeController> stringLocalizer;

        public HomeController(IGamesService gamesService,IStringLocalizer<HomeController> stringLocalizer)
        {
            this.gamesService = gamesService;
            this.stringLocalizer = stringLocalizer;
        }

        public IActionResult Index()
        {
            ViewBag.aha = string.Format(stringLocalizer["Welcome"],"Ibrahim");
            var games = gamesService.GetAll();
            return View( games);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SetLanguage(string culture , string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return LocalRedirect(returnUrl);
        }
    }
}
