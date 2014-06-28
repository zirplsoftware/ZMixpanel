namespace Zirpl.Mixpanel.HttpApi.UserProfiles
{
    public class PersonSetOnceEvent : PersonEventBase
    {
        protected internal PersonSetOnceEvent()
        {
            this.Properties = new Properties<object>();
        }

        public Properties<object> Properties { get; private set; }
    }
}
