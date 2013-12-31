using System;

namespace Zirpl.Metrics.MixPanel.HttpApi.UserProfiles
{
    public class PersonSetEvent : PersonEventBase
    {
        protected internal PersonSetEvent()
        {
            this.Properties = new Properties<object>();
        }

        public Properties<object> Properties { get; private set; }

        public String FirstName
        {
            get { return this.Properties.Get<String>("$first_name"); }
            set { this.Properties.Add("$first_name", value); }
        }
        public String LastName
        {
            get { return this.Properties.Get<String>("$last_name"); }
            set { this.Properties.Add("$last_name", value); }
        }
        public String Name
        {
            get { return this.Properties.Get<String>("$name"); }
            set { this.Properties.Add("$name", value); }
        }
        public String Phone
        {
            get { return this.Properties.Get<String>("$phone"); }
            set { this.Properties.Add("$phone", value); }
        }
        public String Email
        {
            get { return this.Properties.Get<String>("$email"); }
            set { this.Properties.Add("$email", value); }
        }
        public DateTime? Created
        {
            get { return this.Properties.Get<DateTime?>("$created"); }
            set { this.Properties.Add("$created", value); }
        }
    }
}
