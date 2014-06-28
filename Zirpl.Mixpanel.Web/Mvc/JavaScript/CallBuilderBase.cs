using System;
using System.Web;

namespace Zirpl.Mixpanel.Web.Mvc.JavaScript
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
                return String.Format("\r\nmixpanel.{0}", this._instanceName);
            }
            return "\r\nmixpanel";
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
