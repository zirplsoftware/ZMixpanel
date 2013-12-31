using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Zirpl.Metrics.MixPanel.Web.Mvc
{
    public class PropertiesBuilder<TValue> : Properties<TValue>, IHtmlString
    {
        private readonly IHtmlString _outermostContainer;

        public PropertiesBuilder(IHtmlString outermostContainer)
        {
            this._outermostContainer = outermostContainer;
        }

        public string ToHtmlString()
        {
            return this._outermostContainer.ToHtmlString();
        }

        public PropertiesBuilder<TValue> Add(String key, TValue value)
        {
            return (PropertiesBuilder<TValue>)base.Add(key, value);
        }
    }
}
