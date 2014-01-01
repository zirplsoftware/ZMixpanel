﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;

namespace Zirpl.Metrics.MixPanel.Web.Mvc
{
    public class TrackFormsBuilder :CallBuilderBase
    {
        private String _eventName;
        private String _domSelector;
        private String _propertiesCreationFunction;
        private readonly PropertiesBuilder<object> _propertiesBuilder; 

        public TrackFormsBuilder()
        {
            this._propertiesBuilder = new PropertiesBuilder<object>(this);
        }

        public TrackFormsBuilder(String instanceName)
            :base(instanceName)
        {
            this._propertiesBuilder = new PropertiesBuilder<object>(this);
        }

        public PropertiesBuilder<Object> Properties()
        {
            return this._propertiesBuilder;
        }

        public TrackFormsBuilder EventName(String value)
        {
            this._eventName = value;
            return this;
        }

        public TrackFormsBuilder DomSelector(String value)
        {
            this._domSelector = value;
            return this;
        }

        public TrackFormsBuilder PropertiesCreationFunction(String value)
        {
            this._propertiesCreationFunction = value;
            return this;
        }

        public override string ToHtmlString()
        {
            if (String.IsNullOrEmpty(this._eventName))
            {
                throw new InvalidOperationException("Cannot call ToHtmlString without EventName set");
            }
            if (String.IsNullOrEmpty(this._domSelector))
            {
                throw new InvalidOperationException("Cannot call ToHtmlString without EventName set");
            }

            var sb = new StringBuilder();
            sb.AppendFormat("{0}.track_forms(\"{1}\", \"{2}\"", base.ToHtmlString(), this._domSelector, this._eventName);
            if (!String.IsNullOrEmpty(this._propertiesCreationFunction))
            {
                sb.AppendFormat(", {0}", this._propertiesCreationFunction);
            }
            else
            {
                var properties = this.Properties().ToPropertyArrayJson(Formatting.Indented);
                if (!String.IsNullOrEmpty(properties))
                {
                    sb.AppendFormat(", {0}", properties);
                }
            }
            sb.Append(");");
            return sb.ToString();
        }
    }
}