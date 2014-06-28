using FluentAssertions;
using NUnit.Framework;
using Zirpl.Mixpanel.Web.Mvc.JavaScript;

namespace Zirpl.Metrics.MixPanel.Web.Tests.Mvc.JavaScript
{
    [TestFixture]
    public class TrackPageViewBuilderTests
    {
        [Test]
        public void ToHtmlString_Nothing()
        {
            new TrackPageViewBuilder().ToHtmlString().Should().Be("\r\nmixpanel.track_pageview();");
        }

        [Test]
        public void ToHtmlString_NameTag()
        {
            new TrackPageViewBuilder().Page("contactus").ToHtmlString().Should().Be("\r\nmixpanel.track_pageview(\"contactus\");");
        }
        [Test]
        public void ToHtmlString_Instance()
        {
            new TrackPageViewBuilder("help").ToHtmlString().Should().Be("\r\nmixpanel.help.track_pageview();");
        }
    }
}
