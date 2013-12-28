using System;
using System.Collections.Generic;

namespace Zirpl.Metrics.MixPanel
{
    public class PersonIncrementEvent : PersonEventBase
    {
        private readonly Dictionary<String, decimal> additionalProperties;

        protected internal PersonIncrementEvent()
        {
            this.additionalProperties = new Dictionary<string, decimal>();
        }

        

        public void AddIncrement(String propertyName, decimal value)
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


        public Dictionary<string, decimal> GetIncrememnts()
        {
            return new Dictionary<string, decimal>(this.additionalProperties);
        }

        public void ClearGetIncrememnts()
        {
            this.additionalProperties.Clear();
        }

        public void RemoveGetIncrememnts(String name)
        {
            if (this.additionalProperties.ContainsKey(name))
            {
                this.additionalProperties.Remove(name);
            }
        }
    }
}
