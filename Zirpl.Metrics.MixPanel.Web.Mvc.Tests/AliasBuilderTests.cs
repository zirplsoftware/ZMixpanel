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
    public class AliasBuilderTests
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToHtmlString_Nothing()
        {
            new AliasBuilder().ToHtmlString();
        }

        [Test]
        public void ToHtmlString_Alias()
        {
            new AliasBuilder().Alias("me@gmail.com").ToHtmlString().Should().Be("mixpanel.alias(\"me@gmail.com\");");
        }

        [Test]
        public void ToHtmlString_AliasAndOriginal()
        {
            new AliasBuilder().Alias("me@gmail.com").OriginalId("123").ToHtmlString().Should().Be("mixpanel.alias(\"me@gmail.com\", \"123\");");
        }

        [Test]
        public void ToHtmlString_InstanceNameAndAlias()
        {
            new AliasBuilder("help").Alias("me@gmail.com").ToHtmlString().Should().Be("mixpanel.help.alias(\"me@gmail.com\");");
        }
    }
}
