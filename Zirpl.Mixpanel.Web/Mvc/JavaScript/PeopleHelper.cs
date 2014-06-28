using System;

namespace Zirpl.Mixpanel.Web.Mvc.JavaScript
{
    public class PeopleHelper
    {
        private String _instanceName;
        public PeopleHelper()
        {
            
        }

        public PeopleHelper(String instanceName)
        {
            this._instanceName = instanceName;
        }

        public PeopleSetBuilder CallSet()
        {
            return new PeopleSetBuilder(this._instanceName);
        }
        public PeopleIncrementBuilder CallIncrement()
        {
            return new PeopleIncrementBuilder(this._instanceName);
        }
        public PeopleAppendBuilder CallAppend()
        {
            return new PeopleAppendBuilder(this._instanceName);
        }
    }
}
