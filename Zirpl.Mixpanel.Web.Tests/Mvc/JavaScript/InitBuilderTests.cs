using System;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.Mixpanel.Web.Mvc.JavaScript;

namespace Zirpl.Metrics.MixPanel.Web.Tests.Mvc.JavaScript
{
    [TestFixture]
    public class InitBuilderTests
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToHtmlString_NoToken()
        {
            new InitBuilder().ToHtmlString();
        }

        [Test]
        public void ToHtmlString_TokenOnly()
        {
            new InitBuilder().Token("123").ToHtmlString().Should().Be("\r\nmixpanel.init(\"123\");");
        }

        [Test]
        public void ToHtmlString_Library()
        {
            new InitBuilder().Token("123").InstanceName("help").ToHtmlString().Should().Be("\r\nmixpanel.init(\"123\", null, \"help\");");
        }

        [Test]
        public void ToHtmlString_Config()
        {
            new InitBuilder().Token("123").Config().TrackLinksTimeoutInMilliseconds(1).ToHtmlString().Should().Be("\r\nmixpanel.init(\"123\", {\r\n  \"track_links_timeout\": 1\r\n});");
        }

        [Test]
        public void ToHtmlString_ConfigAndLibrary()
        {
            new InitBuilder().Token("123").InstanceName("help").Config().TrackLinksTimeoutInMilliseconds(1).ToHtmlString().Should().Be("\r\nmixpanel.init(\"123\", {\r\n  \"track_links_timeout\": 1\r\n}, \"help\");");
        }
    }
}
