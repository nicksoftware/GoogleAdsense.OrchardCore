using System;
using GoogleAdsense.OrchardCore.Drivers;
using GoogleAdsense.OrchardCore.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.Modules;
using OrchardCore.Navigation;
using OrchardCore.Security.Permissions;
using OrchardCore.Settings;

namespace GoogleAdsense.OrchardCore
{
    public class Startup : StartupBase
    {
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDisplayDriver<ISite>, GoogleAdsenseSettingsDisplayDrivers>();
            services.AddScoped<INavigationProvider, AdminMenu>();
            services.AddScoped<IPermissionProvider, Permissions>();
            
            services.Configure<MvcOptions>((options) =>
            {
                options.Filters.Add(typeof(GoogleAdsenseFilter));
            });
        }

    }
}