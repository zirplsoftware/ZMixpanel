using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zirpl.Metrics.MixPanel.HttpApi
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
