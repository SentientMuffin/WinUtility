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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CurrentClip_Click(object sender, RoutedEventArgs e)
        {
            this.CurrentClip.Text = Clipboard.GetText();
            this.CurrentClipParent.Background = Brushes.Honeydew;
        }

        private async void PullClip_Click(object sender, RoutedEventArgs e)
        {
            // Clear previous state, next step takes a bit of time
            this.PullClip.Text = "Pulling From Cloud";
            this.PullClipParent.Background = Brushes.White;

            // Call API to get content
            var responseObj = await HttpHandler.GetRequestJsonObj();
            responseObj.item.Content.TryGetValue("S", out string cloudContent);
            Clipboard.SetText(cloudContent);

            // Display status update
            this.PullClip.Text = cloudContent;
            this.PullClipParent.Background = Brushes.PaleVioletRed;
        }

        private async void PushClip_Click(object sender, RoutedEventArgs e)
        {
            // API request
            var responseStatusCode = await HttpHandler.PostRequestJsonObj();

            // Display status update
            this.PushClip.Text = "Push To Cloud: Status " + responseStatusCode;
            this.PushClipParent.Background = Brushes.CadetBlue;
        }

    }

}