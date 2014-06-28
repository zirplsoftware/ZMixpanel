using System;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.Mixpanel.Web.Mvc.JavaScript;

namespace Zirpl.Metrics.MixPanel.Web.Tests.Mvc.JavaScript
{
    [TestFixture]
    public class NameTagBuilderTests
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToHtmlString_Nothing()
        {
            new NameTagBuilder().ToHtmlString();
        }

        [Test]
        public void ToHtmlString_NameTag()
        {
            new NameTagBuilder().NameTag("me@gmail.com").ToHtmlString().Should().Be("\r\nmixpanel.name_tag(\"me@gmail.com\");");
        }
        [Test]
        public void ToHtmlString_Instance()
        {
            new NameTagBuilder("help").NameTag("me@gmail.com").ToHtmlString().Should().Be("\r\nmixpanel.help.name_tag(\"me@gmail.com\");");
        }
    }
}
