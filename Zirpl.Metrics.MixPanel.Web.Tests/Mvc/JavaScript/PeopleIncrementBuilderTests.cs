using System;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.Metrics.MixPanel.Web.Mvc.JavaScript;

namespace Zirpl.Metrics.MixPanel.Web.Tests.Mvc.JavaScript
{
    [TestFixture]
    public class PeopleIncrememntBuilderTests
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToHtmlString_Nothing()
        {
            new PeopleIncrementBuilder().ToHtmlString();
        }

        [Test]
        public void ToHtmlString_Instance()
        {
            new PeopleIncrementBuilder("help").Increments().Add("listname", -2).ToHtmlString().Should().Be("\r\nmixpanel.help.people.increment({\r\n  \"listname\": -2\r\n});");
        }

        [Test]
        public void ToHtmlString_ListValues()
        {
            new PeopleIncrementBuilder().Increments().Add("listname", -2).ToHtmlString().Should().Be("\r\nmixpanel.people.increment({\r\n  \"listname\": -2\r\n});");
        }

        [Test]
        public void ToHtmlString_PropertiesAndCallback()
        {
            new PeopleIncrementBuilder().Callback("callback").Increments().Add("listname", -2).ToHtmlString().Should().Be("\r\nmixpanel.people.increment({\r\n  \"listname\": -2\r\n}, callback);");
        }
    }
}
