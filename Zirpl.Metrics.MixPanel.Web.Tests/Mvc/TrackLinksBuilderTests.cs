using System;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.Metrics.MixPanel.Web.Mvc;

namespace Zirpl.Metrics.MixPanel.Web.Tests.Mvc
{
    [TestFixture]
    public class TrackLinksBuilderTests
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToHtmlString_NullDomSelector()
        {
            new TrackLinksBuilder().EventName("name").ToHtmlString();
        }
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToHtmlString_NullEventName()
        {
            new TrackLinksBuilder().DomSelector("#name").ToHtmlString();
        }

        [Test]
        public void ToHtmlString_NoProperties()
        {
            new TrackLinksBuilder().EventName("test").DomSelector("#name").ToHtmlString().Should().Be("mixpanel.track_links(\"#name\", \"test\");");
        }

        [Test]
        public void ToHtmlString_InstanceAndNoProperties()
        {
            new TrackLinksBuilder("help").EventName("test").DomSelector("#name").ToHtmlString().Should().Be("mixpanel.help.track_links(\"#name\", \"test\");");
        }

        [Test]
        public void ToHtmlString_Properties()
        {
            new TrackLinksBuilder().EventName("test").DomSelector("#name").Properties().Add("key1", "value1").ToHtmlString().Should().Be("mixpanel.track_links(\"#name\", \"test\", {\r\n  \"key1\": \"value1\"\r\n});");
        }

        [Test]
        public void ToHtmlString_PropertiesFunction()
        {
            new TrackLinksBuilder().EventName("test").DomSelector("#name").PropertiesCreationFunction("function").ToHtmlString().Should().Be("mixpanel.track_links(\"#name\", \"test\", function);");
        }
    }
}
