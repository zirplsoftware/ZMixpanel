using System;
using System.Collections.Generic;
using Zirpl.Core;

namespace Zirpl.Metrics.MixPanel
{
    public class PersonSetOnceEvent : PersonEventBase
    {
        private readonly Dictionary<String, Object> additionalProperties;

        protected internal PersonSetOnceEvent()
        {
            this.additionalProperties = new Dictionary<string, object>();
        }

        

        public void AddProperty<T>(String propertyName, T value)
        {
            if (this.additionalProperties.ContainsKey(propertyName))
            {
                this.additionalProperties[propertyName] = value;
            }
            else
            {
                this.additionalProperties.Add(propertyName, value);
            }
        }
        public void AddProperty(String propertyName, DateOnlyWrapper value)
        {
            if (this.additionalProperties.ContainsKey(propertyName))
            {
                this.additionalProperties[propertyName] = value;
            }
            else
            {
                this.additionalProperties.Add(propertyName, value);
            }
        }

        public Dictionary<string, object> GetProperties()
        {
            return new Dictionary<string, object>(this.additionalProperties);
        }

        public void ClearProperties()
        {
            this.additionalProperties.Clear();
        }

        public void RemoveProperty(String name)
        {
            if (this.additionalProperties.ContainsKey(name))
            {
                this.additionalProperties.Remove(name);
            }
        }
    }
}
