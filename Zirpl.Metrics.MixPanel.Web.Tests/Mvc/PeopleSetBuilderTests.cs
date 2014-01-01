using System;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.Metrics.MixPanel.Web.Mvc;

namespace Zirpl.Metrics.MixPanel.Web.Tests.Mvc
{
    [TestFixture]
    public class PeopleSetBuilderTests
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToHtmlString_Nothing()
        {
            new PeopleSetBuilder().ToHtmlString();
        }

        [Test]
        public void ToHtmlString_Instance()
        {
            new PeopleSetBuilder("help").Properties().Email("my@email.com").ToHtmlString().Should().Be("mixpanel.help.people.set({\r\n  \"$email\": \"my@email.com\"\r\n});");
        }

        [Test]
        public void ToHtmlString_Properties()
        {
            new PeopleSetBuilder().Properties().Email("my@email.com").Add("key1", "value1").ToHtmlString().Should().Be("mixpanel.people.set({\r\n  \"$email\": \"my@email.com\",\r\n  \"key1\": \"value1\"\r\n});");
        }

        [Test]
        public void ToHtmlString_PropertiesAndCallback()
        {
            new PeopleSetBuilder().Callback("callback").Properties().Email("my@email.com").Add("key1", "value1").ToHtmlString().Should().Be("mixpanel.people.set({\r\n  \"$email\": \"my@email.com\",\r\n  \"key1\": \"value1\"\r\n}, callback);");
        }
    }
}
