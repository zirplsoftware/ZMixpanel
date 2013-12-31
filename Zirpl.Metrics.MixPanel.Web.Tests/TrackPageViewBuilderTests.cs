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
    public class TrackPageViewBuilderTests
    {
        [Test]
        public void ToHtmlString_Nothing()
        {
            new TrackPageViewBuilder().ToHtmlString().Should().Be("mixpanel.track_pageview();");
        }

        [Test]
        public void ToHtmlString_NameTag()
        {
            new TrackPageViewBuilder().Page("contactus").ToHtmlString().Should().Be("mixpanel.track_pageview(\"contactus\");");
        }
        [Test]
        public void ToHtmlString_Instance()
        {
            new TrackPageViewBuilder("help").ToHtmlString().Should().Be("mixpanel.help.track_pageview();");
        }
    }
}
