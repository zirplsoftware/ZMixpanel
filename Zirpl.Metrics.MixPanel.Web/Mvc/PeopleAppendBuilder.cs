using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace Zirpl.Metrics.MixPanel.Web.Mvc
{
    public class PeopleAppendBuilder : CallBuilderBase
    {
        private String _callback;
        private readonly PropertiesBuilder<object> _listValues;

        public PeopleAppendBuilder()
        {
            this._listValues = new PropertiesBuilder<object>(this);
        }

        public PeopleAppendBuilder(String instanceName)
            : base(instanceName)
        {
            this._listValues = new PropertiesBuilder<object>(this);
        }

        public PropertiesBuilder<object> ListValues()
        {
            return this._listValues;
        }

        public PeopleAppendBuilder Callback(String value)
        {
            this._callback = value;
            return this;
        }

        public override string ToHtmlString()
        {
            var sb = new StringBuilder();
            var properties = this.ListValues().ToPartialHtmlString();
            if (String.IsNullOrEmpty(properties))
            {
                throw new InvalidOperationException("Cannot call ToHtmlString without setting any ListValues");
            }
            sb.AppendFormat("{0}.people.append({1}", base.ToHtmlString(), properties);
            if (!String.IsNullOrEmpty(this._callback))
            {
                sb.AppendFormat(", {0}", this._callback);
            }
            sb.Append(");");
            return sb.ToString();
        }
    }
}
