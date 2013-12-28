using System;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.Core;

namespace Zirpl.Metrics.MixPanel.Portable.Tests
{
    [TestFixture]
    public class EventJsonSerializerTests
    {
        [Test]
        public void TestGetJson_Event()
        {
            const String JSON = "{\"event\":\"Test Event\",\"properties\":{\"ip\":\"1.2.3.4\",\"token\":\"bc8c0e9a58aa7a290880a2e381281949\",\"time\":175,\"distinct_id\":\"zirplsoftware@gmail.com\",\"p1\":\"v1\"}}";

            Event e = new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949").CreateEvent();
            e.AddProperty("p1", "v1");
            e.EventName = "Test Event";
            e.IpAddress = "1.2.3.4";
            e.TestMode = true;
            e.EventTime = EventJsonSerializer.EpochBaseDateTime.AddSeconds(175);
            e.DistinctUserId = "zirplsoftware@gmail.com";

            EventJsonSerializer jsonSerializer = new EventJsonSerializer();
            String result = jsonSerializer.GetJson(e);
            result.Should().Be(JSON);
        }
        [Test]
        public void TestGetJson_PersonSetEvent()
        {   
            //{
            //    "$set": {
            //        "$first_name": "John",
            //        "$last_name": "Smith"
            //    },
            //    "$token": "36ada5b10da39a1347559321baf13063",
            //    "$distinct_id": "13793",
            //    "$ip": "123.123.123.123"
            //}
            const String JSON = "{\"$set\":{\"$first_name\":\"Nathan\",\"$last_name\":\"LaFratta\",\"$name\":\"someone\",\"$email\":\"zirplsoftware@gmail.com\",\"$username\":\"freebirdnal\",\"$created\":\"2013-12-24T12:00:00\",\"$ignore_time\":true,\"p1\":\"v1\"}," +
            "\"$token\":\"bc8c0e9a58aa7a290880a2e381281949\",\"$distinct_id\":\"abc123\",\"$ip\":\"1.2.3.4\"}";

            var e = new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949").CreatePersonSetEvent();
            e.AddProperty("p1", "v1");
            e.Created = new DateTime(2013, 12, 24);
            e.Email = "zirplsoftware@gmail.com";
            e.FirstName = "Nathan";
            e.LastName = "LaFratta";
            e.IgnoreTime = true;
            e.Name = "someone";
            e.Username = "freebirdnal";
            e.IpAddress = "1.2.3.4";
            e.DistinctId = "abc123";

            EventJsonSerializer jsonSerializer = new EventJsonSerializer();
            String result = jsonSerializer.GetJson(e);
            result.Should().Be(JSON);
        }
        [Test]
        public void TestGetJson_PersonSetEvent_ResetFields()
        {
            //{
            //    "$set": {
            //        "$first_name": "John",
            //        "$last_name": "Smith"
            //    },
            //    "$token": "36ada5b10da39a1347559321baf13063",
            //    "$distinct_id": "13793",
            //    "$ip": "123.123.123.123"
            //}
            const String JSON = "{\"$set\":{\"$first_name\":null,\"$last_name\":null,\"$name\":null,\"$email\":null,\"$username\":null,\"$created\":null,\"$ignore_time\":true,\"p1\":null}," +
            "\"$token\":\"bc8c0e9a58aa7a290880a2e381281949\",\"$distinct_id\":\"abc123\",\"$ip\":\"1.2.3.4\"}";

            var e = new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949").CreatePersonSetEvent();
            e.AddProperty<Object>("p1", null);
            e.Created = null;
            e.Email = null;
            e.FirstName = null;
            e.LastName = null;
            e.IgnoreTime = true;
            e.Name = null;
            e.Username = null;
            e.IpAddress = "1.2.3.4";
            e.DistinctId = "abc123";

            EventJsonSerializer jsonSerializer = new EventJsonSerializer();
            String result = jsonSerializer.GetJson(e);
            result.Should().Be(JSON);
        }
        [Test]
        public void TestGetJson_PersonSetEvent_NullFields()
        {
            //{
            //    "$set": {
            //        "$first_name": "John",
            //        "$last_name": "Smith"
            //    },
            //    "$token": "36ada5b10da39a1347559321baf13063",
            //    "$distinct_id": "13793",
            //    "$ip": "123.123.123.123"
            //}
            const String JSON = "{\"$set\":{}," +
            "\"$token\":\"bc8c0e9a58aa7a290880a2e381281949\",\"$distinct_id\":\"abc123\",\"$ip\":\"1.2.3.4\"}";

            var e = new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949").CreatePersonSetEvent();
            e.IpAddress = "1.2.3.4";
            e.DistinctId = "abc123";

            EventJsonSerializer jsonSerializer = new EventJsonSerializer();
            String result = jsonSerializer.GetJson(e);
            result.Should().Be(JSON);
        }
        [Test]
        public void TestGetJson_PersonSetOnceEvent()
        {
            const String JSON = "{\"$set_once\":{\"p1\":\"v1\"}," +
            "\"$token\":\"bc8c0e9a58aa7a290880a2e381281949\",\"$distinct_id\":\"abc123\",\"$ip\":\"1.2.3.4\"}";

            var e = new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949").CreatePersonSetOnceEvent();
            e.AddProperty("p1", "v1");
            e.IpAddress = "1.2.3.4";
            e.DistinctId = "abc123";

            EventJsonSerializer jsonSerializer = new EventJsonSerializer();
            String result = jsonSerializer.GetJson(e);
            result.Should().Be(JSON);
        }
        [Test]
        public void TestGetJson_PersonSetOnceEvent_AllDataTypes()
        {
            const String JSON = "{\"$set_once\":{" +
                "\"p1\":\"v1\"," +
                "\"p2\":1," +
                "\"p3\":true," +
                "\"p4\":\"2013-12-24T12:00:00\"," +
                "\"p5\":1," +
                "\"p6\":1," +
                "\"p7\":\"2013-12-24\"," +
                "\"p8\":\"a\"," +
                "\"p9\":543.1," +
                "\"p10\":12.53," +
                "\"p11\":12.53," +
                "\"p12\":1," +
                "\"p13\":1," +
                "\"p14\":1," +
                "\"p15\":0" +
                "}," +
            "\"$token\":\"bc8c0e9a58aa7a290880a2e381281949\",\"$distinct_id\":\"abc123\",\"$ip\":\"1.2.3.4\"}";

            var e = new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949").CreatePersonSetOnceEvent();

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
            e.AddProperty("p7", new DateOnlyWrapper(){Date = new DateTime(2013, 12, 24)});
            e.AddProperty("p8", 'a');
            e.AddProperty("p9", (double)543.1);
            e.AddProperty("p10", (float)12.53);
            e.AddProperty("p11", (decimal)12.53);
            e.AddProperty("p12", (sbyte)1);
            e.AddProperty("p13", (uint)1);
            e.AddProperty("p14", (ulong)1);
            e.AddProperty("p15", TestEnum.Value1);
            e.IpAddress = "1.2.3.4";
            e.DistinctId = "abc123";

            EventJsonSerializer jsonSerializer = new EventJsonSerializer();
            String result = jsonSerializer.GetJson(e);
            result.Should().Be(JSON);
        }

