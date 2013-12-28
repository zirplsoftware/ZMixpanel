using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Zirpl.Core;
using Zirpl.Logging;

namespace Zirpl.Metrics.MixPanel
{
    public abstract class EventSenderBase :IEventSender
    {
        public ILog Log { get; set; }
        protected const String EventUrlTemplate = "http://api.mixpanel.com/track/?data={0}";
        protected const String PersonUrlTemplate = "http://api.mixpanel.com/engage/?data={0}";

        protected virtual HttpWebRequest GetRequest(Event eVent)
        {
            EventJsonSerializer jsonSerializer = new EventJsonSerializer();
            String data = jsonSerializer.GetJson(eVent);
            data = StringUtilities.Base64Encode(data);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(EventUrlTemplate, data);
            if (eVent.TestMode)
            {
                sb.Append("&test=1");
            }
            if (String.IsNullOrEmpty(eVent.IpAddress))
            {
                sb.Append("&ip=1");
            }
            if (eVent.Verbose)
            {
                sb.Append("&verbose=1");
            }

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(sb.ToString());
            request.Method = "GET";
            return request;
        }
        protected virtual HttpWebRequest GetRequest(PersonEventBase personEvent)
        {
            EventJsonSerializer jsonSerializer = new EventJsonSerializer();
            String data = jsonSerializer.GetJson(personEvent);
            data = StringUtilities.Base64Encode(data);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(PersonUrlTemplate, data);
            //if (personEvent.TestMode)
            //{
            //    sb.Append("&test=1");
            //}
            if (personEvent.Verbose)
            {
                sb.Append("&verbose=1");
            }

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(sb.ToString());
            request.Method = "GET";
            return request;
        }

        protected virtual EventSendResult HandleResponse(HttpWebRequest request, IAsyncResult result = null)
        {
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    String content = reader.ReadToEnd();
                    if (String.IsNullOrWhiteSpace(content))
                    {
                        throw new MixPanelApiErrorException("No result returned");
                    }
                    else if (content == "1")
                    {
                        // we're good
                        return new EventSendResult() {Status = 1, IsSuccess = true, RawResult = content};
                    }
                    else if (content == "0")
                    {
                        throw new MixPanelApiErrorException(new EventSendResult() {Status = 0, IsSuccess = false, RawResult = content}, "Error returned from MixPanel without details");
                    }
                    else
                    {
                        EventSendResult callResult = null;
                        try
                        {
                            callResult = JsonConvert.DeserializeObject<EventSendResult>(content);
                        }
                        catch (Exception e)
                        {
                        }
                        if (callResult == null)
                        {
                            return new EventSendResult() {Status = 0, IsSuccess = false, RawResult = content}; 
                        }
                        else if (callResult.Status == 1)
                        {
                            callResult.IsSuccess = true;
                            callResult.RawResult = content;
                            return callResult;
                        }
                        else
                        {
                            callResult.IsSuccess = false;
                            callResult.RawResult = content;
                            throw new MixPanelApiErrorException(callResult, "Error returned from MixPanel");
                        }
                    }
                }
            }
        }

        public abstract void Send(PersonEventBase personEvent);
        public abstract void Send(Event eVent);
    }
}
