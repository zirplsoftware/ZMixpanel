using System;

namespace Zirpl.Metrics.MixPanel
{
    public class EventSendResult
    {
        public Int32 Status { get; set; }
        public String Error { get; set; }
        public bool IsSuccess { get; set; }
        public String RawResult { get; set; }
    }
}
