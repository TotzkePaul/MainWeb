using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Main.Web.TagHelpers;

namespace Main.Web.Models
{
    public class LanguageSwitcherModel
    {
        public CultureInfo SelectedCulture { get; set; }
        public List<CultureInfo> SupportedCultures { get; set; }
        public DisplayMode Mode { get; set; }
    }
}
