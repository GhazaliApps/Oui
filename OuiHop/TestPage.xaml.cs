using OuiHop.Helpers;
using OuiHop.Models.SignInResponse;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using Windows.Globalization.DateTimeFormatting;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using winsdkfb;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace OuiHop
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TestPage : Page
    {
        HttpClient OperationWebClient = null;
        public TestPage()
        {
            this.InitializeComponent();
            

        /*    OperationWebClient = new HttpClient();
            OperationWebClient.BaseAddress = new Uri("http://213.246.61.49/weel-web/");
            OperationWebClient.DefaultRequestHeaders.Add("Api-Version", "1.0");
            OperationWebClient.DefaultRequestHeaders.Add("App-Version", "0.0.1");
            string UniqueDeviceID = DeviceInfo.Instance.Id;
            OperationWebClient.DefaultRequestHeaders.Add("Device-UniqueId", UniqueDeviceID);
            OperationWebClient.DefaultRequestHeaders.Add("Timezone-Format", "UTC");
            OperationWebClient.DefaultRequestHeaders.Add("User-Agent", "Windows Phone");
            var cultureName = new DateTimeFormatter("longdate", new[] { "US" }).ResolvedLanguage;
            CultureInfo cultureInfo = CultureInfo.CurrentCulture;
            CultureInfo currentUIInfo = CultureInfo.CurrentUICulture;
            if (cultureName.ToString() == "en-US")
                OperationWebClient.DefaultRequestHeaders.Add("User-Lang", "EN");
            else if (cultureName.ToString() == "fr-FR")
                OperationWebClient.DefaultRequestHeaders.Add("User-Lang", "FR");
            else
                OperationWebClient.DefaultRequestHeaders.Add("User-Lang", "FR");
            //OperationWebClient.DefaultRequestHeaders.Add("Content-Type", "application/json"); // not for all operations
            OperationWebClient.DefaultRequestHeaders.Add("Auth-Token", ""); 
            */
        }

 /*       private async void Page_Loaded(object sender, RoutedEventArgs e)
        {   //Get Data 
            /* string getCars = "api/car/getAllCarMake";
              HttpResponseMessage response = await OperationWebClient.GetAsync(getCars);
              string result = await response.Content.ReadAsStringAsync();
              Result.Text = result.ToString();
              ResponseStatus.Text = response.EnsureSuccessStatusCode().ToString();
          
            //Post Data
           HttpClient client = null;
            client= new HttpClient();
            //client.BaseAddress = new Uri("http://213.246.61.49/weel-web/");
            string signinuser = "api/auth/signin";
            var User = new user();
            User.username = "test@ouihop2.com";
            User.password = "1234";
            if (User == null) return;

            var ser = new DataContractJsonSerializer(typeof(user));
            using (var ms = new MemoryStream())
            {
                ser.WriteObject(ms, User);
                ms.Position = 0;
                 var json = Encoding.UTF8.GetString(ms.ToArray(), 0, (int) ms.Length);
                 //var content = new StringContent(json).ToString();
                //content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                /* HttpResponseMessage response = null;
                 response = await client.PostAsync(signinuser, content);
                 string result = await response.Content.ReadAsStringAsync();
                 Result.Text = result.ToString();
                 ResponseStatus.Text = response.EnsureSuccessStatusCode().ToString();
          
        
        var cultureName = new DateTimeFormatter("longdate", new[] { "US" }).ResolvedLanguage;
                CultureInfo cultureInfo = CultureInfo.CurrentCulture;
                CultureInfo currentUIInfo = CultureInfo.CurrentUICulture;

                var request = new HttpRequestMessage();
                request.RequestUri = new Uri("http://213.246.61.49/weel-web/api/auth/signin");
                request.Headers.Add("Api-Version", "1.0");
                request.Headers.Add("App-Version", "0.0.1");
                string UniqueDeviceID = DeviceInfo.Instance.Id;
                request.Headers.Add("Device-UniqueId", UniqueDeviceID);
                request.Headers.Add("Timezone-Format", "UTC");
                request.Headers.Add("User-Agent", "IOS");
                if (cultureName.ToString() == "en-US")
                    request.Headers.Add("User-Lang", "EN");
                else if (cultureName.ToString() == "fr-FR")
                    request.Headers.Add("User-Lang", "FR");
                else
                request.Headers.Add("User-Lang", "FR");
               // request.Headers.Add("Content-Type", "application/json");
                request.Headers.Add("Auth-Token", "");
                request.Method = HttpMethod.Post;
                request.Content = new StringContent("{\"email\":\"test@ouihop2.com\",\"password\":1234}", Encoding.UTF8, "application/json");
                var response = await client.SendAsync(request);

                string result = await response.Content.ReadAsStringAsync();
                Result.Text = result.ToString();
                ResponseStatus.Text = response.EnsureSuccessStatusCode().ToString();
            }
        }*/

        private  void FlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         
        }

        private void Main_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private void Second_Click(object sender, RoutedEventArgs e)
        {

        }

      async  private void FBLoginButton_Click(object sender, RoutedEventArgs e)
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
                string name = sess.User.Name;
               
            }

        }

        /*  void CanvasControl_Draw(CanvasControl sender, CanvasDrawEventArgs args)
 {
     args.DrawingSession.DrawEllipse(155, 115, 80, 30, Colors.Black, 3);
     args.DrawingSession.DrawText("Hello, world!", 100, 100, Colors.Yellow);
 }
 public SolidColorBrush GetSolidColorBrush(string hex)
 {
     hex = hex.Replace("#", string.Empty).Trim().ToString();
     byte a = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
     byte r = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
     byte g = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
     byte b = (byte)(Convert.ToUInt32(hex.Substring(6, 2), 16));
     SolidColorBrush myBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, b));
     return myBrush;
 }

 private void colorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
 {
     var selectedItem = colorList.SelectedItem as GridViewItem;
     if (selectedItem != null)
     {
         // var grid = selectedItem.ContentTemplateRoot as Ellipse;
         //grid.Opacity = 1;

         foreach (GridViewItem color in colorList.Items)
         {
             if (color != selectedItem)
             {
                 var plate = color.ContentTemplateRoot as Ellipse;
                 color.Opacity = 0.5;
             }
             else
             {
                 var plate = color.ContentTemplateRoot as Ellipse;
                 color.Opacity = 1;
             }
         }
     }
 }
*/

    }
}
