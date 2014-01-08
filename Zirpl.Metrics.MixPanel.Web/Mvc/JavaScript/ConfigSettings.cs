using System;
using Newtonsoft.Json;

namespace Zirpl.Metrics.MixPanel.Web.Mvc.JavaScript
{
    public class ConfigSettings
    {
        [JsonProperty(PropertyName="cross_subdomain_cookie")]
        public bool? UseCrossSubdomainCookie { get; set; }

        [JsonProperty(PropertyName="cookie_name")]
        public String CookieName { get; set; }

        [JsonProperty(PropertyName="cookie_expiration")]
        public int? CookieLifetimeInDays { get; set; }

        [JsonProperty(PropertyName="track_pageview")]
        public bool? TrackPageView { get; set; }

        [JsonProperty(PropertyName="track_links_timeout")]
        public int? TrackLinksTimeoutInMilliseconds { get; set; }

        [JsonProperty(PropertyName="disable_cookie")]
        public bool? DisableCookie { get; set; }

        [JsonProperty(PropertyName="secure_cookie")]
        public bool? TransmitSecureCookieOnly { get; set; }

        [JsonProperty(PropertyName="upgrade")]
        public bool? UpgradeCookie { get; set; }
    }
}
