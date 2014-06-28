using System;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.Mixpanel.HttpApi.Events;

namespace Zirpl.Mixpanel.HttpApi.Portable.Tests
{
    [TestFixture]
    public class MixPanelApiTests
    {
        [Test]
        public void TestCreateEvent()
        {
            new MixpanelHttpApiClient("bc8c0e9a58aa7a290880a2e381281949").CreateEvent().Should().NotBeNull();
        }
        //[Test]
        //public void TestCreateEvent_AutopopulatesProjectToken()
        //{
        //    new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949") { ProjectToken = "haha" }.CreateEvent().ProjectToken().Should().Be("haha");
        //}

        [Test]
        public void TestCreateEvent_Generic()
        {
            new MixpanelHttpApiClient("bc8c0e9a58aa7a290880a2e381281949").CreateEvent<TestMixPanelEvent>().Should().NotBeNull();
        }
        //[Test]
        //public void TestCreateEvent_Generic_AutopopulatesProjectToken()
        //{
        //    new MixPanelApi() { ProjectToken = "haha" }.CreateEvent<TestMixPanelEvent>().ProjectToken().Should().Be("haha");
        //}

        [Test]
        public void TestSend()
        {
            Event e = new MixpanelHttpApiClient("bc8c0e9a58aa7a290880a2e381281949") { ProjectToken = "bc8c0e9a58aa7a290880a2e381281949" }.CreateEvent();
            e.Properties.Add("p1", "v1");
            e.EventName = ("Test Event");
            e.IpAddress = ("1.2.3.4");
            e.Options.TestMode = true;
            e.EventTime = (DateTime.Now);
            e.DistinctUserId = ("zirplsoftware@gmail.com");

            new MixpanelHttpApiClient().Send(e);
        }

        private const String JSON = "{\"event\":\"Test Event\",\"properties\":{\"ip\":\"1.2.3.4\",\"token\":\"abcdefghijklmnop\",\"time\":175,\"distinct_id\":\"zirplsoftware@gmail.com\",\"p1\":\"v1\"}}";

        public class TestMixPanelEvent : Event
        {
            
        }

    }
}
