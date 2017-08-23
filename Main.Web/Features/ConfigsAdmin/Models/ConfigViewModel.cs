using System;

namespace Main.Web.Features.ConfigsAdmin.Models
{
    public class ConfigViewModel
    {
        public int Id { get; set; }
        public string SettingName { get; set; }
        public string SettingType { get; set; }
        public string SettingValue { get; set; }
        public bool IsActive { get; set; }
        public string Username { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
