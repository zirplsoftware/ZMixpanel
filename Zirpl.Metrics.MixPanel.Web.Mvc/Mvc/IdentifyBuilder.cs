using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Zirpl.Metrics.MixPanel.Web.Mvc
{
    public class IdentifyBuilder :CallBuilderBase, IHideObjectMembers
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
                throw new InvalidOperationException("Cannot call ToHtmlString without DistinctId set");
            }

            return String.Format("{0}.identify(\"{1}\");", base.ToHtmlString(), this._distinctId);
        }
    }
}
