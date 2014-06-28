using System;

namespace Zirpl.Mixpanel.HttpApi
{
    public interface ICommonEvent
    {
        Options Options { get; }
        DateTime? EventTime { get; set; }
        String IpAddress { get; set; }
        String ProjectToken { get; }
        String DistinctUserId { get; set; }
    }
}
