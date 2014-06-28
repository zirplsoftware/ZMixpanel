using System;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.Mixpanel.Web.Mvc.JavaScript;

namespace Zirpl.Metrics.MixPanel.Web.Tests.Mvc.JavaScript
{
    [TestFixture]
    public class TrackBuilderTests
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToHtmlString_Nothing()
        {
            new TrackBuilder().ToHtmlString();
        }

        [Test]
        public void ToHtmlString_Instance()
        {
            new TrackBuilder("help").EventName("test").ToHtmlString().Should().Be("\r\nmixpanel.help.track(\"test\");");
        }

        [Test]
        public void ToHtmlString_Callback()
        {
            new TrackBuilder("help").EventName("test").Callback("callback").ToHtmlString().Should().Be("\r\nmixpanel.help.track(\"test\", null, callback);");
        }

        [Test]
        public void ToHtmlString_Properties()
        {
            new TrackBuilder("help").EventName("test").Properties().Add("key1", "value1").ToHtmlString().Should().Be("\r\nmixpanel.help.track(\"test\", {\r\n  \"key1\": \"value1\"\r\n});");
        }

        [Test]
        public void ToHtmlString_PropertiesAndCallback()
        {
            new TrackBuilder("help").EventName("test").Callback("callback").Properties().Add("key1", "value1").ToHtmlString().Should().Be("\r\nmixpanel.help.track(\"test\", {\r\n  \"key1\": \"value1\"\r\n}, callback);");
        }
    }
}
