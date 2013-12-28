using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Zirpl.Core;

namespace Zirpl.Metrics.MixPanel
{
    public class EventJsonSerializer
    {
        public readonly static DateTime EpochBaseDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        private const String DateTimeFormatTemplate = "{0}T{1}";
        private const String DateFormat = "yyyy-MM-dd";
        private const String TimeFormat = "hh:mm:ss";
        
        public virtual String GetJson(PersonEventBase personEvent)
        {
            String returnValue = null;

            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    writer.Formatting = Formatting.None;

                    if (personEvent is PersonSetEvent)
                    {
                        this.WriteSetJson(writer, (PersonSetEvent)personEvent);
                    }
                    else if (personEvent is PersonSetOnceEvent)
                    {
                        this.WriteSetOnceJson(writer, (PersonSetOnceEvent)personEvent);
                    }
                    else if (personEvent is PersonTransactionEvent)
                    {
                        this.WriteTrackChargeJson(writer, (PersonTransactionEvent)personEvent);
                    }
                    else if (personEvent is PersonIncrementEvent)
                    {
                        this.WriteIncrementJson(writer, (PersonIncrementEvent)personEvent);
                    }
                }
                returnValue = sb.ToString();
            }

            return returnValue;
        }

        protected virtual void WriteSetJson(JsonWriter writer, PersonSetEvent personEvent)
        {
            // https://mixpanel.com/docs/people-analytics/people-http-specification-insert-data
            //{
            //    "$set": {
            //        "$first_name": "John",
            //        "$last_name": "Smith"
            //    },
            //    "$token": "36ada5b10da39a1347559321baf13063",
            //    "$distinct_id": "13793",
            //    "$ip": "123.123.123.123"
            //}

            writer.WriteStartObject();
            writer.WritePropertyName("$set");
            writer.WriteStartObject();
            if (personEvent.FirstName != null
                || personEvent.ResetFirstName)
            {
                writer.WritePropertyName("$first_name");
                writer.WriteValue(personEvent.FirstName);
            }
            if (personEvent.LastName != null
                || personEvent.ResetLastName)
            {
                writer.WritePropertyName("$last_name");
                writer.WriteValue(personEvent.LastName);
            }
            if (personEvent.Name != null
                || personEvent.ResetName)
            {
                writer.WritePropertyName("$name");
                writer.WriteValue(personEvent.Name);
            }
            if (personEvent.Email != null
                || personEvent.ResetEmail)
            {
                writer.WritePropertyName("$email");
                writer.WriteValue(personEvent.Email);
            }
            if (personEvent.IgnoreTime)
            {
                writer.WritePropertyName("$username");
                writer.WriteValue(personEvent.Username);
            }
            // TODO: need to format "created" to match the USER's timezone
            if (personEvent.Created.HasValue
                || personEvent.ResetCreated)
            {
                writer.WritePropertyName("$created");
                if (personEvent.Created.HasValue)
                {
                    writer.WriteValue(String.Format(DateTimeFormatTemplate, personEvent.Created.Value.ToString(DateFormat),personEvent.Created.Value.ToString(TimeFormat)));
                }
                else
                {
                    writer.WriteNull();
                }
            }
            if (personEvent.Username != null
                || personEvent.ResetUsername)
            {
                writer.WritePropertyName("$ignore_time");
                writer.WriteValue(true);
            }
            foreach (KeyValuePair<String, Object> pair in personEvent.GetProperties())
            {
                if (!String.IsNullOrEmpty(pair.Key))
                {
                    writer.WritePropertyName(pair.Key);
                    this.WriteValue(writer, pair.Value);
                }
            }
            writer.WriteEnd();

            this.WriteCommonPersonValues(writer, personEvent);

            writer.WriteEnd();
        }

        protected virtual void WriteTrackChargeJson(JsonWriter writer, PersonTransactionEvent personEvent)
        {
            // https://mixpanel.com/docs/people-analytics/people-http-specification-insert-data
            //{
            //    "$append": {
            //        "$transactions": {
            //            "$time": "2013-01-03T00:58:05",
            //            "$amount": 25.34
            //        }
            //    },
            //    "$token": "36ada5b10da39a1347559321baf13063",
            //    "$distinct_id": "13793"
            //}

            writer.WriteStartObject();
            writer.WritePropertyName("$append");
            writer.WriteStartObject();
            writer.WritePropertyName("$transactions");
            writer.WriteStartObject();
            writer.WritePropertyName("$time");
            this.WriteValue(writer, personEvent.TransactionDateTime);
            writer.WritePropertyName("$amount");
            writer.WriteValue(personEvent.TransactionAmount);
            writer.WriteEnd();
            writer.WriteEnd();

            this.WriteCommonPersonValues(writer, personEvent);

            writer.WriteEnd();
        }

        protected virtual void WriteIncrementJson(JsonWriter writer, PersonIncrementEvent personEvent)
        {
            // https://mixpanel.com/docs/people-analytics/people-http-specification-insert-data
            //{
            //    "$add": {
            //          "dollars spent": 17,
            //          "credits remaining": -34 // Subtract by passing a negative value
            //    },
            //    "$token": "36ada5b10da39a1347559321baf13063",
            //    "$distinct_id": "13793"
            //}

            writer.WriteStartObject();
            writer.WritePropertyName("$add");
            writer.WriteStartObject();
            foreach (KeyValuePair<String, decimal> pair in personEvent.GetIncrememnts())
            {
                if (!String.IsNullOrEmpty(pair.Key))
                {
                    writer.WritePropertyName(pair.Key);
                    this.WriteValue(writer, pair.Value);
                    //writer.WriteRawValue(pair.Value.ToString("G"));
                }
            }
            writer.WriteEnd();

            this.WriteCommonPersonValues(writer, personEvent);
            
            writer.WriteEnd();
        }

        protected virtual void WriteSetOnceJson(JsonWriter writer, PersonSetOnceEvent personEvent)
        {
            // https://mixpanel.com/docs/people-analytics/people-http-specification-insert-data
            //{
            //    "$set_once": {
            //          "initial referrer": "Dr. So and So"
            //    },
            //    "$token": "36ada5b10da39a1347559321baf13063",
            //    "$distinct_id": "13793"
            //}

            writer.WriteStartObject();
            writer.WritePropertyName("$set_once");
            writer.WriteStartObject();
            foreach (KeyValuePair<String, Object> pair in personEvent.GetProperties())
            {
                if (!String.IsNullOrEmpty(pair.Key))
                {
                    writer.WritePropertyName(pair.Key);
                    this.WriteValue(writer, pair.Value);
                }
            }
            writer.WriteEnd();

            this.WriteCommonPersonValues(writer, personEvent);

            writer.WriteEnd();
        }

        private void WriteCommonPersonValues(JsonWriter writer, PersonEventBase personEvent)
        {
            writer.WritePropertyName("$token");
            writer.WriteValue(personEvent.ProjectToken);
            writer.WritePropertyName("$distinct_id");
            writer.WriteValue(personEvent.DistinctId);
            if (personEvent.MaskIpAddress)
            {
                writer.WritePropertyName("$ip");
                writer.WriteValue(0);
            }
            else if (!String.IsNullOrEmpty(personEvent.IpAddress))
            {
                writer.WritePropertyName("$ip");
                writer.WriteValue(personEvent.IpAddress);
            }
        }

        protected virtual void WriteValue(JsonWriter writer, Object value)
        {
                    if (value == null)
                    {
                        writer.WriteNull();
                    }
                    else if (value is String)
                    {
                        writer.WriteValue((String)value);
                    }
                    else if (value is Int32)
                    {
                        writer.WriteValue((Int32)value);
                    }
                    else if (value is Decimal)
                    {
                        writer.WriteRawValue(((Decimal)value).ToString("G"));
                    }
                    else if (value is bool)
                    {
                        writer.WriteValue((bool)value);
                    }
                    else if (value is DateTime)
                    {
                        DateTime valueAsDateTime = (DateTime)value;
                        writer.WriteValue(String.Format(DateTimeFormatTemplate, valueAsDateTime.ToString(DateFormat), valueAsDateTime.ToString(TimeFormat)));
                    }
                    else if (value is byte)
                    {
                        writer.WriteValue((byte)value);
                    }
                    else if (value is Guid)
                    {
                        writer.WriteValue(((Guid)value).ToString());
                    }
                    else if (value is Int16)
                    {
                        writer.WriteValue((Int16)value);
                    }
                    else if (value is Int64)
                    {
                        writer.WriteValue((Int64)value);
                    }
                    else if (value is DateOnlyWrapper)
                    {
                        DateTime valueAsDate = ((DateOnlyWrapper)value).Date;
                        writer.WriteValue(valueAsDate.ToString(DateFormat));
                    }
                    else if (value is char)
                    {
                        writer.WriteValue((char)value);
                    }
                    else if (value is Double)
                    {
                        writer.WriteRawValue(((Double)value).ToString("G"));
                    }
                    else if (value is float)
                    {
                        writer.WriteRawValue(((float)value).ToString("G"));
                    }
                    else if (value is sbyte)
                    {
                        writer.WriteValue((sbyte)value);
                    }
                    else if (value is uint)
                    {
                        writer.WriteValue((uint)value);
                    }
                    else if (value is ulong)
                    {
                        writer.WriteValue((ulong)value);
                    }
                    else if (value is ushort)
                    {
                        writer.WriteValue((ushort)value);
                    }
                    else if (value is Enum)
                    {
                        writer.WriteValue((Enum)value);
                    }
                    else
                    {
                        writer.WriteValue(value);
                    }
        }

        public virtual String GetJson(Event eVent)
        {
            String returnValue = null;

            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    writer.Formatting = Formatting.None;

                    // https://mixpanel.com/docs/api-documentation/http-specification-insert-data
                    //{   "event": "game", 
                    //    "properties": {
                    //        "ip": "123.123.123.123",  // resevered
                    //        "token": "e3bc4100330c35722740fb8c6f5abddc",  // resevered
                    //        "time": 1245613885,  //reserved
                    //        "action": "play"
                    //    }
                    //}

                    writer.WriteStartObject();
                    writer.WritePropertyName("event");
                    writer.WriteValue(eVent.EventName);
                    writer.WritePropertyName("properties");
                    writer.WriteStartObject();
                    if (!String.IsNullOrEmpty(eVent.IpAddress))
                    {
                        writer.WritePropertyName("ip");
                        writer.WriteValue(eVent.IpAddress);
                    }
                    writer.WritePropertyName("token");
                    writer.WriteValue(eVent.ProjectToken);
                    if (eVent.EventTime.HasValue)
                    {
                        writer.WritePropertyName("time");
                        writer.WriteValue(Convert.ToInt64(eVent.EventTime.Value.Subtract(EpochBaseDateTime).TotalSeconds));
                    }
                    if (!String.IsNullOrEmpty(eVent.DistinctUserId))
                    {
                        writer.WritePropertyName("distinct_id");
                        writer.WriteValue(eVent.DistinctUserId);
                    }
                    if (!String.IsNullOrEmpty(eVent.UserNameTag))
                    {
                        writer.WritePropertyName("mp_name_tag");
                        writer.WriteValue(eVent.UserNameTag);
                    }
                    foreach (KeyValuePair<String, Object> pair in eVent.GetProperties())
                    {
                        if (!String.IsNullOrEmpty(pair.Key))
                        {
                            writer.WritePropertyName(pair.Key);
                            this.WriteValue(writer, pair.Value);
                        }
                    }
                    writer.WriteEnd();
                    writer.WriteEnd();
                }
                returnValue = sb.ToString();
            }

            return returnValue;
        }
    }
}
