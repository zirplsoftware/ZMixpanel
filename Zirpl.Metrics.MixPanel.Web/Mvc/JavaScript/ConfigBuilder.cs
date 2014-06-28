using System;
using System.Web;
using Newtonsoft.Json;

namespace Zirpl.Mixpanel.Web.Mvc.JavaScript
{
    public class ConfigBuilder :IHtmlString, IPartialJavaScriptString
    {
        private readonly ConfigSettings _settings;
        private readonly IHtmlString _outermostBuilder;
        internal bool IsDirty { get; private set; }

        public ConfigBuilder(IHtmlString outermostBuilder)
        {
            this._outermostBuilder = outermostBuilder;
            this._settings = new ConfigSettings();
        }

        public ConfigBuilder UseCrossSubdomainCookie(bool value)
        {
            this._settings.UseCrossSubdomainCookie = value;
            this.IsDirty = true;
            return this;
        }

        public ConfigBuilder CookieName(String value)
        {
            this._settings.CookieName = value;
            this.IsDirty = true;
            return this;
        }

        public ConfigBuilder CookieLifetimeInDays(int value)
        {
            this._settings.CookieLifetimeInDays = value;
            this.IsDirty = true;
            return this;
        }

        public ConfigBuilder TrackPageView(bool value)
        {
            this._settings.TrackPageView = value;
            this.IsDirty = true;
            return this;
        }

        public ConfigBuilder TrackLinksTimeoutInMilliseconds(int value)
        {
            this._settings.TrackLinksTimeoutInMilliseconds = value;
            this.IsDirty = true;
            return this;
        }

        public ConfigBuilder DisableCookie(bool value)
        {
            this._settings.DisableCookie = value;
            this.IsDirty = true;
            return this;
        }

        public ConfigBuilder TransmitSecureCookieOnly(bool value)
        {
            this._settings.TransmitSecureCookieOnly = value;
            this.IsDirty = true;
            return this;
        }

        public ConfigBuilder UpgradeCookie(bool value)
        {
            this._settings.UpgradeCookie = value;
            this.IsDirty = true;
            return this;
        }

        public string ToHtmlString()
        {
            return this._outermostBuilder.ToHtmlString();
        }

        public string ToPartialJavaScriptString()
        {
            if (this.IsDirty)
            {
                return JsonConvert.SerializeObject(this._settings,
                    Formatting.Indented,
                    new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
            }
            return null;
        }
    }
}
