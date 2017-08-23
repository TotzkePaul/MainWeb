using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Main.Data.Core.Domain;
using Main.Data.Persistence.Entities;
using Main.Data.Persistence.Repositories;
using Main.Web.Features.Polyglot.Models;
using Main.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Main.Web.Features.Polyglot
{
    public class PolyglotController : Controller
    {
        private readonly AppRepository _appEntities;
        public PolyglotController(ApplicationDbContext appEntities)
        {
            _appEntities = new AppRepository(appEntities);
        }

        public async Task<ActionResult> Index()
        {
            LocalizationConfigsModel model = new LocalizationConfigsModel();
            model.LocalizationConfigs = _appEntities.GetAllLocalizationConfigs().ToList();

            return View(model);
        }

        public async Task<ActionResult> Details(string id)
        {
            int configId;
            if (id == null || !int.TryParse(id, out configId))
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            var config = _appEntities.GetLocalizationConfigById(configId);
            if (config == null)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
            LocalizationConfigViewModel model = new LocalizationConfigViewModel()
            {
                Id = config.Id,
                Culture = config.Culture,
                Parent = config.Parent,
                Key = config.Key,
                Value = config.Value,
                Timestamp = config.Timestamp,
                Username = config.Username,
                IsActive = config.IsActive
            };

            return View(model);
        }

        public async Task<ActionResult> Create(LocalizationConfigsModel model)
        {
            var supportedCultures = new[]
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("en-GB"),
                    new CultureInfo("en-AU"),

                    new CultureInfo("zh-CN"),
                    new CultureInfo("zh-TW"),
                    new CultureInfo("zh-HK"),

                    new CultureInfo("es"),
                    new CultureInfo("fr"),


                    new CultureInfo("hi-IN"),
                    new CultureInfo("ar"),

                    new CultureInfo("pt-BR"),

                    new CultureInfo("ko-KR"),

                    new CultureInfo("ta-IN"),

                    new CultureInfo("ru-RU"),
                    new CultureInfo("de-DE"),

                    new CultureInfo("ja-JP")
                };

            model.CultureList = new SelectList(supportedCultures.ToList().Select(x => new {Id = x.Name, Value = x.Name}), "Id","Value");
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(LocalizationConfigsModel model, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var culture = new CultureInfo(model.SelectedLocalizationConfig.Culture);
                var config = new LocalizationConfig()
                {
                    Id = model.SelectedLocalizationConfig.Id,
                    Culture = model.SelectedLocalizationConfig.Culture,
                    Parent = culture.Parent.Name,
                    Key = model.SelectedLocalizationConfig.Key,
                    Value = model.SelectedLocalizationConfig.Value,
                };

                var b = _appEntities.UpsertLocalizationConfig(config);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        //
        // GET: /Users/Edit/1
        public async Task<ActionResult> Edit(string id)
        {
            int configId;
            if (id == null || !int.TryParse(id, out configId))
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            var config = _appEntities.GetLocalizationConfigById(configId);
            if (config == null)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }

            LocalizationConfigViewModel model = new LocalizationConfigViewModel()
            {
                Id = config.Id,
                Culture = config.Culture,
                Parent = config.Parent,
                Key = config.Key,
                Value = config.Value,
                Timestamp = config.Timestamp,
                Username = config.Username,
                IsActive = config.IsActive
            };

            return View(model);
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(LocalizationConfigViewModel model, params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                var config = _appEntities.GetLocalizationConfigById(model.Id);
                if (config == null)
                {
                    return new StatusCodeResult(StatusCodes.Status404NotFound);
                }


                config = new LocalizationConfig()
                {
                    Id = model.Id,
                    Culture = model.Culture,
                    Parent = model.Parent,
                    Key = model.Key,
                    Value = model.Value,
                    Timestamp = model.Timestamp,
                    Username = model.Username,
                    IsActive = model.IsActive
                };

                _appEntities.UpsertLocalizationConfig(config);
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        //
        // GET: /Users/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            int configId;
            if (id == null || !int.TryParse(id, out configId))
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            var config = _appEntities.GetLocalizationConfigById(configId);
            if (config == null)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
            LocalizationConfigViewModel model = new LocalizationConfigViewModel()
            {
                Id = config.Id,
                Culture = config.Culture,
                Parent = config.Parent,
                Key = config.Key,
                Value = config.Value,
                Timestamp = config.Timestamp,
                Username = config.Username,
                IsActive = config.IsActive
            };
            return View(model);
        }

        //
        // POST: /Users/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, ApplicationUser appuser)
        {
            if (ModelState.IsValid)
            {
                int configId;
                if (id == null || !int.TryParse(id, out configId))
                {
                    return new StatusCodeResult(StatusCodes.Status400BadRequest);
                }
                var config = _appEntities.GetLocalizationConfigById(configId);
                if (config == null)
                {
                    return new StatusCodeResult(StatusCodes.Status404NotFound);
                }

                _appEntities.DeleteLocalizationConfig(config);

                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
