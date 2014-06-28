using System;
using System.Text;

namespace Zirpl.Mixpanel.Web.Mvc.JavaScript
{
    public class TrackBuilder :CallBuilderBase
    {
        private String _eventName;
        private String _callback;
        private readonly PropertiesBuilder<object> _propertiesBuilder; 

        public TrackBuilder()
        {
            this._propertiesBuilder = new PropertiesBuilder<object>(this);
        }

        public TrackBuilder(String instanceName)
            :base(instanceName)
        {
            this._propertiesBuilder = new PropertiesBuilder<object>(this);
        }

        public PropertiesBuilder<Object> Properties()
        {
            return this._propertiesBuilder;
        }

        public TrackBuilder EventName(String value)
        {
            this._eventName = value;
            return this;
        }

        public TrackBuilder Callback(String value)
        {
            this._callback = value;
            return this;
        }

        public override string ToHtmlString()
        {
            if (String.IsNullOrEmpty(this._eventName))
            {
                throw new InvalidOperationException("Cannot call ToHtmlString without EventName set");
            }

            var sb = new StringBuilder();
            sb.AppendFormat("{0}.track(\"{1}\"", base.ToHtmlString(), this._eventName);
            var properties = this.Properties().ToPartialJavaScriptString();
            if (!String.IsNullOrEmpty(properties)
                || !String.IsNullOrEmpty(this._callback))
            {
                sb.AppendFormat(", {0}", properties ?? "null");
                if (!String.IsNullOrEmpty(this._callback))
                {
                    sb.AppendFormat(", {0}", this._callback);
                }
            }
            sb.Append(");");
            return sb.ToString();
        }
    }
}
