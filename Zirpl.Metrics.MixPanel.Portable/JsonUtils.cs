using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Zirpl.Core;

namespace Zirpl.Metrics.MixPanel
{
    public static class JsonUtils
    {
        public readonly static DateTime EpochBaseDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public const String DateTimeFormatTemplate = "{0}T{1}";
        public const String DateFormat = "yyyy-MM-dd";
        public const String TimeFormat = "hh:mm:ss";

        public static void WriteAsMillisecondsSinceEpochBase(this JsonWriter writer, DateTime time)
        {
            writer.WriteValue(Convert.ToInt64(time.Subtract(EpochBaseDateTime).TotalMilliseconds));
        }

        public static void CastAndWriteValue(this JsonWriter writer, Object value)
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
    }
}
