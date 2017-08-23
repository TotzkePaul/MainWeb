using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace Main.Web
{
    public static class FeaturesRazorViewEngineOptions
    {
        /// <summary>
        /// Use feature folders with custom options
        /// </summary>
        public static RazorViewEngineOptions AddFeatureFolders(this RazorViewEngineOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }
            
            options.AreaViewLocationFormats.Add("/Features/{1}/Views/{0}.cshtml");
            options.AreaViewLocationFormats.Add("/Features/Shared/Views/{0}.cshtml");
            options.AreaViewLocationFormats.Add("/Features/{1}/{0}.cshtml");
            options.ViewLocationFormats.Add("/Features/{1}/Views/{0}.cshtml");
            options.ViewLocationFormats.Add("/Features/Shared/Views/{0}.cshtml");
            options.ViewLocationFormats.Add("/Features/{1}/{0}.cshtml");
            options.ViewLocationExpanders.Add(new FeatureViewLocationExpander());

            return options;
        }
    }
}
