using System;

namespace Main.Web.Features.Polyglot.Models
{
    public class LocalizationConfigViewModel
    {
        public int Id { get; set; }

        public string Culture { get; set; }
        public string Parent { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }

        public bool IsActive { get; set; }
        public string Username { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
