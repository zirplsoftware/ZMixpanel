using Zirpl.Metrics.MixPanel.HttpApi.Events;
using Zirpl.Metrics.MixPanel.HttpApi.UserProfiles;

namespace Zirpl.Metrics.MixPanel.HttpApi
{
    public interface IApiCaller
    {
        void Send(PersonEventBase personEvent);
        void Send(Event eVent);
    }
}
