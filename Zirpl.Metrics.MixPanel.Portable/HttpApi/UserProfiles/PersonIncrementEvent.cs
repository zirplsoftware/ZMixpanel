namespace Zirpl.Metrics.MixPanel.HttpApi.UserProfiles
{
    public class PersonIncrementEvent : PersonEventBase
    {
        protected internal PersonIncrementEvent()
        {
            this.Increments = new Properties<decimal>();
        }

        public Properties<decimal> Increments { get; private set; }
    }
}
