using System;

namespace Zirpl.Metrics.MixPanel.Web.Mvc.JavaScript
{
    public class TrackPageViewBuilder :CallBuilderBase
    {
        private String _page;

        public TrackPageViewBuilder()
        {
            
        }

        public TrackPageViewBuilder(String instanceName)
            :base(instanceName)
        {
            
        }

        public TrackPageViewBuilder Page(String value)
        {
            this._page = value;
            return this;
        }

        public override string ToHtmlString()
        {
            if (String.IsNullOrEmpty(this._page))
            {
                return String.Format("{0}.track_pageview();", base.ToHtmlString());
            }

            return String.Format("{0}.track_pageview(\"{1}\");", base.ToHtmlString(), this._page);
        }
    }
}
