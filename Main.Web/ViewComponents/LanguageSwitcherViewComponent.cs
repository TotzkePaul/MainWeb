using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Main.Web.Models;
using Main.Web.TagHelpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

namespace Main.Web.ViewComponents
{
    public class LanguageSwitcherViewComponent : ViewComponent
    {
        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;

        public LanguageSwitcherViewComponent(IOptions<RequestLocalizationOptions> options)
        {
            _localizationOptions = options;
        }

        public DisplayMode Mode { get; set; } = DisplayMode.ImageAndText;
        public async Task<IViewComponentResult> InvokeAsync()
        {
            LanguageSwitcherModel model = new LanguageSwitcherModel()
            {
                SelectedCulture = ViewContext.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture,
                SupportedCultures = _localizationOptions.Value.SupportedUICultures.ToList(),
                Mode = DisplayMode.ImageAndText
            };
            return View(model);
        }
    }
}
