using FluentAssertions;
using NUnit.Framework;
using Zirpl.Metrics.MixPanel.Web.Mvc.JavaScript;

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