        public enum TestEnum
        {
            Value1
        }
        [Test]
        public void TestGetJson_PersonIncrementEvent()
        {
            const String JSON = "{\"$add\":{\"p1\":12,\"p2\":24.2}," +
            "\"$token\":\"bc8c0e9a58aa7a290880a2e381281949\",\"$distinct_id\":\"abc123\",\"$ip\":\"1.2.3.4\"}";

            var e = new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949").CreatePersonIncrementEvent();
            e.AddIncrement("p1", 12);
            e.AddIncrement("p2", (decimal)24.2);
            e.IpAddress = "1.2.3.4";
            e.DistinctId = "abc123";

            EventJsonSerializer jsonSerializer = new EventJsonSerializer();
            String result = jsonSerializer.GetJson(e);
            result.Should().Be(JSON);
        }
        [Test]
        public void TestGetJson_PersonTransactionEvent()
        {
            const String JSON = "{\"$append\":{\"$transactions\":{\"$time\":\"2013-12-24T12:00:00\",\"$amount\":-362.25}}," +
            "\"$token\":\"bc8c0e9a58aa7a290880a2e381281949\",\"$distinct_id\":\"abc123\",\"$ip\":\"1.2.3.4\"}";

            var e = new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949").CreatePersonTransactionEvent();
            e.TransactionAmount = (decimal) -362.25;
            e.TransactionDateTime = new DateTime(2013, 12, 24);
            e.IpAddress = "1.2.3.4";
            e.DistinctId = "abc123";

            EventJsonSerializer jsonSerializer = new EventJsonSerializer();
            String result = jsonSerializer.GetJson(e);
            result.Should().Be(JSON);
        }
    }
}
