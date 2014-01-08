using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace Zirpl.Metrics.MixPanel.Web.Mvc
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
            var properties = this.Increments().ToPartialHtmlString();
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
