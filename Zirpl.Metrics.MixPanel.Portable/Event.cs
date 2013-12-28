using System;
using System.Collections.Generic;
using Zirpl.Core;

namespace Zirpl.Metrics.MixPanel
{
    public class Event
    {
        private readonly Dictionary<String, Object> additionalProperties;

        protected internal Event()
        {
            this.additionalProperties = new Dictionary<string, object>();
        }

        public String EventName { get; set; }
        public DateTime? EventTime { get; set; }
        public String IpAddress { get; set; }
        public String ProjectToken { get; internal set; }
        public String DistinctUserId { get; set; }
        public String UserNameTag { get; set; }
        public Boolean MaskIpAddress { get; set; }
        public Boolean Verbose { get; set; }
        public Boolean TestMode { get; set; }

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
