using System;

namespace Zirpl.Metrics.MixPanel.Web.Mvc.JavaScript
{
    public class AliasBuilder :CallBuilderBase
    {
        private String _alias;
        private String _originalId;

        public AliasBuilder()
        {
            
        }

        public AliasBuilder(String instanceName)
            :base(instanceName)
        {
            
        }

        public AliasBuilder Alias(String value)
        {
            this._alias = value;
            return this;
        }
        public AliasBuilder OriginalId(String value)
        {
            this._originalId = value;
            return this;
        }

        public override string ToHtmlString()
        {
            if (String.IsNullOrEmpty(this._alias))
            {
                throw new InvalidOperationException("Cannot call ToHtmlString without Alias set");
            }

            if (!String.IsNullOrEmpty(this._originalId))
            {
                return String.Format("{0}.alias(\"{1}\", \"{2}\");", base.ToHtmlString(), this._alias, this._originalId);
            }
            else
            {
                return String.Format("{0}.alias(\"{1}\");", base.ToHtmlString(), this._alias);
            }
        }
    }
}
