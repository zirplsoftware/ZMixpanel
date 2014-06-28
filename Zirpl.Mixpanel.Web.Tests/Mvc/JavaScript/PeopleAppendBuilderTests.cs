using System;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.Mixpanel.Web.Mvc.JavaScript;

namespace Zirpl.Metrics.MixPanel.Web.Tests.Mvc.JavaScript
{
    [TestFixture]
    public class PeopleAppendBuilderTests
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToHtmlString_Nothing()
        {
            new PeopleAppendBuilder().ToHtmlString();
        }

        [Test]
        public void ToHtmlString_Instance()
        {
            new PeopleAppendBuilder("help").ListValues().Add("listname", "value").ToHtmlString().Should().Be("\r\nmixpanel.help.people.append({\r\n  \"listname\": \"value\"\r\n});");
        }

        [Test]
        public void ToHtmlString_ListValues()
        {
            new PeopleAppendBuilder().ListValues().Add("listname", "value").ToHtmlString().Should().Be("\r\nmixpanel.people.append({\r\n  \"listname\": \"value\"\r\n});");
        }

        [Test]
        public void ToHtmlString_PropertiesAndCallback()
        {
            new PeopleAppendBuilder().Callback("callback").ListValues().Add("listname", "value").ToHtmlString().Should().Be("\r\nmixpanel.people.append({\r\n  \"listname\": \"value\"\r\n}, callback);");
        }
    }
}
