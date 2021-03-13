using System;
using Newtonsoft.Json;

namespace WinUtility
{
    // TODO clean up this mess
    public class JsonObj
    {
        [JsonProperty("CRUD")]
        public String CRUD { get; set; }
        [JsonProperty("Request")]
        public Clip clip { get; set; }
    }
}