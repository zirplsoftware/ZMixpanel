using Zirpl.Mixpanel.HttpApi.Events;
using Zirpl.Mixpanel.HttpApi.UserProfiles;

namespace Zirpl.Mixpanel.HttpApi
{
    public interface IApiCaller
    {
        void Send(PersonEventBase personEvent);
        void Send(Event eVent);
    }
}
