using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Main.Data.Core.Domain;
using Main.Data.Persistence.Entities;

namespace Main.Data.Persistence.Repositories
{
    public class AppRepository
    {
        private readonly ApplicationDbContext _context;
        public AppRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Log> GetLogs()
        {
            return _context.Logs
              .OrderByDescending(x=>x.Timestamp)
              .ToList();
        }

        public bool AddLog(Log log)
        {
            log.Timestamp = log.Timestamp == DateTime.MinValue ? DateTime.UtcNow : log.Timestamp;
            log.Username = log.Username ?? "Unknown";
            log.Server = System.Environment.MachineName?? "Unknown";
            log.Logger = log.Logger ?? "AppRepository";
            _context.Logs.Add(log);
            return _context.SaveChanges() > 0;
        }

        public bool DeleteLogs(DateTime timestamp)
        {
            _context.Logs.RemoveRange(_context.Logs.Where(x=>x.Timestamp< timestamp));
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<Config> GetConfigByName(string name)
        {
            var lowerUserName = name.ToLowerInvariant();

            return  _context.Configs
              .Where(v => v.SettingName.ToLowerInvariant() == lowerUserName)
              .ToList();
        }

        public Config GetConfigById(int id)
        {
            return _context.Configs.FirstOrDefault(v => v.Id == id);
        }

        public T GetSetting<T>(string settingName)
        {
            return GetSetting<T>(settingName, default(T));
        }

        public T GetSetting<T>(string settingName, T defaultValue)
        {
            var list = GetConfigByName(settingName);
            var myConfig = list.FirstOrDefault();
            if (myConfig == null)
            {
                return defaultValue;
            }

            try
            {
                return (T)Convert.ChangeType(myConfig.SettingValue, typeof(T), CultureInfo.InvariantCulture);
            }
            catch
            {
                return defaultValue;
            }
        }

        public  IEnumerable<Config> GetAllConfigs()
        {
            return  _context.Configs.ToList();
        }

        public bool AddConfig(Config config)
        {
            config.Timestamp = config.Timestamp == DateTime.MinValue? DateTime.UtcNow : config.Timestamp;
            config.Username = config.Username ?? "Unknown";
            _context.Configs.Add(config);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateConfig(Config config)
        {
            _context.Configs.Update(config);
            return _context.SaveChanges() > 0;
        }

        public bool UpsertConfig(Config config)
        {
            if (config.Id == 0)
            {
                return AddConfig(config);
            }
            else
            {
                return UpdateConfig(config);
            }
        }

        public bool DeleteConfig(Config config)
        {
            _context.Configs.Remove(config);
            return _context.SaveChanges() > 0;
        }

        public bool DisableConfig(Config config)
        {
            config.IsActive = false;
            return UpdateConfig(config);
        }

        public LocalizationConfig GetLocalizationConfigById(int id)
        {
            return _context.LocalizationConfigs.FirstOrDefault(v => v.Id == id);
        }

        public IEnumerable<LocalizationConfig> GetAllLocalizationConfigs()
        {
            return _context.LocalizationConfigs.ToList();
        }

        public bool AddLocalizationConfig(LocalizationConfig config)
        {
            config.Timestamp = config.Timestamp == DateTime.MinValue ? DateTime.UtcNow : config.Timestamp;
            config.Username = config.Username ?? "Unknown";
            _context.LocalizationConfigs.Add(config);
            return _context.SaveChanges() > 0;
        }

        public bool UpdateLocalizationConfig(LocalizationConfig config)
        {
            _context.LocalizationConfigs.Update(config);
            return _context.SaveChanges() > 0;
        }

        public bool UpsertLocalizationConfig(LocalizationConfig config)
        {
            if (config.Id == 0)
            {
                return AddLocalizationConfig(config);
            }
            else
            {
                return UpdateLocalizationConfig(config);
            }
        }

        public bool DeleteLocalizationConfig(LocalizationConfig config)
        {
            _context.LocalizationConfigs.Remove(config);
            return _context.SaveChanges() > 0;
        }

        public bool DisableLocalizationConfig(LocalizationConfig config)
        {
            config.IsActive = false;
            return UpdateLocalizationConfig(config);
        }
    }
}
