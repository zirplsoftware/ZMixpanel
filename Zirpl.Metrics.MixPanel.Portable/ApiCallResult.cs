using System;

namespace Zirpl.Mixpanel.HttpApi
{
    public class ApiCallResult
    {
        public Int32 Status { get; set; }
        public String Error { get; set; }
        public bool IsSuccess { get; set; }
        public String RawResult { get; set; }
    }
}
