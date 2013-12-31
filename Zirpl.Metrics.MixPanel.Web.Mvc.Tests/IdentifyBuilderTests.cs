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
