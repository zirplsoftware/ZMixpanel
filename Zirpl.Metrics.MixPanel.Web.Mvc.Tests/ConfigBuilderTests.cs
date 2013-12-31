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
    public class ConfigBuilderTests
    {
        [Test]
        public void ToHtmlString_NotDirty()
        {
            var html = new Zirpl.Metrics.MixPanel.Web.Mvc.ConfigBuilder(null).ToPartialHtmlString();
            html.Should().BeNull();
        }

        [Test]
        public void ToHtmlString_IgnoresNulls()
        {
            new Zirpl.Metrics.MixPanel.Web.Mvc.ConfigBuilder(null).TrackLinksTimeoutInMilliseconds(1)
                .ToPartialHtmlString()
                .Should()
                .Be("{\r\n  \"track_links_timeout\": 1\r\n}");
        }
    }
}
