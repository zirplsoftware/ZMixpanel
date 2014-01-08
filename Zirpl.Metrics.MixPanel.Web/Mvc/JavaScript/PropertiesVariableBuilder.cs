using System;
using System.Web;

namespace Zirpl.Metrics.MixPanel.Web.Mvc.JavaScript
{
    public class PropertiesVariableBuilder : IHtmlString
    {
        private String _variableName;
        private readonly PropertiesBuilder<object> _propertiesBuilder; 

        public PropertiesVariableBuilder()
        {
            this._propertiesBuilder = new PropertiesBuilder<object>(this);
        }

        public PropertiesVariableBuilder VariableName(String value)
        {
            this._variableName = value;
            return this;
        }

        public string ToHtmlString()
        {
            if (String.IsNullOrEmpty(this._variableName))
            {
                throw new InvalidOperationException("Cannot call ToHtmlString without setting VariableName");
            }

            return string.Format("var {0} = {1};", this._variableName,
                this._propertiesBuilder.ToPartialJavaScriptString());
        }
    }
}
