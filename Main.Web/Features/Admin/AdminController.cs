using System;
using System.Linq;
using System.Threading.Tasks;
using Main.Data.Core.Domain;
using Main.Data.Persistence.Entities;
using Main.Data.Persistence.Repositories;
using Main.Web.Features.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Main.Web.Features.Admin
{
    public class AdminController : Controller
    {
        private readonly AppRepository _appEntities;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationDbContext appEntities, UserManager<ApplicationUser> userManager)
        {
            _appEntities = new AppRepository(appEntities);
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            ViewBag.Twitter = _appEntities.GetSetting<string>("Twitter", "@totzkepaul");

            return View();
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        public async Task<IActionResult> UserCheck()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            return View(user);
        }

        public IActionResult Logs()
        {
            LogsModel model = new LogsModel();
            model.Logs = _appEntities.GetLogs().ToList();
            model.ClearLogsDate = DateTime.Now;
            return View(model);
        }

        [HttpPost]
        public IActionResult Logs(LogsModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                _appEntities.DeleteLogs(model.ClearLogsDate);
            }
            model.Logs = _appEntities.GetLogs().ToList();

            return View(model);
        }

        public IActionResult Config(int id)
        {
            ConfigsModel model = new ConfigsModel();
            model.Configs = _appEntities.GetAllConfigs().ToList();
            model.SelectedConfig = model.Configs.FirstOrDefault(x => x.Id == id);

            return View(model);
        }

        [HttpPost]
        public IActionResult Config(ConfigsModel model)
        {
            if (ModelState.IsValid && model?.SelectedConfig != null)
            {
                _appEntities.UpsertConfig(model.SelectedConfig);
            }
            model.Configs = _appEntities.GetAllConfigs().ToList();

            return View(model);
        }

        public IActionResult DeleteConfig(int id)
        {
            ConfigsModel model = new ConfigsModel();
            model.Configs = _appEntities.GetAllConfigs().ToList();
            model.SelectedConfig = model.Configs.FirstOrDefault(x => x.Id == id);

            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteConfig(ConfigsModel model)
        {
            if (ModelState.IsValid && model?.SelectedConfig != null)
            {
                _appEntities.DeleteConfig(model.SelectedConfig);
            }

            model.Configs = _appEntities.GetAllConfigs().ToList();
            model.SelectedConfig = model.Configs.FirstOrDefault(x => x.Id == model.SelectedConfig.Id);
            return View(model);
        }

        public IActionResult DisableConfig(int id)
        {
            ConfigsModel model = new ConfigsModel();
            model.Configs = _appEntities.GetAllConfigs().ToList();
            model.SelectedConfig = model.Configs.FirstOrDefault(x => x.Id == id);

            return View(model);
        }

        [HttpPost]
        public IActionResult DisableConfig(ConfigsModel model)
        {
            if (ModelState.IsValid && model?.SelectedConfig != null)
            {
                _appEntities.DisableConfig(model.SelectedConfig);
            }
            model.Configs = _appEntities.GetAllConfigs().ToList();
            model.SelectedConfig = model.Configs.FirstOrDefault(x => x.Id == model.SelectedConfig.Id);
            return View(model);
        }
    }
}