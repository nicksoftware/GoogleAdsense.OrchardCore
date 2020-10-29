using System;
using System.Collections.Generic;
using System.Text;
using GoogleAdsense.OrchardCore;

namespace GoogleAdsense.OrchardCore.Settings
{
    public class GoogleAdsenseSettings
    {

        public string GoogleAdClient { get; set; }

        public string ScriptUrl { get; set; }

        public string GoogleAdsenseScript
        {
            get => "<script data-ad-client=\" " + GoogleAdClient + "\" async src=\"" + ScriptUrl + "\"></script>";

        }
    }
}
