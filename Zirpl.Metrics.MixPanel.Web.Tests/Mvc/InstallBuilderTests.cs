using System;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.Metrics.MixPanel.Web.Mvc;

namespace Zirpl.Metrics.MixPanel.Web.Tests.Mvc
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
