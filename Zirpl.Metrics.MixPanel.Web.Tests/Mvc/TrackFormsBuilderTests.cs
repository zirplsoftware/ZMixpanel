using System;
using FluentAssertions;
using NUnit.Framework;
using Zirpl.Metrics.MixPanel.Web.Mvc;

namespace Zirpl.Metrics.MixPanel.Web.Tests.Mvc
{
    [TestFixture]
    public class TrackFormsBuilderTests
    {
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToHtmlString_NullDomSelector()
        {
            new TrackFormsBuilder().EventName("name").ToHtmlString();
        }
        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ToHtmlString_NullEventName()
        {
            new TrackFormsBuilder().DomSelector("#name").ToHtmlString();
        }

        [Test]
        public void ToHtmlString_NoProperties()
        {
            new TrackFormsBuilder().EventName("test").DomSelector("#name").ToHtmlString().Should().Be("\r\nmixpanel.track_forms(\"#name\", \"test\");");
        }

        [Test]
        public void ToHtmlString_InstanceAndNoProperties()
        {
            new TrackFormsBuilder("help").EventName("test").DomSelector("#name").ToHtmlString().Should().Be("\r\nmixpanel.help.track_forms(\"#name\", \"test\");");
        }

        [Test]
        public void ToHtmlString_Properties()
        {
            new TrackFormsBuilder().EventName("test").DomSelector("#name").Properties().Add("key1", "value1").ToHtmlString().Should().Be("\r\nmixpanel.track_forms(\"#name\", \"test\", {\r\n  \"key1\": \"value1\"\r\n});");
        }

        [Test]
        public void ToHtmlString_PropertiesFunction()
        {
            new TrackFormsBuilder().EventName("test").DomSelector("#name").Properties().CreateFunctionName("function").ToHtmlString().Should().Be("\r\nmixpanel.track_forms(\"#name\", \"test\", function());");
        }

        [Test]
        public void ToHtmlString_PropertiesVariable()
        {
            new TrackFormsBuilder().EventName("test").DomSelector("#name").Properties().VariableName("function").ToHtmlString().Should().Be("\r\nmixpanel.track_forms(\"#name\", \"test\", function);");
        }
    }
}
