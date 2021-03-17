using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WinUtility
{
    public static class HttpHandler
    {
        private static readonly string apiEndpoint = "https://pwot5ufm9b.execute-api.us-east-2.amazonaws.com/POC/clips";
        private static readonly HttpClient httpClient = new();

        public static async Task<Response> GetRequestJsonObj()
        {
            Response responseObj = null;
            String rawResponse;

            var requestObj = RequestObjBuilder(RequestTypes.GET, "WIP_MobilePhone");

            var stringRequestObj = await Task.Run(() => JsonConvert.SerializeObject(requestObj));

            HttpContent httpContent = new StringContent(stringRequestObj);

            HttpResponseMessage response = await httpClient.PostAsync(apiEndpoint, httpContent);
            if (response.IsSuccessStatusCode)
            {
                rawResponse = await response.Content.ReadAsStringAsync();
                responseObj = JsonConvert.DeserializeObject<Response>(rawResponse);
            }
            return responseObj;
        }

        public static async Task<int> PostRequestJsonObj()
        {

            DynamoDBContent[] contentList = new DynamoDBContent[]
            {
                new DynamoDBContent
                {
                    ColumnHeader = "Content",
                    ColumnValue = Clipboard.GetText()
                }
            };
            var requestObj = RequestObjBuilder(RequestTypes.POST, "WIP_Windows", contentList);

            var stringRequestObj = await Task.Run(() => JsonConvert.SerializeObject(requestObj));

            HttpContent httpContent = new StringContent(stringRequestObj);

            HttpResponseMessage response = await httpClient.PostAsync(apiEndpoint, httpContent);

            return (int)response.StatusCode;
        }


        // Helper Methods ================================================================================================
        private static JsonObj RequestObjBuilder(string requestType, string clipID)
        {
            return RequestObjBuilder(requestType, clipID, null);
        }

        private static JsonObj RequestObjBuilder(string requestType, string clipID, DynamoDBContent[] contentList)
        {
            return new JsonObj
            {
                CRUD = requestType,
                clip = new Clip
                {
                    ClipID = clipID,
                    ContentList = contentList,
                }
            };
        }
    }
}
