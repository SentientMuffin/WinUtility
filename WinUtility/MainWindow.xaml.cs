using System;
using System.Windows;
using System.Windows.Media;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

namespace WinUtility
{

    // ======================== Mess ====================================================

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CurrentClip_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentClip.Content = "Current ClipBoard: " + Clipboard.GetText();
            this.CurrentClip.Background = Brushes.Honeydew;
        }

        private String apiEndpoint = "https://pwot5ufm9b.execute-api.us-east-2.amazonaws.com/POC/clips";
        static HttpClient httpClient = new();

        static async Task<Response> GetRequestJsonObj(string path)
        {
            Response responseObj = null;
            String rawResponse;

            var requestObj = new JsonObj
            {
                CRUD = "GET",
                clip = new Clip {
                    //ClipID = "FlutterTest",
                    ClipID = "WIP_MobilePhone",
                    ContentList = null,
                }
            };

            var stringRequestObj = await Task.Run(() => JsonConvert.SerializeObject(requestObj));

            HttpContent httpContent = new StringContent(stringRequestObj);

            HttpResponseMessage response = await httpClient.PostAsync(path, httpContent);
            if (response.IsSuccessStatusCode)
            {
                rawResponse = await response.Content.ReadAsStringAsync();
                responseObj = JsonConvert.DeserializeObject<Response>(rawResponse);
            }
            return responseObj;
        }

        private async void PullClip_Click(object sender, RoutedEventArgs e)
        {
            // Call API to get content
            var responseObj = await GetRequestJsonObj(apiEndpoint);
            String cloudContent;
            responseObj.item.Content.TryGetValue("S", out cloudContent);

            // Display status update
            this.PullClip.Content = "Pull From Cloud\nContent: " + cloudContent;
            this.PullClip.Background = Brushes.PaleVioletRed;
        }

        static async Task<int> PostRequestJsonObj(String path, JsonObj requestObj)
        {

            var stringRequestObj = await Task.Run(() => JsonConvert.SerializeObject(requestObj));

            HttpContent httpContent = new StringContent(stringRequestObj);

            HttpResponseMessage response = await httpClient.PostAsync(path, httpContent);

            return (int) response.StatusCode;
        }

        private async void PushClip_Click(object sender, RoutedEventArgs e)
        {
            // Create Post Body
            var requestObj = new JsonObj
            {
                CRUD = "POST",
                clip = new Clip
                {
                    //ClipID = "FlutterTest",
                    ClipID = "WIP_Windows",
                    ContentList = new DynamoDBContent[] {
                        new DynamoDBContent {
                            ColumnHeader = "Content",
                            ColumnValue = Clipboard.GetText()
                        }
                    }
                }
            };

            // API request
            var responseStatusCode = await PostRequestJsonObj(apiEndpoint, requestObj);

            // Display status update
            this.PushClip.Content = "Push To Cloud: Status " + responseStatusCode;
            this.PushClip.Background = Brushes.CadetBlue;
        }

    }

}