using System;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.Core;
using Zirpl.Metrics.MixPanel.HttpApi;
using Zirpl.Metrics.MixPanel.HttpApi.Events;

namespace Zirpl.Metrics.MixPanel.Portable.Tests
{
    [TestFixture]
    public class EventJsonSerializerTests
    {
        [Test]
        public void TestGetJson_Event()
        {
            const String JSON = "{\"event\":\"Test Event\",\"properties\":{\"distinct_id\":\"zirplsoftware@gmail.com\",\"ip\":\"1.2.3.4\",\"p1\":\"v1\",\"time\":\"1970-01-01T12:02:55\",\"token\":\"bc8c0e9a58aa7a290880a2e381281949\"}}";

            Event e = new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949").CreateEvent();
            e.Properties.Add("p1", "v1");
            e.EventName = ("Test Event");
            e.IpAddress =("1.2.3.4");
            e.Options.TestMode=true;
            e.EventTime =(JsonUtils.EpochBaseDateTime.AddSeconds(175));
            e.DistinctUserId = ("zirplsoftware@gmail.com");

            JsonSerializer jsonSerializer = new JsonSerializer();
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
            const String JSON = "{\"$token\":\"bc8c0e9a58aa7a290880a2e381281949\",\"$distinct_id\":\"abc123\",\"$ip\":\"1.2.3.4\",\"$ignore_time\":true,\"$set\":{\"$created\":\"2013-12-24T12:00:00\",\"$email\":\"zirplsoftware@gmail.com\",\"$first_name\":\"Nathan\",\"$last_name\":\"LaFratta\",\"$name\":\"someone\",\"$phone\":\"freebirdnal\",\"p1\":\"v1\"}}";

            var e = new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949").CreatePersonSetEvent();
            e.Properties.Add("p1", "v1");
            e.Created = new DateTime(2013, 12, 24);
            e.Email = "zirplsoftware@gmail.com";
            e.FirstName = "Nathan";
            e.LastName = "LaFratta";
            e.IgnoreTime = true;
            e.Name = "someone";
            e.Phone = "freebirdnal";
            e.IpAddress = "1.2.3.4";
            e.DistinctUserId = "abc123";

            JsonSerializer jsonSerializer = new JsonSerializer();
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
            const String JSON = "{\"$token\":\"bc8c0e9a58aa7a290880a2e381281949\",\"$distinct_id\":\"abc123\",\"$ip\":\"1.2.3.4\",\"$ignore_time\":true,\"$set\":{\"$created\":null,\"$email\":null,\"$first_name\":null,\"$last_name\":null,\"$name\":null,\"$phone\":null,\"p1\":null}}";

            var e = new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949").CreatePersonSetEvent();
            e.Properties.Add<Object>("p1", null);
            e.Created = null;
            e.Email = null;
            e.FirstName = null;
            e.LastName = null;
            e.IgnoreTime = true;
            e.Name = null;
            e.Phone = null;
            e.IpAddress = "1.2.3.4";
            e.DistinctUserId = "abc123";

            JsonSerializer jsonSerializer = new JsonSerializer();
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
            const String JSON = "{\"$token\":\"bc8c0e9a58aa7a290880a2e381281949\",\"$distinct_id\":\"abc123\",\"$ip\":\"1.2.3.4\",\"$set\":{}}";

            var e = new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949").CreatePersonSetEvent();
            e.IpAddress = "1.2.3.4";
            e.DistinctUserId = "abc123";

            JsonSerializer jsonSerializer = new JsonSerializer();
            String result = jsonSerializer.GetJson(e);
            result.Should().Be(JSON);
        }
        [Test]
        public void TestGetJson_PersonSetOnceEvent()
        {
            const String JSON = "{\"$set_once\":{\"p1\":\"v1\"}," +
            "\"$token\":\"bc8c0e9a58aa7a290880a2e381281949\",\"$distinct_id\":\"abc123\",\"$ip\":\"1.2.3.4\"}";

            var e = new MixPanelApi("bc8c0e9a58aa7a290880a2e381281949").CreatePersonSetOnceEvent();
            e.Properties.Add("p1", "v1");
            e.IpAddress = "1.2.3.4";
            e.DistinctUserId = "abc123";

            JsonSerializer jsonSerializer = new JsonSerializer();
            String result = jsonSerializer.GetJson(e);
            result.Should().Be(JSON);
        }
        [Test]
        public void TestGetJson_PersonSetOnceEvent_AllDataTypes()
        {
            const String JSON = "{\"$set_once\":{" +
                "\"p1\":\"v1\"," +
                "\"p10\":12.53," +
                "\"p11\":12.53," +
                "\"p12\":1," +
                "\"p13\":1," +
                "\"p14\":1," +
                "\"p15\":0," +
                "\"p2\":1," +
                "\"p3\":true," +
                "\"p4\":\"2013-12-24T12:00:00\"," +
                "\"p5\":1," +
                "\"p6\":1," +
                "\"p7\":\"2013-12-24\"," +
                "\"p8\":\"a\"," +
                "\"p9\":543.1" +
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
            e.Properties.Add("p15", TestEnum.Value1);
            e.IpAddress = "1.2.3.4";
            e.DistinctUserId = "abc123";

            JsonSerializer jsonSerializer = new JsonSerializer();
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
            e.Increments.Add("p1", (decimal)12);
            e.Increments.Add("p2", (decimal)24.2);
            e.IpAddress = "1.2.3.4";
            e.DistinctUserId = "abc123";

            JsonSerializer jsonSerializer = new JsonSerializer();
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
            e.DistinctUserId = "abc123";

            JsonSerializer jsonSerializer = new JsonSerializer();
            String result = jsonSerializer.GetJson(e);
            result.Should().Be(JSON);
        }
    }
}
