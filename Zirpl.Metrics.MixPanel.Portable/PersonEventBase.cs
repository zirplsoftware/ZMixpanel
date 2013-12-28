using System;

namespace Zirpl.Metrics.MixPanel
{
    public abstract class PersonEventBase
    {
        protected internal PersonEventBase()
        {
            
        }
        public bool MaskIpAddress { get; set; }
        public String IpAddress { get; set; }
        public String DistinctId { get; set; }
        public String ProjectToken { get; internal set; }
        public bool Verbose { get; set; }
    }
}
