using System;

namespace Zirpl.Mixpanel.HttpApi.Events
{
    public class Event : ICommonEvent
    {
        protected internal Event()
        {
            this.Properties = new Properties<object>();
            this.Options = new Options();
        }

        public Properties<object> Properties { get; private set; }
        public Options Options { get; private set; }

        public String EventName { get; set; }
        public DateTime? EventTime
        {
            get { return this.Properties.Get<DateTime?>("time"); }
            set { this.Properties.Add("time", value); }
        }
        public String IpAddress
        {
            get { return this.Properties.Get<String>("ip"); }
            set { this.Properties.Add("ip", value); }
        }
        public String ProjectToken
        {
            get { return this.Properties.Get<String>("token"); }
            internal set { this.Properties.Add("token", value); }
        }
        public String DistinctUserId
        {
            get { return this.Properties.Get<String>("distinct_id"); }
            set { this.Properties.Add("distinct_id", value); }
        }
        public String UserNameTag
        {
            get { return this.Properties.Get<String>("mp_name_tag"); }
            set { this.Properties.Add("mp_name_tag", value); }
        }
    }
}
