using Newtonsoft.Json;
using OuiHop.Controllers;
using OuiHop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace OuiHop.Views.SignIn
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ForgetPassword : Page
    {
        public string temp;
        public ForgetPassword()
        {
            this.InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
        }
        private void submitButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SigninRequest();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignInPage));
        }

        private void UserEmailTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UserEmailTxtBox.PlaceholderText != " ")
            {
                UserEmailTxtBox.Header = UserEmailTxtBox.PlaceholderText;
                temp = UserEmailTxtBox.PlaceholderText;
                UserEmailTxtBox.PlaceholderText = " ";
            }
            else
            {
                UserEmailTxtBox.Header = temp;
            }
        }

        private void UserEmailTxtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(UserEmailTxtBox.Text))
            {
                UserEmailTxtBox.PlaceholderText = UserEmailTxtBox.Header.ToString();
                UserEmailTxtBox.Header = "";
            }
            else
            {
                UserEmailTxtBox.Header = temp;
            }
        }
        async void SigninRequest()
        {
            HttpClient client = null;
            client = new HttpClient();

            ForgetPassUser user = new ForgetPassUser();
            user.email = UserEmailTxtBox.Text.ToString();
            if (user == null) return;
            var SerializeJson = JsonConvert.SerializeObject(user);

            //Build Request
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://213.246.61.49/weel-web/api/auth/password/retrieve");
            request.Headers.Add("Api-Version", "1.0");
            request.Headers.Add("App-Version", "0.0.1");
            string UniqueDeviceID = DeviceInfo.Instance.Id;
            request.Headers.Add("Device-UniqueId", UniqueDeviceID);
            request.Headers.Add("Timezone-Format", "UTC");
            request.Headers.Add("User-Agent", "Windows");
            request.Headers.Add("Auth-Token", "");
            request.Method = HttpMethod.Post;
            request.Content = new StringContent(SerializeJson.ToString(), Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);
            string result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                MessageDialog msg = new MessageDialog("Password Request Sent Succefuly");
                await msg.ShowAsync();
            }

            else
            {
                var Deserializedresult = JsonConvert.DeserializeObject<ExceptionClassManager>(result);
                MessageDialog error = new MessageDialog(Deserializedresult.exceptionMessage);
                await error.ShowAsync();
            }

        }
    }
}
