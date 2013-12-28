namespace Zirpl.Metrics.MixPanel
{
    public interface IEventSender
    {
        void Send(PersonEventBase personEvent);
        void Send(Event eVent);
    }
}
