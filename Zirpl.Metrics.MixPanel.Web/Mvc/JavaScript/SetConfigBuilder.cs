using System;

namespace Zirpl.Metrics.MixPanel.Web.Mvc.JavaScript
{
    public class SetConfigBuilder : CallBuilderBase
    {
        private readonly ConfigBuilder _configBuilder;

        public SetConfigBuilder()
        {
            this._configBuilder = new ConfigBuilder(this);
        }
        public SetConfigBuilder(String instanceName)
            : base(instanceName)
        {
            this._configBuilder = new ConfigBuilder(this);
        }

        public ConfigBuilder Config()
        {
            return this._configBuilder;
        }

        public override string ToHtmlString()
        {
            return String.Format("{0}.set_config({1});", base.ToHtmlString(), this._configBuilder.ToPartialJavaScriptString());
        }
    }
}
