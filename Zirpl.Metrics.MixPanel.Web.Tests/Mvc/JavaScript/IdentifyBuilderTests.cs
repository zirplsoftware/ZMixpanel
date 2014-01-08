using System;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.Metrics.MixPanel.Web.Mvc.JavaScript;

namespace Zirpl.Metrics.MixPanel.Web.Tests.Mvc.JavaScript
{
    [TestFixture]
    public class IdentifyBuilderTests
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToHtmlString_Nothing()
        {
            new IdentifyBuilder().ToHtmlString();
        }

        [Test]
        public void ToHtmlString_Identify()
        {
            new IdentifyBuilder().DistinctId("me@gmail.com").ToHtmlString().Should().Be("\r\nmixpanel.identify(\"me@gmail.com\");");
        }
        [Test]
        public void ToHtmlString_Instance()
        {
            new IdentifyBuilder("help").DistinctId("me@gmail.com").ToHtmlString().Should().Be("\r\nmixpanel.help.identify(\"me@gmail.com\");");
        }
    }
}
