using System;

namespace Zirpl.Mixpanel.HttpApi
{
    public class Options
    {
        // TODO: this is not being used correctly
        public Boolean MaskIpAddress { get; set; }

        public Boolean Verbose { get; set; }
        public Boolean TestMode { get; set; }
    }
}
