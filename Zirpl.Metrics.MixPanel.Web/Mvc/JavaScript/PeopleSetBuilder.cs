using System;
using System.Text;

namespace Zirpl.Metrics.MixPanel.Web.Mvc.JavaScript
{
    public class PeopleSetBuilder :CallBuilderBase
    {
        private String _callback;
        private readonly PeopleSetPropertiesBuilder _propertiesBuilder; 

        public PeopleSetBuilder()
        {
            this._propertiesBuilder = new PeopleSetPropertiesBuilder(this);
        }

        public PeopleSetBuilder(String instanceName)
            :base(instanceName)
        {
            this._propertiesBuilder = new PeopleSetPropertiesBuilder(this);
        }

        public PeopleSetPropertiesBuilder Properties()
        {
            return this._propertiesBuilder;
        }

        public PeopleSetBuilder Callback(String value)
        {
            this._callback = value;
            return this;
        }

        public override string ToHtmlString()
        {
            var sb = new StringBuilder();
            var properties = this.Properties().ToPartialJavaScriptString();
            if (String.IsNullOrEmpty(properties))
            {
                throw new InvalidOperationException("Cannot call ToHtmlString without setting any properties");
            }
            sb.AppendFormat("{0}.people.set({1}", base.ToHtmlString(), properties);
            if (!String.IsNullOrEmpty(this._callback))
            {
                sb.AppendFormat(", {0}", this._callback);
            }
            sb.Append(");");
            return sb.ToString();
        }
    }
}
