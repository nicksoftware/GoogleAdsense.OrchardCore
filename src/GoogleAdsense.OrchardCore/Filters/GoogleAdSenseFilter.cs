using GoogleAdsense.OrchardCore.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrchardCore.Admin;
using OrchardCore.Entities;
using OrchardCore.ResourceManagement;
using OrchardCore.Settings;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Html;

namespace GoogleAdsense.OrchardCore.Filters
{
    public class GoogleAdsenseFilter : IAsyncResultFilter
    {
        private readonly IResourceManager _resourceManager;
        private readonly ISiteService _siteService;

        public GoogleAdsenseFilter(
            IResourceManager resourceManager,
            ISiteService siteService)
        {
            _resourceManager = resourceManager;
            _siteService = siteService;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
           //Only inject in client not admin
            if ((context.Result is ViewResult 
                || context.Result is PageResult) 
                && !AdminAttribute.IsApplied(context.HttpContext))
            {
                var settings = (await _siteService.GetSiteSettingsAsync())
                    .As<GoogleAdsenseSettings>();

                
                if (!string.IsNullOrEmpty(settings.ScriptUrl)  && !string.IsNullOrEmpty(settings.GoogleAdClient))
                {
                        
                     string script = " <script data-ad-client =\"" + settings.GoogleAdClient + "\" async src=\""+ settings.ScriptUrl +"\"></script>";
                     HtmlString htmlString = new HtmlString(script);

                        _resourceManager.RegisterHeadScript(htmlString);
                }
            }

            await next.Invoke();
        }
    }
}
