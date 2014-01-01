using System;
using System.Net;
using NUnit.Framework;
using Zirpl.Core;
using Zirpl.Metrics.MixPanel.HttpApi;
using Zirpl.Metrics.MixPanel.HttpApi.Events;
using Zirpl.Metrics.MixPanel.HttpApi.UserProfiles;

namespace Zirpl.Metrics.MixPanel.Portable.Tests
{
    [TestFixture]
    public class EventSenderTests
    {
        [Test]
        public void TestSend_Event()
        {
            Event e = new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949").CreateEvent();
            e.Properties.Add("p1", "v1");
            e.EventName = ("Test Event");
            e.IpAddress =("1.2.3.4");
            e.Options.TestMode = true;
            e.EventTime =(DateTime.Now);
            e.DistinctUserId =("zirplsoftware@gmail.com");

            new TestSyncApiCaller().Send(e);
        }
        [Test]
        public void TestSend_PersonSetEvent()
        {
            var e = new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949").CreatePersonSetEvent();
            e.Properties.Add("p1", "v1");
            e.Created = DateTime.Today;
            e.Email = "zirplsoftware@gmail.com";
            e.FirstName = "Nathan";
            e.LastName = "LaFratta";
            e.IgnoreTime = true;
            e.Name = "someone";
            e.Phone = "freebirdnal";
            e.IpAddress = "1.2.3.4";
            e.DistinctUserId = "abc123";
            e.Options.Verbose = true;

            new TestSyncApiCaller().Send(e);
        }
        [Test]
        public void TestSend_PersonSetOnceEvent()
        {
            var e = new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949").CreatePersonSetOnceEvent();
            e.Properties.Add("p1", "v1");
            e.IpAddress = "1.2.3.4";
            e.DistinctUserId = "abc123";
            e.Options.Verbose = true;

            new TestSyncApiCaller().Send(e);
        }
        [Test]
        public void TestSend_PersonSetEvent_allValueTypes()
        {
            var e = new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949").CreatePersonSetEvent();

            //else if (value is Int32)
            //else if (value is bool)
            //else if (value is DateTime)
            //else if (value is byte)
            //else if (value is Guid)
            //else if (value is Int16)
            //else if (value is Int64)
            //else if (value is DateOnlyWrapper)
            //else if (value is char)
            //else if (value is Double)
            //else if (value is float)
            //else if (value is sbyte)
            //else if (value is uint)
            //else if (value is ulong)
            //else if (value is ushort)
            //else if (value is Enum)
            e.Properties.Add("p1", "v1");
            e.Properties.Add("p2", (Int32)1);
            e.Properties.Add("p3", true);
            e.Properties.Add("p4", new DateTime(2013, 12, 24));
            e.Properties.Add("p5", (Int16)1);
            e.Properties.Add("p6", (Int64)1);
            e.Properties.Add("p7", new DateOnlyWrapper() { Date = new DateTime(2013, 12, 24) });
            e.Properties.Add("p8", 'a');
            e.Properties.Add("p9", (double)543.1);
            e.Properties.Add("p10", (float)12.53);
            e.Properties.Add("p11", (decimal)12.53);
            e.Properties.Add("p12", (sbyte)1);
            e.Properties.Add("p13", (uint)1);
            e.Properties.Add("p14", (ulong)1);
            e.Properties.Add("p15", EventJsonSerializerTests.TestEnum.Value1);
            e.IpAddress = "1.2.3.4";
            e.DistinctUserId = "abc123";
            e.Options.Verbose = true;

            new TestSyncApiCaller().Send(e);
        }
        [Test]
        public void TestSend_PersonTransationEvent()
        {
            var e = new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949").CreatePersonTransactionEvent();
            e.TransactionAmount = (decimal)-362.25;
            e.TransactionDateTime = new DateTime(2013, 12, 24);
            e.IpAddress = "1.2.3.4";
            e.DistinctUserId = "abc123";
            e.Options.Verbose = true;

            new TestSyncApiCaller().Send(e);

        }
        [Test]
        public void TestSend_PersonIncrementEvent()
        {
            var e = new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949").CreatePersonIncrementEvent();
            e.Increments.Add("i1", (decimal)12);
            e.Increments.Add("i2", (decimal)24.2);
            e.IpAddress = "1.2.3.4";
            e.DistinctUserId = "abc123";
            e.Options.Verbose = true;

            new TestSyncApiCaller().Send(e);
        }
    }
    public class TestSyncApiCaller :ApiCallerBase
    {
        public override void Send(PersonEventBase personEvent)
        {
            HttpWebRequest request = GetRequest(personEvent);
            IAsyncResult result = request.BeginGetResponse(null, null);

            result.AsyncWaitHandle.WaitOne();
            HandleResponse(request, result);
        }

        public override void Send(Event eVent)
        {         
            HttpWebRequest request = GetRequest(eVent);
            IAsyncResult result = request.BeginGetResponse(null, null);

            result.AsyncWaitHandle.WaitOne();
            HandleResponse(request, result);
        }
    }
}
