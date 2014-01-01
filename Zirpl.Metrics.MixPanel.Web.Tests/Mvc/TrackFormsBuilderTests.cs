﻿using System;
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
            new TrackFormsBuilder().EventName("test").DomSelector("#name").ToHtmlString().Should().Be("mixpanel.track_forms(\"#name\", \"test\");");
        }

        [Test]
        public void ToHtmlString_InstanceAndNoProperties()
        {
            new TrackFormsBuilder("help").EventName("test").DomSelector("#name").ToHtmlString().Should().Be("mixpanel.help.track_forms(\"#name\", \"test\");");
        }

        [Test]
        public void ToHtmlString_Properties()
        {
            new TrackFormsBuilder().EventName("test").DomSelector("#name").Properties().Add("key1", "value1").ToHtmlString().Should().Be("mixpanel.track_forms(\"#name\", \"test\", {\r\n  \"key1\": \"value1\"\r\n});");
        }

        [Test]
        public void ToHtmlString_PropertiesFunction()
        {
            new TrackFormsBuilder().EventName("test").DomSelector("#name").PropertiesCreationFunction("function").ToHtmlString().Should().Be("mixpanel.track_forms(\"#name\", \"test\", function);");
        }
    }
}
