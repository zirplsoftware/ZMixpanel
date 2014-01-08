using System;
using System.Text;

namespace Zirpl.Metrics.MixPanel.Web.Mvc.JavaScript
{
    public class PeopleIncrementBuilder :CallBuilderBase
    {
        private String _callback;
        private readonly PropertiesBuilder<decimal> _incrementsBuilder; 

        public PeopleIncrementBuilder()
        {
            this._incrementsBuilder = new PropertiesBuilder<decimal>(this);
        }

        public PeopleIncrementBuilder(String instanceName)
            :base(instanceName)
        {
            this._incrementsBuilder = new PropertiesBuilder<decimal>(this);
        }

        public PropertiesBuilder<decimal> Increments()
        {
            return this._incrementsBuilder;
        }

        public PeopleIncrementBuilder Callback(String value)
        {
            this._callback = value;
            return this;
        }

        public override string ToHtmlString()
        {
            var sb = new StringBuilder();
            var properties = this.Increments().ToPartialJavaScriptString();
            if (String.IsNullOrEmpty(properties))
            {
                throw new InvalidOperationException("Cannot call ToHtmlString without setting any increments");
            }
            sb.AppendFormat("{0}.people.increment({1}", base.ToHtmlString(), properties);
            if (!String.IsNullOrEmpty(this._callback))
            {
                sb.AppendFormat(", {0}", this._callback);
            }
            sb.Append(");");
            return sb.ToString();
        }
    }
}
