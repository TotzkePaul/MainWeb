using System.Linq;
using System.Threading.Tasks;
using Main.Data.Core.Domain;
using Main.Data.Persistence.Entities;
using Main.Data.Persistence.Repositories;
using Main.Web.Features.ConfigsAdmin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Main.Web.Features.ConfigsAdmin
{
    public class ConfigsAdminController : Controller
    {
        private readonly AppRepository _appEntities;
        public ConfigsAdminController(ApplicationDbContext appEntities)
        {
            _appEntities = new AppRepository(appEntities);
        }

        public async Task<ActionResult> Index()
        {
            var configs = _appEntities.GetAllConfigs().ToList();
            return View(configs);
        }

        public async Task<ActionResult> Details(string id)
        {
            int configId;
            if (id == null || !int.TryParse(id, out configId))
            {
                return new StatusCodeResult(StatusCodes.Status400BadRequest);
            }
            var config = _appEntities.GetConfigById(configId);
            if (config == null)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
            ConfigViewModel model = new ConfigViewModel()
            {
                Id = config.Id,
                SettingName = config.SettingName,
                SettingValue = config.SettingValue,
                SettingType = config.SettingType,
                Timestamp = config.Timestamp,
                Username = config.Username,
                IsActive = config.IsActive
            };

            return View(model);
        }

        public async Task<ActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ConfigViewModel configViewModel, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var config = new Config()
                {
                    Id = configViewModel.Id,
                    SettingName = configViewModel.SettingName,
                    SettingValue = configViewModel.SettingValue,
                    SettingType = configViewModel.SettingType
                };

                _appEntities.UpsertConfig(config);
                return RedirectToAction("Index");
            }
            
            return View();
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
            var config = _appEntities.GetConfigById(configId);
            if (config == null)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
            
            ConfigViewModel model = new ConfigViewModel()
            {
                Id = config.Id,
                SettingName = config.SettingName,
                SettingValue = config.SettingValue,
                SettingType = config.SettingType,
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
        public async Task<ActionResult> Edit(ConfigViewModel configViewModel, params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                var config = _appEntities.GetConfigById(configViewModel.Id);
                if (config == null)
                {
                    return new StatusCodeResult(StatusCodes.Status404NotFound);
                }


                config = new Config()
                {
                    Id = configViewModel.Id,
                    SettingName = configViewModel.SettingName,
                    SettingValue = configViewModel.SettingValue,
                    SettingType = configViewModel.SettingType,
                    Timestamp = configViewModel.Timestamp,
                    Username = configViewModel.Username,
                    IsActive = configViewModel.IsActive
                };

                _appEntities.UpsertConfig(config);
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
            var config = _appEntities.GetConfigById(configId);
            if (config == null)
            {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }
            ConfigViewModel model = new ConfigViewModel()
            {
                Id = config.Id,
                SettingName = config.SettingName,
                SettingValue = config.SettingValue,
                SettingType = config.SettingType,
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
                var config = _appEntities.GetConfigById(configId);
                if (config == null)
                {
                    return new StatusCodeResult(StatusCodes.Status404NotFound);
                }

                _appEntities.DeleteConfig(config);

                return RedirectToAction("Index");
            }
            return View();
        }
    }
}