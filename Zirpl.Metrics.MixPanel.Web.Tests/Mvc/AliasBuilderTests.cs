using System;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.Metrics.MixPanel.Web.Mvc;

namespace Zirpl.Metrics.MixPanel.Web.Tests.Mvc
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
            new AliasBuilder().Alias("me@gmail.com").ToHtmlString().Should().Be("\r\nmixpanel.alias(\"me@gmail.com\");");
        }

        [Test]
        public void ToHtmlString_AliasAndOriginal()
        {
            new AliasBuilder().Alias("me@gmail.com").OriginalId("123").ToHtmlString().Should().Be("\r\nmixpanel.alias(\"me@gmail.com\", \"123\");");
        }

        [Test]
        public void ToHtmlString_InstanceNameAndAlias()
        {
            new AliasBuilder("help").Alias("me@gmail.com").ToHtmlString().Should().Be("\r\nmixpanel.help.alias(\"me@gmail.com\");");
        }
    }
}
