using System;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.Metrics.MixPanel.Web.Mvc;

namespace Zirpl.Metrics.MixPanel.Web.Tests.Mvc
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
            new IdentifyBuilder().DistinctId("me@gmail.com").ToHtmlString().Should().Be("mixpanel.identify(\"me@gmail.com\");");
        }
        [Test]
        public void ToHtmlString_Instance()
        {
            new IdentifyBuilder("help").DistinctId("me@gmail.com").ToHtmlString().Should().Be("mixpanel.help.identify(\"me@gmail.com\");");
        }
    }
}
