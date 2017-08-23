using System.Security.Claims;
using Main.Data.Core.Domain;
using Main.Data.Persistence.Entities;
using Main.Data.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Main.Web.Features.Home
{
    public class HomeController : Controller
    {
        private readonly AppRepository _appEntities;
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(ApplicationDbContext appEntities, IStringLocalizer<HomeController> localizer)
        {
            _appEntities = new AppRepository(appEntities);
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            return View();

        }

        public IActionResult About()
        {
            ViewData["Message"] = _localizer["About"];
            var log = new Log()
            {
                Message = "Visited /About",
                Username = HttpContext.User?.FindFirst(ClaimTypes.Name)?.Value
            };

            _appEntities.AddLog(log);

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Profile()
        {
            ViewData["Message"] = "Your profile page.";

            return View();
        }


        public IActionResult Error()
        {
            return View();
        }
    }
}
