using System;
using System.Text;

namespace Zirpl.Mixpanel.HttpApi
{
    public class MixpanelHttpApiErrorException : MixpanelException
    {
        public ApiCallResult Result { get; set; }

        public MixpanelHttpApiErrorException()
            :base()
        {
        }
        
        public MixpanelHttpApiErrorException(string message)
            :base(message)
        {
        }

        public MixpanelHttpApiErrorException(string message, Exception innerException)
            :base(message, innerException)
        {
        }
        public MixpanelHttpApiErrorException(ApiCallResult result)
            : base()
        {
            this.Result = result;
        }

        public MixpanelHttpApiErrorException(ApiCallResult result, string message)
            : base(message)
        {
            this.Result = result;
        }

        public MixpanelHttpApiErrorException(ApiCallResult result, string message, Exception innerException)
            : base(message, innerException)
        {
            this.Result = result;
        }


        public override string Message
        {
            get
            {
                var message = new StringBuilder();
                message.Append(base.Message);
                if (this.Result != null)
                {
                    if (message.Length > 0)
                    {
                        message.Append(": ");
                    }
                    message.AppendFormat("Status: {0}, Error: {1}, RawResult: {2}", this.Result.Status, this.Result.Error, this.Result.RawResult);
                }
                return message.ToString();
            }
        }
    }
}
