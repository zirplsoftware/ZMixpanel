using System;

namespace Zirpl.Mixpanel.HttpApi
{
    public class MixpanelException : Exception
    {
        public MixpanelException()
            :base()
        {
        }
        
        public MixpanelException(string message)
            :base(message)
        {
        }
        
        public MixpanelException(string message, Exception innerException)
            :base(message, innerException)
        {
        }
    }
}
