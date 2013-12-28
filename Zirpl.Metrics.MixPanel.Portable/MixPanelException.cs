using System;

namespace Zirpl.Metrics.MixPanel
{
    public class MixPanelException : Exception
    {
        public MixPanelException()
            :base()
        {
        }
        
        public MixPanelException(string message)
            :base(message)
        {
        }
        
        public MixPanelException(string message, Exception innerException)
            :base(message, innerException)
        {
        }
    }
}
