using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WinUtility
{
    public class MetaData
    {
        [JsonProperty("RequestId")]
        public String RequestID { get; set; }
        [JsonProperty("HTTPStatusCode")]
        public String HTTPStatusCode { get; set; }
        [JsonProperty("RetryAttempts")]
        public int RetryAttempts { get; set; }
        [JsonProperty("HTTPHeaders")]
        public Dictionary<String, String> HTTPHeaders { get; set; }
    }
}