using System;
using System.Net;
using NUnit.Framework;
using Zirpl.Core;

namespace Zirpl.Metrics.MixPanel.Portable.Tests
{
    [TestFixture]
    public class EventSenderTests
    {
        [Test]
        public void TestSend_Event()
        {
            Event e = new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949").CreateEvent();
            e.AddProperty("p1", "v1");
            e.EventName = "Test Event";
            e.IpAddress = "1.2.3.4";
            e.TestMode = true;
            e.EventTime = DateTime.Now;
            e.DistinctUserId = "zirplsoftware@gmail.com";

            new TestSyncEventSender().Send(e);
        }
        [Test]
        public void TestSend_PersonSetEvent()
        {
            var e = new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949").CreatePersonSetEvent();
            e.AddProperty("p1", "v1");
            e.Created = DateTime.Today;
            e.Email = "zirplsoftware@gmail.com";
            e.FirstName = "Nathan";
            e.LastName = "LaFratta";
            e.IgnoreTime = true;
            e.Name = "someone";
            e.Username = "freebirdnal";
            e.IpAddress = "1.2.3.4";
            e.DistinctId = "abc123";
            e.Verbose = true;

            new TestSyncEventSender().Send(e);
        }
        [Test]
        public void TestSend_PersonSetOnceEvent()
        {
            var e = new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949").CreatePersonSetOnceEvent();
            e.AddProperty("p1", "v1");
            e.IpAddress = "1.2.3.4";
            e.DistinctId = "abc123";
            e.Verbose = true;

            new TestSyncEventSender().Send(e);
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
            e.AddProperty("p1", "v1");
            e.AddProperty("p2", (Int32)1);
            e.AddProperty("p3", true);
            e.AddProperty("p4", new DateTime(2013, 12, 24));
            e.AddProperty("p5", (Int16)1);
            e.AddProperty("p6", (Int64)1);
            e.AddProperty("p7", new DateOnlyWrapper() { Date = new DateTime(2013, 12, 24) });
            e.AddProperty("p8", 'a');
            e.AddProperty("p9", (double)543.1);
            e.AddProperty("p10", (float)12.53);
            e.AddProperty("p11", (decimal)12.53);
            e.AddProperty("p12", (sbyte)1);
            e.AddProperty("p13", (uint)1);
            e.AddProperty("p14", (ulong)1);
            e.AddProperty("p15", EventJsonSerializerTests.TestEnum.Value1);
            e.IpAddress = "1.2.3.4";
            e.DistinctId = "abc123";
            e.Verbose = true;

            new TestSyncEventSender().Send(e);
        }
        [Test]
        public void TestSend_PersonTransationEvent()
        {
            var e = new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949").CreatePersonTransactionEvent();
            e.TransactionAmount = (decimal)-362.25;
            e.TransactionDateTime = new DateTime(2013, 12, 24);
            e.IpAddress = "1.2.3.4";
            e.DistinctId = "abc123";
            e.Verbose = true;

            new TestSyncEventSender().Send(e);

        }
        [Test]
        public void TestSend_PersonIncrementEvent()
        {
            var e = new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949").CreatePersonIncrementEvent();
            e.AddIncrement("i1", 12);
            e.AddIncrement("i2", (decimal)24.2);
            e.IpAddress = "1.2.3.4";
            e.DistinctId = "abc123";
            e.Verbose = true;

            new TestSyncEventSender().Send(e);
        }
    }
    public class TestSyncEventSender :EventSenderBase
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
