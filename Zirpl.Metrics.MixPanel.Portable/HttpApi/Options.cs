using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zirpl.Metrics.MixPanel.HttpApi
{
    public class Options
    {
        // TODO: this is not being used correctly
        public Boolean MaskIpAddress { get; set; }

        public Boolean Verbose { get; set; }
        public Boolean TestMode { get; set; }
    }
}
