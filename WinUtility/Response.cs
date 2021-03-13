using Newtonsoft.Json;

namespace WinUtility
{
    public class Response
    {
        [JsonProperty("Item")]
        public ResponseContent item { get; set; }
        [JsonProperty("ResponseMetadata")]
        public MetaData data { get; set; }
    }
}