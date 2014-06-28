using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Zirpl.Mixpanel.HttpApi.Events;
using Zirpl.Mixpanel.HttpApi.UserProfiles;

namespace Zirpl.Mixpanel.HttpApi
{
    public abstract class ApiCallerBase :IApiCaller
    {
        //public ILog Log { get; set; }
        protected const String EventUrlTemplate = "http://api.mixpanel.com/track/?data={0}";
        protected const String PersonUrlTemplate = "http://api.mixpanel.com/engage/?data={0}";

        protected virtual HttpWebRequest GetRequest(Event eVent)
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            String data = jsonSerializer.GetJson(eVent);
            //if (this.Log != null)
            //{
            //    this.Log.DebugFormat("Json Data: {0}", data);
            //}
            data = data.Base64Encode();

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(EventUrlTemplate, data);
            if (eVent.Options.TestMode)
            {
                sb.Append("&test=1");
            }
            if (eVent.Options.MaskIpAddress)
            {
                sb.Append("&ip=1");
            }
            if (eVent.Options.Verbose)
            {
                sb.Append("&verbose=1");
            }

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(sb.ToString());
            request.Method = "GET";
            return request;
        }
        protected virtual HttpWebRequest GetRequest(PersonEventBase personEvent)
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            String data = jsonSerializer.GetJson(personEvent);
            //if (this.Log != null)
            //{
            //    this.Log.DebugFormat("Json Data: {0}", data);
            //}
            data = data.Base64Encode();

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(PersonUrlTemplate, data);
            if (personEvent.Options.TestMode)
            {
                sb.Append("&test=1");
            }
            if (personEvent.Options.MaskIpAddress)
            {
                sb.Append("&ip=1");
            }
            if (personEvent.Options.Verbose)
            {
                sb.Append("&verbose=1");
            }

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(sb.ToString());
            request.Method = "GET";
            return request;
        }

        protected virtual ApiCallResult HandleResponse(HttpWebRequest request, IAsyncResult result = null)
        {
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    String content = reader.ReadToEnd();
                    if (String.IsNullOrWhiteSpace(content))
                    {
                        throw new MixpanelHttpApiErrorException("No result returned");
                    }
                    else if (content == "1")
                    {
                        // we're good
                        return new ApiCallResult() {Status = 1, IsSuccess = true, RawResult = content};
                    }
                    else if (content == "0")
                    {
                        throw new MixpanelHttpApiErrorException(new ApiCallResult() {Status = 0, IsSuccess = false, RawResult = content}, "Error returned from MixPanel without details");
                    }
                    else
                    {
                        ApiCallResult callResult = null;
                        try
                        {
                            callResult = JsonConvert.DeserializeObject<ApiCallResult>(content);
                        }
                        catch (Exception e)
                        {
                        }
                        if (callResult == null)
                        {
                            return new ApiCallResult() {Status = 0, IsSuccess = false, RawResult = content}; 
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
                            throw new MixpanelHttpApiErrorException(callResult, "Error returned from MixPanel");
                        }
                    }
                }
            }
        }

        public abstract void Send(PersonEventBase personEvent);
        public abstract void Send(Event eVent);
    }
}
