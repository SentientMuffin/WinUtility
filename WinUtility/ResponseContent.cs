using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace WinUtility
{
    public class ResponseContent
    {
        [JsonProperty("ClipID")]
        public Dictionary<String, String> ClipID { get; set; }
        [JsonProperty("Content")]
        public Dictionary<String, String> Content { get; set; }
       
    }
}