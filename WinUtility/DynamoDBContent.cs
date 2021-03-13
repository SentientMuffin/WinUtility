using System;
using Newtonsoft.Json;

namespace WinUtility
{
    public class DynamoDBContent
    {
        [JsonProperty("Column_Header")]
        public String ColumnHeader { get; set; }
        [JsonProperty("Column_Value")]
        public String ColumnValue { get; set; }
    }

}