using System;

namespace Zirpl.Metrics.MixPanel.Web.Mvc.JavaScript
{
    public class IdentifyBuilder :CallBuilderBase
    {
        private String _distinctId;

        public IdentifyBuilder()
        {
            
        }

        public IdentifyBuilder(String instanceName)
            :base(instanceName)
        {
            
        }

        public IdentifyBuilder DistinctId(String value)
        {
            this._distinctId = value;
            return this;
        }

        public override string ToHtmlString()
        {
            if (String.IsNullOrEmpty(this._distinctId))
            {
                throw new InvalidOperationException("Cannot call ToHtmlString without DistinctUserId set");
            }

            return String.Format("{0}.identify(\"{1}\");", base.ToHtmlString(), this._distinctId);
        }
    }
}
