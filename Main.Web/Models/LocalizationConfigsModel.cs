using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Main.Data.Core.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Main.Web.Models
{
    public class LocalizationConfigsModel
    {
        public List<LocalizationConfig> LocalizationConfigs { get; set; }
        public SelectList CultureList { get; set; }
        public SelectList ParentCultureList { get; set; }
        public LocalizationConfig SelectedLocalizationConfig { get; set; }
    }
}
