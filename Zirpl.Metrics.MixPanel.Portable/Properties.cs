using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Zirpl.Mixpanel.HttpApi
{
    public class Properties<TValue> : IPropertyArrayString
    {
        private readonly Dictionary<String, TValue> _properties;

        public Properties()
        {
            this._properties = new Dictionary<string, TValue>();
        }

        public Properties<TValue> Add<T>(String key, T value) where T : TValue
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            if (this._properties.ContainsKey(key))
            {
                this._properties[key] = value;
            }
            else
            {
                this._properties.Add(key, value);
            }
            return this;
        }
        public Properties<TValue> AddAll(IDictionary<String, TValue>  properties)
        {
            if (properties != null)
            {
                foreach (var key in properties.Keys)
                {
                    this.Add(key, properties[key]);
                }
            }
            return this;
        }
        public Properties<TValue> AddAll(Properties<TValue> properties)
        {
            if (properties != null)
            {
                var dictionary = properties.GetAll();
                foreach (var key in dictionary.Keys)
                {
                    this.Add(key, dictionary[key]);
                }
            }
            return this;
        }

        public TProperties Add<T, TProperties>(String key, T value) 
            where T : TValue
            where TProperties : Properties<TValue>
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            if (this._properties.ContainsKey(key))
            {
                this._properties[key] = value;
            }
            else
            {
                this._properties.Add(key, value);
            }
            return (TProperties)this;
        }

        public TValue Get(String key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            return this._properties.ContainsKey(key)
                ? this._properties[key]
                : default(TValue);
        }

        public T Get<T>(String key) where T: TValue
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            return this._properties.ContainsKey(key)
                ? (T)this._properties[key]
                : default(T);
        }

        public Dictionary<string, TValue> GetAll()
        {
            return new Dictionary<string, TValue>(this._properties);
        }

        public void Clear()
        {
            this._properties.Clear();
        }

        protected internal int Count
        {
            get
            {
             return    this._properties.Count;
            }
        }

        public void Remove(String key)
        {
            if (String.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }
            if (this._properties.ContainsKey(key))
            {
                this._properties.Remove(key);
            }
        }

        public string ToPropertyArrayJson()
        {
            return ToPropertyArrayJson(Formatting.Indented);
        }

        public string ToPropertyArrayJson(Formatting formatting)
        {
            String returnValue = null;
            if (this._properties.Count > 0)
            {
                var sb = new StringBuilder();
                using (var sw = new StringWriter(sb))
                {
                    using (var writer = new JsonTextWriter(sw))
                    {
                        writer.Formatting = formatting;

                        // https://mixpanel.com/docs/api-documentation/http-specification-insert-data
                        //   {
                        //        "ip": "123.123.123.123",  // resevered
                        //        "token": "e3bc4100330c35722740fb8c6f5abddc",  // resevered
                        //        "time": 1245613885,  //reserved
                        //        "action": "play"
                        //    }

                        writer.WriteStartObject();
                        foreach (var key in this._properties.Keys.ToArray().OrderBy(o => o))
                        {
                            writer.WritePropertyName(key);
                            writer.CastAndWriteValue(_properties[key]);
                        }
                        writer.WriteEnd();
                    }
                    returnValue = sb.ToString();
                }
            }

            return returnValue;
        }
    }
}
