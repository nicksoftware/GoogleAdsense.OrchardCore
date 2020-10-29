using OrchardCore.Security.Permissions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoogleAdsense.OrchardCore
{
    public class Permissions : IPermissionProvider
    {
        public static readonly Permission ManageGoogleAdsense = new Permission("Google Adsense", "Manage Google Adsense");

        public Task<IEnumerable<Permission>> GetPermissionsAsync()
        {
            return Task.FromResult(new[] { ManageGoogleAdsense }.AsEnumerable());
        }

        public IEnumerable<PermissionStereotype> GetDefaultStereotypes()
        {
            return new[]
            {
                new PermissionStereotype
                {
                    Name = "Administrator",
                    Permissions = new[] { ManageGoogleAdsense }
                }
            };
        }
    }
}
