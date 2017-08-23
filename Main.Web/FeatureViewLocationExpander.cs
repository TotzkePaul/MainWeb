using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Main.Web
{
    public class FeatureViewLocationExpander : IViewLocationExpander
    {
        
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            
            return new []
            {
                "/Features/{1}/Views/{0}.cshtml",
                "/Features/Shared/Views/{0}.cshtml",
                //"/Features/Shared/Views/{0}",
                "/Features/{1}/{0}.cshtml"
            };
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }
    }
}
