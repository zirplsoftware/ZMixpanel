using System;

namespace Zirpl.Mixpanel.Web.Mvc.JavaScript
{
    public class NameTagBuilder :CallBuilderBase
    {
        private String _nameTag;

        public NameTagBuilder()
        {
            
        }

        public NameTagBuilder(String instanceName)
            :base(instanceName)
        {
            
        }

        public NameTagBuilder NameTag(String value)
        {
            this._nameTag = value;
            return this;
        }

        public override string ToHtmlString()
        {
            if (String.IsNullOrEmpty(this._nameTag))
            {
                throw new InvalidOperationException("Cannot call ToHtmlString without NameTag set");
            }

            return String.Format("{0}.name_tag(\"{1}\");", base.ToHtmlString(), this._nameTag);
        }
    }
}
