using Newtonsoft.Json;
using OuiHop.Common;
using OuiHop.Controllers;
using OuiHop.Models;
using OuiHop.Views.MapView;
using OuiHop.Views.ModeSelection;
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
using winsdkfb;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace OuiHop.Views.SignIn
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SignInPage : Page
    {
        public string mailTemp , passTemp;

        public SignInPage()
        {
            this.InitializeComponent();
        }

        async void SigninRequest()
        {
            HttpClient client = null;
            client = new HttpClient();

            User user = new User();
            user.email = UserEmailTxtBox.Text.ToString();
            user.password = UserPasswordtxtBox.Password.ToString();
            if (user == null) return;
            var SerializeJson = JsonConvert.SerializeObject(user);

            //Build Request
            var request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://213.246.61.49/weel-web/api/auth/signin");
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
                Frame.Navigate(typeof(MainMap));
            }

           else
            {
                var Deserializedresult = JsonConvert.DeserializeObject<ExceptionClassManager>(result);
                MessageDialog error = new MessageDialog(Deserializedresult.exceptionMessage);
                await error.ShowAsync();
            }

        }
        private void UserEmailTxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UserEmailTxtBox.PlaceholderText != " ")
            {
                UserEmailTxtBox.Header = UserEmailTxtBox.PlaceholderText;
                mailTemp = UserEmailTxtBox.PlaceholderText;
                UserEmailTxtBox.PlaceholderText = " ";
            }
            else
            {
                UserEmailTxtBox.Header = mailTemp;
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
                UserEmailTxtBox.Header = mailTemp;
            }

        }
        private void UserPasswordtxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UserPasswordtxtBox.PlaceholderText != " ")
            {
                UserPasswordtxtBox.Header = UserPasswordtxtBox.PlaceholderText;
                passTemp = UserPasswordtxtBox.PlaceholderText;
                UserPasswordtxtBox.PlaceholderText = " ";
            }
            else
            {
                UserPasswordtxtBox.Header = passTemp;
            }
        }
        private void UserPasswordtxtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(UserEmailTxtBox.Text))
            {
                UserPasswordtxtBox.PlaceholderText = UserPasswordtxtBox.Header.ToString();
                UserPasswordtxtBox.Header = "";
            }
            else
            {
                UserPasswordtxtBox.Header = passTemp;
            }
        }
        private void signinButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            SigninRequest();
        }

        private async void FaceBookPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FBSession sess = FBSession.ActiveSession;
            sess.FBAppId = "1836050649967083";
            sess.WinAppId = "s-1-15-2-4077727447-3229361925-2416648694-1706077684-1728342750-3868544593-3947295911";
            List<string> permissionList = new List<string>();
            permissionList.Add("public_profile");
            permissionList.Add("email");

            FBPermissions permissions = new FBPermissions(permissionList);
            var result = await sess.LoginAsync(permissions);
            if (result.Succeeded)
            {
                App.OuiApp.fbUser = new facebookUser();
                App.OuiApp.fbUser.email = sess.User.Email;
                App.OuiApp.fbUser.firstName = sess.User.FirstName;
                App.OuiApp.fbUser.lastName = sess.User.LastName;
                App.OuiApp.fbUser.gender = sess.User.Gender;
                Frame.Navigate(typeof(ModeSelectionPage));
               // App.OuiApp.fbUser.Image = sess.User.Picture;
            }

        }
        private void ForgetPassHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ForgetPassword));
        }
        private void NoAccount_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ModeSelectionPage));
        }
        private void OuiTerms_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(WebPage.Web));
        }  
    }
}
