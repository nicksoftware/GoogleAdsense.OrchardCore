using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoogleAdsense.OrchardCore.ViewModels
{
    public class GoogleAdsenseSettingsViewModel
    {
        public string GoogleAdClient { get; set; }
        public string ScriptUrl { get; set; } = "https://pagead2.googlesyndication.com/pagead/js/adsbygoogle.js";
 
    }
}
