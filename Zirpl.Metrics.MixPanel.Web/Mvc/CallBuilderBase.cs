using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Zirpl.Metrics.MixPanel.Web.Mvc
{
    public abstract class CallBuilderBase : IHtmlString//, IHideObjectMembers
    {
        private String _instanceName;

        protected CallBuilderBase()
        {
            
        }

        protected CallBuilderBase(String instanceName)
        {
            this._instanceName = instanceName;
        }

        public virtual String ToHtmlString()
        {
            if (!String.IsNullOrEmpty(this._instanceName))
            {
                return String.Format("mixpanel.{0}", this._instanceName);
            }
            return "mixpanel";
        }

        //bool IHideObjectMembers.Equals(object value)
        //{
        //    return base.Equals(value);
        //}

        //int IHideObjectMembers.GetHashCode()
        //{
        //    return base.GetHashCode();
        //}

        //Type IHideObjectMembers.GetType()
        //{
        //    return base.GetType();
        //}

        //string IHideObjectMembers.ToString()
        //{
        //    return base.ToString();
        //}
    }
}
