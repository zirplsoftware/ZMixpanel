using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Zirpl.Mixpanel.HttpApi.Events;
using Zirpl.Mixpanel.HttpApi.UserProfiles;

namespace Zirpl.Mixpanel.HttpApi
{
    public class JsonSerializer
    {
        
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
            this.WriteCommonPersonValues(writer, personEvent);
            writer.WritePropertyName("$set");
            writer.WriteRawValue(personEvent.Properties.ToPropertyArrayJson(Formatting.None) ?? "{}");
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
            writer.CastAndWriteValue(personEvent.TransactionDateTime);
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
            writer.WriteRawValue(personEvent.Increments.ToPropertyArrayJson(Formatting.None) ?? "{}");
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
            writer.WriteRawValue(personEvent.Properties.ToPropertyArrayJson(Formatting.None) ?? "{}");
            this.WriteCommonPersonValues(writer, personEvent);
            writer.WriteEnd();
        }

        private void WriteCommonPersonValues(JsonWriter writer, PersonEventBase personEvent)
        {
            writer.WritePropertyName("$token");
            writer.WriteValue(personEvent.ProjectToken);
            writer.WritePropertyName("$distinct_id");
            writer.WriteValue(personEvent.DistinctUserId);
            if (personEvent.Options.MaskIpAddress)
            {
                writer.WritePropertyName("$ip");
                writer.WriteValue(0);
            }
            else if (!String.IsNullOrEmpty(personEvent.IpAddress))
            {
                writer.WritePropertyName("$ip");
                writer.WriteValue(personEvent.IpAddress);
            }
            if (personEvent.EventTime.HasValue)
            {
                writer.WritePropertyName("$time");
                writer.WriteAsMillisecondsSinceEpochBase(personEvent.EventTime.Value);
            }
            if (personEvent.IgnoreTime.HasValue)
            {
                writer.WritePropertyName("$ignore_time");
                writer.WriteValue(personEvent.IgnoreTime.Value);
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
                    writer.WriteRawValue(eVent.Properties.ToPropertyArrayJson(Formatting.None) ?? "{}");
                    writer.WriteEnd();
                }
                returnValue = sb.ToString();
            }

            return returnValue;
        }
    }
}
