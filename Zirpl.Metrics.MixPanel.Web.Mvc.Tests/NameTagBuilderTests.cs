using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;

namespace Zirpl.Metrics.MixPanel.Web.Mvc.Tests
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
            new NameTagBuilder().NameTag("me@gmail.com").ToHtmlString().Should().Be("mixpanel.name_tag(\"me@gmail.com\");");
        }
        [Test]
        public void ToHtmlString_Instance()
        {
            new NameTagBuilder("help").NameTag("me@gmail.com").ToHtmlString().Should().Be("mixpanel.help.name_tag(\"me@gmail.com\");");
        }
    }
}
