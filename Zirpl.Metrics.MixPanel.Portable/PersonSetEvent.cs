using System;
using System.Collections.Generic;
using Zirpl.Core;

namespace Zirpl.Metrics.MixPanel
{
    public class PersonSetEvent : PersonEventBase
    {
        private readonly Dictionary<String, Object> additionalProperties;

        protected internal PersonSetEvent()
        {
            this.additionalProperties = new Dictionary<string, object>();
        }

        private String firstName;
        private String lastName;
        private String name;
        private String email;
        private String username;
        private DateTime? created;

        public bool IgnoreTime { get; set; }
        public String FirstName
        {
            get { return this.firstName; }
            set
            {
                this.ResetFirstName = String.IsNullOrEmpty(value);
                this.firstName = value;
            }
        }
        protected internal bool ResetFirstName { get; private set; }
        public String LastName
        {
            get { return this.lastName; }
            set
            {
                this.ResetLastName = String.IsNullOrEmpty(value);
                this.lastName = value;
            }
        }
        protected internal bool ResetLastName { get; private set; }
        public String Name
        {
            get { return this.name; }
            set
            {
                this.ResetName = String.IsNullOrEmpty(value);
                this.name = value;
            }
        }
        protected internal bool ResetName { get; private set; }
        public String Username
        {
            get { return this.username; }
            set
            {
                this.ResetUsername = String.IsNullOrEmpty(value);
                this.username = value;
            }
        }
        protected internal bool ResetUsername { get; private set; }
        public String Email
        {
            get { return this.email; }
            set
            {
                this.ResetEmail = String.IsNullOrEmpty(value);
                this.email = value;
            }
        }
        protected internal bool ResetEmail { get; private set; }
        public DateTime? Created
        {
            get { return this.created; }
            set
            {
                this.ResetCreated = !value.HasValue;
                this.created = value;
            }
        }
        protected internal bool ResetCreated { get; private set; }

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
