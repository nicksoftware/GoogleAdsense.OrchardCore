using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
namespace GoogleAdsense.OrchardCore
{
    public class AdminMenu : INavigationProvider
    {
        private readonly IStringLocalizer<AdminMenu> loc;

        public AdminMenu(IStringLocalizer<AdminMenu> localizer)
        {
            loc = localizer;
        }

        public Task BuildNavigationAsync(string name, NavigationBuilder builder)
        {
            if (!String.Equals(name, "admin", StringComparison.OrdinalIgnoreCase))
            {
                return Task.CompletedTask;
            }

            builder.Add(loc["Configuration"], config => config
            .Add(loc["Settings"], settings => settings
                .Add(loc["Google Adsense"], loc["Google Adsense"].PrefixPosition(), set => set
                .AddClass("googleAdsense").Id("googleAdsense")
                  .Action("Index", "Admin", new { area = "OrchardCore.Settings", groupId = Constants.GroupId })
                  .LocalNav()
                )));

            return Task.CompletedTask;
        }
    }
}
