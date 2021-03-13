using System;
using Newtonsoft.Json;

namespace WinUtility
{
    public class Clip
    {
        [JsonProperty("ClipID")]
        public String ClipID { get; set; }
        [JsonProperty("ContentList")]
        public DynamoDBContent[] ContentList { get; set; }
    }
}