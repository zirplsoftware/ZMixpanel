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
    public class SetConfigBuilderTests
    {
        [Test]
        public void ToHtmlString_Nothing()
        {
            new SetConfigBuilder().ToHtmlString().Should().Be("mixpanel.set_config();");
        }

        [Test]
        public void ToHtmlString_Instance()
        {
            new SetConfigBuilder("help").ToHtmlString().Should().Be("mixpanel.help.set_config();");
        }

        [Test]
        public void ToHtmlString_Config()
        {
            new SetConfigBuilder().Config().TrackLinksTimeoutInMilliseconds(1).ToHtmlString().Should().Be("mixpanel.set_config({\r\n  \"track_links_timeout\": 1\r\n});");
        }

        [Test]
        public void ToHtmlString_LibraryAndConfig()
        {
            new SetConfigBuilder("help").Config().TrackLinksTimeoutInMilliseconds(1).ToHtmlString().Should().Be("mixpanel.help.set_config({\r\n  \"track_links_timeout\": 1\r\n});");
        }
    }
}
