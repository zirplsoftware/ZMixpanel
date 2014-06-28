using System;
using System.Web;
using Newtonsoft.Json;
using Zirpl.Mixpanel.HttpApi;

namespace Zirpl.Mixpanel.Web.Mvc.JavaScript
{
    public class PropertiesBuilder<TValue> : Properties<TValue>, IHtmlString, IPartialJavaScriptString
    {
        private readonly IHtmlString _outermostContainer;
        private String _variableName;
        private string _createFunctionName;

        public PropertiesBuilder(IHtmlString outermostContainer)
        {
            this._outermostContainer = outermostContainer;
        }

        public PropertiesBuilder<TValue> CreateFunctionName(String createPropertiesFunctionName)
        {
            this._createFunctionName = createPropertiesFunctionName;
            return this;
        }

        public PropertiesBuilder<TValue> VariableName(String propertiesVariableName)
        {
            this._variableName = propertiesVariableName;
            return this;
        }

        public string ToHtmlString()
        {
            if (this._outermostContainer != null)
            {
                return this._outermostContainer.ToHtmlString();
            }
            else
            {
                return this.ToPartialJavaScriptString();
            }
        }

        public PropertiesBuilder<TValue> Add(String key, TValue value)
        {
            return (PropertiesBuilder<TValue>)base.Add(key, value);
        }

        public string ToPartialJavaScriptString()
        {
            int count = 0;
            count = count + (!String.IsNullOrEmpty(this._variableName) ? 1 : 0);
            count = count + (!String.IsNullOrEmpty(this._createFunctionName) ? 1 : 0);
            count = count + ((this.Count > 0) ? 1 : 0);
            if (count > 1)
            {
                throw new InvalidOperationException("VariableName OR CreateFunctionName OR properties need to be set, but only 1");
            }

            if (!String.IsNullOrEmpty(this._variableName))
            {
                return this._variableName;
            }
            else if (!String.IsNullOrEmpty(this._createFunctionName))
            {
                return String.Format("{0}()", this._createFunctionName);
            }
            else
            {
                return this.ToPropertyArrayJson(Formatting.Indented);
            }
        }
    }
}
