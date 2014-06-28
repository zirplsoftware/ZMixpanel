using FluentAssertions;
using NUnit.Framework;
using Zirpl.Mixpanel.Web.Mvc.JavaScript;

namespace Zirpl.Metrics.MixPanel.Web.Tests.Mvc.JavaScript
{
    [TestFixture]
    public class InstallBuilderTests
    {
        [Test]
        public void ToHtmlString()
        {
            new InstallBuilder().ToHtmlString().Should().NotBeEmpty();
        }
    }
}
