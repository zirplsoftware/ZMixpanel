using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.HtmlControls;

namespace Zirpl.Metrics.MixPanel.Web.Mvc
{
    public class InitBuilder : IHtmlString
    {
        private String _token;
        private String _instanceName;
        private readonly ConfigBuilder _configBuilder;

        public InitBuilder()
        {
            this._configBuilder = new ConfigBuilder(this);
        }

        public InitBuilder Token(String token)
        {
            this._token = token;
            return this;
        }

        public InitBuilder InstanceName(String instanceName)
        {
            this._instanceName = instanceName;
            return this;
        }

        public ConfigBuilder Config()
        {
            return this._configBuilder;
        }

        public string ToHtmlString()
        {
            if (String.IsNullOrEmpty(this._token))
            {
                throw new InvalidOperationException("Cannot call ToHtmlString without the Token set");
            }

            var builder = new StringBuilder();
            builder.AppendFormat("mixpanel.init(\"{0}\"", this._token);
            if (this._configBuilder.IsDirty
                || !String.IsNullOrEmpty(this._instanceName))
            {
                if (this._configBuilder.IsDirty)
                {
                    builder.AppendFormat(", {0}", this._configBuilder.ToPartialHtmlString());
                    if (!String.IsNullOrEmpty(this._instanceName))
                    {
                        builder.AppendFormat(", \"{0}\"", this._instanceName);
                    }
                }
                else
                {
                    builder.AppendFormat(", null, \"{0}\"", this._instanceName);
                }
            }
            builder.Append(");");
            return builder.ToString();
        }
    }
}
