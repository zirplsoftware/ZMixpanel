﻿using System;
using System.Net;
using Zirpl.Mixpanel.HttpApi.Events;
using Zirpl.Mixpanel.HttpApi.UserProfiles;

namespace Zirpl.Mixpanel.HttpApi
{
    public class AsyncApiCaller : ApiCallerBase
    {
        public override void Send(PersonEventBase personEvent)
        {
                HttpWebRequest request = GetRequest(personEvent);
                IAsyncResult result = request.BeginGetResponse(new AsyncCallHandler(request, 
                    //this.Log, 
                    this.HandleResponse).HandleAsyncCallback, null);
        }

        public override void Send(Event eVent)
        {
                HttpWebRequest request = GetRequest(eVent);
                IAsyncResult result = request.BeginGetResponse(new AsyncCallHandler(request, 
                    //this.Log, 
                    this.HandleResponse).HandleAsyncCallback, null);
        }

        private class AsyncCallHandler
        {
            private HttpWebRequest request;
            //private ILog log;
            private Func<HttpWebRequest, IAsyncResult, ApiCallResult> callbackHandler;

            public AsyncCallHandler(HttpWebRequest request, 
                //ILog log, 
                Func<HttpWebRequest, IAsyncResult, ApiCallResult> callbackHandler)
            {
                //this.log = log;
                this.request = request;
                this.callbackHandler = callbackHandler;
            }

            public void HandleAsyncCallback(IAsyncResult result)
            {
                try
                {
                    callbackHandler(request, result);
                }
                catch (Exception e)
                {
                    //if (this.log != null)
                    //{
                    //    this.log.TryError(e, "An error occurred attempting to process a response from MixPanel");
                    //}
                }
            }
        }

    }
}
