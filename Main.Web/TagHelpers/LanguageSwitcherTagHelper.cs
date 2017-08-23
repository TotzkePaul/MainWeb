using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

namespace Main.Web.TagHelpers
{
    [HtmlTargetElement("language-switcher")]
    public class LanguageSwitcherTagHelper : TagHelper
    {
        private readonly IOptions<RequestLocalizationOptions> _localizationOptions;

        public LanguageSwitcherTagHelper(IOptions<RequestLocalizationOptions> options)
        {
            _localizationOptions = options;
        }

        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public DisplayMode Mode { get; set; } = DisplayMode.ImageAndText;

        private void AppendScript(ref TagHelperOutput output)
        {
            output.Content.AppendHtml($@"
        <script type='text/javascript'>
            function useCookie(code) {{
                var culture = code;
                var uiCulture = code;
                var cookieValue = '{CookieRequestCultureProvider.DefaultCookieName}=c='+code+'|uic='+code+'; Path=/;'; 
                document.cookie = cookieValue; 
                window.location.reload();
            }}
        </script>");
        }
        
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var selectedCulture = ViewContext.HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture;
            var cultures = _localizationOptions.Value.SupportedUICultures;

            output.TagName = null;

            switch (Mode)
            {
                case DisplayMode.ImageAndText:
                    output.Content.AppendHtml($@"<ul class='nav navbar-nav navbar-right'>
                        <li class='dropup'>
                            <a href='#' class='dropdown-toggle footer-link' data-toggle='dropdown' role='button' aria-haspopup='true' aria-expanded='false'><span class='flag-icon flag-icon-{selectedCulture.Name}'></span> {selectedCulture.NativeName}<span class='caret'></span></a>
                            <ul class='dropdown-menu'>");
                    
                    foreach (var culture in cultures)
                    {
                        output.Content.AppendHtml($"<li><a class='language-item' href='#' onclick=\"useCookie('{culture.Name}')\"><span class='flag-icon flag-icon-{culture.Name}'></span> {culture.NativeName}</a></li>");
                    }
                    break;
                case DisplayMode.Image:
                    output.Content.AppendHtml($@"<ul class='nav navbar-nav navbar-right'>
                        <li class='dropup'>
                            <a href='#' class='dropdown-toggle' data-toggle='dropdown' role='button' aria-haspopup='true' aria-expanded='false'><span class='flag-icon flag-icon-{selectedCulture.Name}' ></span> <span class='caret'></span></a>
                            <ul class='dropdown-menu'>");
                    
                    foreach (var culture in cultures)
                    {
                        output.Content.AppendHtml($"<li><a href='#' onclick=\"useCookie('{culture.Name}')\"><span class='flag-icon flag-icon-{culture.Name}'></span></a></li>");
                    }
                    break;
                case DisplayMode.Text:
                    
                    output.Content.AppendHtml($@"<ul class='nav navbar-nav navbar-right'>
                        <li class='dropdown'>
                            <a href='#' class='dropdown-toggle' data-toggle='dropdown' role='button' aria-haspopup='true' aria-expanded='false'> {selectedCulture.EnglishName}<span class='caret'></span></a>
                            <ul class='dropdown-menu'>");
                    foreach (var culture in cultures)
                    {
                        output.Content.AppendHtml($"<li><a href='#' onclick=\"useCookie('{culture.Name}')\">{culture.EnglishName}</a></li>");
                    }
                    break;
            }
            
            output.Content.AppendHtml(@"</ul>
                        </li>
                    </ul>");
            AppendScript(ref output);
        }
    }
}