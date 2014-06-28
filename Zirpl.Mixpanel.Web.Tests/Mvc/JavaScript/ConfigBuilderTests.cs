using FluentAssertions;
using NUnit.Framework;
using Zirpl.Mixpanel.Web.Mvc.JavaScript;

namespace Zirpl.Metrics.MixPanel.Web.Tests.Mvc.JavaScript
{
    [TestFixture]
    public class ConfigBuilderTests
    {
        [Test]
        public void ToHtmlString_NotDirty()
        {
            var html = new ConfigBuilder(null).ToPartialJavaScriptString();
            html.Should().BeNull();
        }

        [Test]
        public void ToHtmlString_IgnoresNulls()
        {
            new ConfigBuilder(null).TrackLinksTimeoutInMilliseconds(1)
                .ToPartialJavaScriptString()
                .Should()
                .Be("{\r\n  \"track_links_timeout\": 1\r\n}");
        }
    }
}
