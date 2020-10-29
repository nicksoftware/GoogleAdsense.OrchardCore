using System;
using System.Threading.Tasks;
using OrchardCore.DisplayManagement.Entities;
using OrchardCore.DisplayManagement.Handlers;
using OrchardCore.DisplayManagement.Views;
using OrchardCore.Environment.Shell;

using GoogleAdsense.OrchardCore.ViewModels;
using OrchardCore.Settings;
using GoogleAdsense.OrchardCore.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace GoogleAdsense.OrchardCore.Drivers
{
    public class GoogleAdsenseSettingsDisplayDrivers : SectionDisplayDriver<ISite, GoogleAdsenseSettings>
    {
 
        private readonly IShellHost _shellHost;
        private readonly ShellSettings _shellSettings;
        private readonly IAuthorizationService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GoogleAdsenseSettingsDisplayDrivers(IShellHost shellHost,
            ShellSettings shellSettings,
             IAuthorizationService authService,
             IHttpContextAccessor httpContextAccessor)
        {
            _shellHost = shellHost;
            _shellSettings = shellSettings;
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async override Task<IDisplayResult> EditAsync(GoogleAdsenseSettings section, BuildEditorContext context)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (!await _authService.AuthorizeAsync(user, Permissions.ManageGoogleAdsense))
                return null;

            return Initialize<GoogleAdsenseSettingsViewModel>("GoogleAdsenseSettings_Edit", model =>
            {
                model.GoogleAdClient = section.GoogleAdClient?.ToString() ?? string.Empty;
                model.ScriptUrl = section.ScriptUrl?.ToString() ?? string.Empty;

            }).Location("Content").OnGroup(Constants.GroupId);
        }


        public override async Task<IDisplayResult> UpdateAsync(GoogleAdsenseSettings section, BuildEditorContext context)
        {
        var user = _httpContextAccessor.HttpContext?.User;

        if (!await _authService.AuthorizeAsync(user, Permissions.ManageGoogleAdsense))
            return null;

        if (context.GroupId == Constants.GroupId)
            {
                var model = new GoogleAdsenseSettingsViewModel();

                if (await context.Updater.TryUpdateModelAsync(model, Prefix))
                {
                    section.GoogleAdClient = model.GoogleAdClient?.Trim();
                    section.ScriptUrl = model.ScriptUrl?.Trim();

                    // Release the tenant to apply settings.
                    await _shellHost.ReleaseShellContextAsync(_shellSettings);
                }
            }

            return await EditAsync(section, context);
        }
    }

}
