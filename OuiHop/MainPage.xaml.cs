using OuiHop.Helpers;
using OuiHop.Models.SignUp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Popups;
using Windows.UI.Xaml.Shapes;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OuiHop.Views.SignIn;
using OuiHop.Models;
using System.Globalization;
using Windows.Globalization.DateTimeFormatting;
using Windows.UI;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace OuiHop
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        CameraCaptureUI _capture_ui = new CameraCaptureUI();
        string Validimageuri = "ms-appx:///Images/SignUp/Validation/true.png";
        string Notvalidimageuri = "ms-appx:///Images/SignUp/Validation/cross.png";
        //Google Places
        private const string ApiKey = "AIzaSyDRgqi6JDcB8xXqUEE0bPuz8UPpH5yGlwI";
        string CountrySelected = "";
        string Gender = " ";
        string birthYear = " ";
        DateTime SelectedbirthYear = new DateTime();
        SignUpUser User = new SignUpUser();
        byte[] buffer = null;
        string txtMailTemp, txtPassTemp, txtMobileTemp, TxtCodeTemp;
        string txtFnTemp, txtLnTemp;
        string txtModelTemp, txtPlateTemp;
        public Style redTxtBoxHeaderStyle ;
        public Style BlueTxtBoxHeaderStyle ;
        public Style redPassBoxHeaderStyle;
        public Style BluePassBoxHeaderStyle;
        bool IsMailvalid = false; bool IsPassValid=false; bool IsPhoneValid = false;
        bool IsFnValid = false; bool IsLnValid = false; bool IsGenderValid = false; bool IsDobValid=false;
        bool WorkAddress = false; bool HomeAddress = false;

        public MainPage()
        {
            this.InitializeComponent();
            HomeSearchBox.TextChanged += HomeSearchBox_TextChanged;
            HomeSearchBox.SuggestionChosen += HomeSearchBox_SuggestionChosen;
            HomeSearchBox.QuerySubmitted+= HomeSearchBox_QuerySubmitted;
            CompanySearchBox.TextChanged += CompanySearchBox_TextChanged;
            CompanySearchBox.SuggestionChosen += CompanySearchBox_SuggestionChosen;
            OuiCompanies.TextChanged += OuiCompanies_TextChanged;
            OuiCompanies.SuggestionChosen += OuiCompanies_SuggestionChosen;
            redTxtBoxHeaderStyle = (Style)this.Resources["ErrorTextBox"];
            BlueTxtBoxHeaderStyle = (Style)this.Resources["TrueTextBox"];
            redPassBoxHeaderStyle = (Style)this.Resources["RedPassStyle"];
            BluePassBoxHeaderStyle = (Style)this.Resources["BluePassStyle"];
    }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            IsBusinessCar.OnContent = "";
            IsBusinessCar.OffContent = "";
            #region FillYearsComboBox
            var _folder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            _folder = await _folder.GetFolderAsync("Data");
            var _file = await _folder.GetFileAsync("years.txt");
            IList<string> _allYears = await Windows.Storage.FileIO.ReadLinesAsync(_file);
            _year.ItemsSource = _allYears;
            #endregion
            #region FillListCarBrands
            var _carsFile = await _folder.GetFileAsync("cars.txt");
            IList<string> _allCars = await FileIO.ReadLinesAsync(_carsFile);
            listCarBrands.ItemsSource = _allCars;
            #endregion
           if (App.OuiApp.userMode == "DRIVER")
            {
                Dline1.Visibility = Visibility.Visible;
                BasicInfo.Visibility = Visibility.Visible;
                firstTitle.Visibility = Visibility.Visible;
            }
            else if (App.OuiApp.userMode == "PEDESTRIAN")
            {
                Pline1.Visibility = Visibility.Visible;
                BasicInfo.Visibility = Visibility.Visible;
                firstTitle.Visibility = Visibility.Visible;
            }
            if (App.OuiApp.fbUser != null)
            {
                txtEmail.Text = App.OuiApp.fbUser.email;
                txtEmail.Header = txtEmail.PlaceholderText;
                MailimageValid.Source = new BitmapImage(new Uri(Validimageuri, UriKind.Absolute));

                FNtxtBox.Text = App.OuiApp.fbUser.firstName;
                FNtxtBox.Header = FNtxtBox.PlaceholderText;
                FNimageValid.Source=new BitmapImage(new Uri(Validimageuri, UriKind.Absolute));

                LNtxtBox.Text = App.OuiApp.fbUser.lastName;
                LNtxtBox.Header = LNtxtBox.PlaceholderText;
                LNimageValid.Source = new BitmapImage(new Uri(Validimageuri, UriKind.Absolute));

            }
        }
        #region BasicInfoMethods
        
        private void txtEmail_GotFocus(object sender, RoutedEventArgs e)

        {
            txtEmail.Style = BlueTxtBoxHeaderStyle;
            MailimageValid.Source = null;
            if (txtEmail.PlaceholderText != " ")

            {

                txtEmail.Header = txtEmail.PlaceholderText;

                txtMailTemp = txtEmail.PlaceholderText;

                txtEmail.PlaceholderText = " ";

            }

            else

            {

                txtEmail.Header = txtMailTemp;

            }

        }

        private void txtEmail_LostFocus(object sender, RoutedEventArgs e)

        {
            if (String.IsNullOrWhiteSpace(txtEmail.Text.ToString()))

            {

                txtEmail.PlaceholderText = txtEmail.Header.ToString();
                txtEmail.Header = " ";
                PassimageValid.Source = null;

            }

            else

            {

              if (ValidationManager.ValidateEmail(txtEmail.Text))

                {

                    MailimageValid.Source = new BitmapImage(new Uri(Validimageuri, UriKind.Absolute));
                    txtEmail.Style = BlueTxtBoxHeaderStyle;
                    IsMailvalid = true;

                }

                else

                {

                    MailimageValid.Source = new BitmapImage(new Uri(Notvalidimageuri, UriKind.Absolute));
                    txtEmail.Style = redTxtBoxHeaderStyle;
                    IsMailvalid = false;
                }
                
                txtEmail.Header = txtMailTemp;

            }

        }

        private void txtPassword_GotFocus(object sender, RoutedEventArgs e)

        {
            txtPassword.Style = BluePassBoxHeaderStyle;
            PassimageValid.Source = null;
            if (txtPassword.PlaceholderText != " ")

            {
                txtPassword.Header = txtPassword.PlaceholderText;

                txtPassTemp = txtPassword.PlaceholderText;

                txtPassword.PlaceholderText = " ";

            }

            else

            {

                txtPassword.Header = txtPassTemp;

            }

        }
        private void txtPassword_LostFocus(object sender, RoutedEventArgs e)

        {

            if (String.IsNullOrWhiteSpace(txtPassword.Password.ToString()))

            {

                txtPassword.PlaceholderText = txtPassword.Header.ToString();

                txtPassword.Header = " ";

                PassimageValid.Source = null;

            }

            else

            {

                if (txtPassword.Password.Length >= 4)

                {

                    PassimageValid.Source = new BitmapImage(new Uri(Validimageuri, UriKind.Absolute));
                    txtPassword.Style = BluePassBoxHeaderStyle;
                    IsPassValid = true;
                }

                else

                {

                    PassimageValid.Source = new BitmapImage(new Uri(Notvalidimageuri, UriKind.Absolute));
                    txtPassword.Style = redPassBoxHeaderStyle;
                    IsPassValid = false;
                }

                txtPassword.Header = txtPassTemp;

            }

        }

        private void txtPhone_GotFocus(object sender, RoutedEventArgs e)

        {
            txtPhone.Style = BlueTxtBoxHeaderStyle;
            MobileimageValid.Source = null;
            if (txtPhone.PlaceholderText != " ")
            {

                txtPhone.Header = txtPhone.PlaceholderText;

                txtMobileTemp = txtPhone.PlaceholderText;

                txtPhone.PlaceholderText = " ";

            }

            else

            {

                txtPhone.Header = txtMobileTemp;

            }

        }
        
        private async void txtPhone_LostFocus(object sender, RoutedEventArgs e)

        {

            if (String.IsNullOrWhiteSpace(txtPhone.Text.ToString()))

            {

                txtPhone.PlaceholderText = txtPhone.Header.ToString();

                txtPhone.Header = " ";

                MobileimageValid.Source = null;

            }

            else

            {

                if (CountrySelected == "France")

                {

                    if (!String.IsNullOrWhiteSpace(txtPhone.Text) && txtPhone.Text.Length >= 10 && txtPhone.Text.Length <= 11)

                    {

                        MobileimageValid.Source = new BitmapImage(new Uri(Validimageuri, UriKind.Absolute));
                        txtPhone.Style = BlueTxtBoxHeaderStyle;
                        IsPhoneValid = true;
                    }

                    else

                    {

                        MobileimageValid.Source = new BitmapImage(new Uri(Notvalidimageuri, UriKind.Absolute));
                        txtPhone.Style = redTxtBoxHeaderStyle;
                        IsPhoneValid = false;
                    }

                    txtPhone.Header = txtMobileTemp;

                }

                else if (CountrySelected == "Canda")

                {

                    if (!String.IsNullOrWhiteSpace(txtPhone.Text) && txtPhone.Text.Length >= 10)

                    {

                        MobileimageValid.Source = new BitmapImage(new Uri(Validimageuri, UriKind.Absolute));
                        txtPhone.Style = BlueTxtBoxHeaderStyle;
                        IsPhoneValid = true;
                    }

                    else

                    {

                        MobileimageValid.Source = new BitmapImage(new Uri(Notvalidimageuri, UriKind.Absolute));
                        txtPhone.Style = redTxtBoxHeaderStyle;
                        IsPhoneValid = false;
                    }

                    txtPhone.Header = txtMobileTemp;

                }

                else if (CountrySelected == "other")

                {

                    if (!String.IsNullOrWhiteSpace(txtPhone.Text))

                    {

                        MobileimageValid.Source = new BitmapImage(new Uri(Validimageuri, UriKind.Absolute));
                        txtPhone.Style = BlueTxtBoxHeaderStyle;
                        IsPhoneValid = true;
                    }

                    else

                    {

                        MobileimageValid.Source = new BitmapImage(new Uri(Notvalidimageuri, UriKind.Absolute));
                        txtPhone.Style = redTxtBoxHeaderStyle;
                        IsPhoneValid = false;
                    }

                    txtPhone.Header = txtMobileTemp;

                }

                else

                {

                    MessageDialog msg = new MessageDialog("please select Country first ");

                    await msg.ShowAsync();

                }



            }

        }
        
        private void CountriesList_SelectionChanged(object sender, SelectionChangedEventArgs e)

        {

            CountriesList.Header = CountriesList.PlaceholderText;

            var selectedCountry = CountriesList.SelectedItem as ComboBoxItem;

            CountrySelected = selectedCountry.Content.ToString();

        }

        private void txtCode_GotFocus(object sender, RoutedEventArgs e)
        {
            txtCode.Style = BlueTxtBoxHeaderStyle;

            if (txtCode.PlaceholderText != " ")
            {

                txtCode.Header = txtCode.PlaceholderText;
                TxtCodeTemp = txtCode.PlaceholderText;
                txtCode.PlaceholderText = " ";
            }
            else
            {
                txtCode.Header = TxtCodeTemp;
            }

        }



        private void txtCode_LostFocus(object sender, RoutedEventArgs e)

        {
            txtCode.Style = BlueTxtBoxHeaderStyle;
            if (String.IsNullOrWhiteSpace(txtCode.Text.ToString()))

            {
                txtCode.PlaceholderText = txtCode.Header.ToString();
                txtCode.Header = " ";

            }

            else

            {

                txtCode.Header = TxtCodeTemp;

            }

        }

        private void newsToggleSwitch_Toggled(object sender, RoutedEventArgs e)

        {

            newsToggleSwitch.OnContent = "";

        }

        #endregion
        #region ProfileInfo
        private void FNtxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            FNtxtBox.Style = BlueTxtBoxHeaderStyle;
            FNimageValid.Source = null;

            if (FNtxtBox.PlaceholderText != " ")
            {
                FNtxtBox.Header = FNtxtBox.PlaceholderText;
                txtFnTemp = FNtxtBox.PlaceholderText;
                FNtxtBox.PlaceholderText = " ";
            }
            else
            {
                FNtxtBox.Header = txtFnTemp;
            }
        }
        private void LNtxtBox_GotFocus(object sender, RoutedEventArgs e)
        {
            LNtxtBox.Style = BlueTxtBoxHeaderStyle;
            LNimageValid.Source = null;

            if (LNtxtBox.PlaceholderText != " ")
            {
                LNtxtBox.Header = LNtxtBox.PlaceholderText;
                txtLnTemp = LNtxtBox.PlaceholderText;
                LNtxtBox.PlaceholderText = " ";
            }
            else
            {
                LNtxtBox.Header = txtLnTemp;
            }
        }
        private void FNtxtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(FNtxtBox.Text.ToString()))
            {
                FNtxtBox.PlaceholderText = FNtxtBox.Header.ToString();
                FNtxtBox.Header = " ";
                FNimageValid.Source = null;

            }
            else
            {
                    FNimageValid.Source = new BitmapImage(new Uri(Validimageuri, UriKind.RelativeOrAbsolute));
                    FNtxtBox.Header = txtFnTemp;
                    FNtxtBox.Style = BlueTxtBoxHeaderStyle;
                    IsFnValid = true;
            }

        }
        private void LNtxtBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(LNtxtBox.Text.ToString()))
            {
                LNtxtBox.PlaceholderText = LNtxtBox.Header.ToString();
                LNtxtBox.Header = " ";
                LNimageValid.Source = null;
            }
            else
            {
                LNimageValid.Source = new BitmapImage(new Uri(Validimageuri, UriKind.RelativeOrAbsolute));
                LNtxtBox.Header = txtLnTemp;
                LNtxtBox.Style = BlueTxtBoxHeaderStyle;
                IsLnValid = true;
            }

        }

        void HandleProfilePicDescription()
        {
            PicDes.Visibility = Visibility.Collapsed;
            EditPicDes.Visibility = Visibility.Visible;
        }
        private void Rectangle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
        private void TakeProfilePhoto_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }
        private async void TakePicture_Click(object sender, RoutedEventArgs e)
        {
            _capture_ui.PhotoSettings.AllowCropping = true;
            _capture_ui.PhotoSettings.MaxResolution = CameraCaptureUIMaxPhotoResolution.HighestAvailable;
            var captured_file = await _capture_ui.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (captured_file != null)
            {
                var stream = await captured_file.OpenReadAsync();
                //display the image
                var image = new BitmapImage();
                image.SetSource(stream);
                myprofileImage.ImageSource = image;
                HandleProfilePicDescription();
            }
            else
            {
                //messageDialog
            }

        }
        private async void PickPicture_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker opener = new FileOpenPicker();
            opener.ViewMode = PickerViewMode.Thumbnail;
            opener.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            opener.CommitButtonText = "Select Picture";
            opener.FileTypeFilter.Add(".png");
            opener.FileTypeFilter.Add(".jpg");
            var selected_file = await opener.PickSingleFileAsync();
            if (selected_file != null)
            {
                var stream = await selected_file.OpenReadAsync();
                //display the image
                var image = new BitmapImage();
                image.SetSource(stream);
                myprofileImage.ImageSource = image;
                HandleProfilePicDescription();
                using (var inputStream = await selected_file.OpenSequentialReadAsync())
                {
                    var readStream = inputStream.AsStreamForRead();
                    buffer = new byte[readStream.Length];
                    await readStream.ReadAsync(buffer, 0, buffer.Length);
                }
            }
            else
            {
                //messageDialog
            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Gen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Gen.Header = Gen.PlaceholderText;
            var res = Gen.SelectedItem as ComboBoxItem;
            Gender = res.Content.ToString();
            GenimageValid.Source = new BitmapImage(new Uri(Validimageuri, UriKind.RelativeOrAbsolute));
            IsGenderValid = true;
        }
        private void _year_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _year.Header = _year.PlaceholderText;
            birthYear = _year.SelectedItem.ToString();
            if(SelectedbirthYear!=null)
            SelectedbirthYear = new DateTime(Int32.Parse(birthYear),1,1);
            YearimageValid.Source = new BitmapImage(new Uri(Validimageuri, UriKind.RelativeOrAbsolute));
            IsDobValid = true;
        }

        #endregion
        #region DriverStackInfo
        private void listCarBrands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listCarBrands.Header = listCarBrands.PlaceholderText;
        }

        private void txtModel_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtModel.PlaceholderText != " ")
            {
                txtModel.Header = txtModel.PlaceholderText;
                txtModelTemp = txtModel.PlaceholderText;
                txtModel.PlaceholderText = " ";
            }
            else
            {
                txtModel.Header = txtModelTemp;

            }

        }

        private void txtModel_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtModel.Text.ToString()))
            {
                txtModel.PlaceholderText = txtModel.Header.ToString();
                txtModel.Header = " ";
                carModelimageValid.Source = null;
            }
            else
            {
                txtModel.Header = txtModelTemp;
                carModelimageValid.Source = new BitmapImage(new Uri(Validimageuri, UriKind.Absolute));
            }
        }

        private void txtCarNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtCarNumber.PlaceholderText != " ")
            {
                txtCarNumber.Header = txtCarNumber.PlaceholderText;
                txtPlateTemp = txtCarNumber.PlaceholderText;
                txtCarNumber.PlaceholderText = " ";
            }
            else
            {
                txtCarNumber.Header = txtPlateTemp;
            }
        }
        private void txtCarNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtCarNumber.Text.ToString()))
            {
                txtCarNumber.PlaceholderText = txtCarNumber.Header.ToString();
                txtCarNumber.Header = " ";
                carplateimageValid.Source = null;
            }
            else
            {
                txtCarNumber.Header = txtPlateTemp;
                carplateimageValid.Source = new BitmapImage(new Uri(Validimageuri, UriKind.Absolute));
            }
         }
        private void IsBusinessCar_Toggled(object sender, RoutedEventArgs e)
        {

        }
        private void colorList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = colorList.SelectedItem as GridViewItem;
            if (selectedItem != null)
            {
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
                        color.Opacity =1;
                    }
                }

            }
            colorimageValid.Source = new BitmapImage(new Uri(Validimageuri, UriKind.Absolute));
        }


        #endregion
        #region Addresses
        public static async Task GetAllPredectionsFromGoogle(string searchterm)
        {
            // Assemble the URL
            string url = String.Format("https://maps.googleapis.com/maps/api/place/autocomplete/json?input={0}&types=address&language=fr&key={1}", searchterm, ApiKey);
            // Call out to Google Api
            HttpClient http = new HttpClient();
            var response = await http.GetAsync(url);
            var jsonMessage = await response.Content.ReadAsStringAsync();
            // Response -> string / json -> deserialize
            var serializer = new DataContractJsonSerializer(typeof(PredictionRoot));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonMessage));
            var result = (PredictionRoot)serializer.ReadObject(ms);

            // Filter List of Places 
            var allPlaces = result.predictions;
            foreach (var place in allPlaces)
            {
                if (place.description != null)
                    App.OuiApp.GAddress.Add(place);
            }
        }
        public async static Task<List<Group>> GetGroupsData()
        {
            var http = new HttpClient();
            var response = await http.GetAsync("http://213.246.61.49/weel-web/api/group/getWithLocation?country=FR");
            if (response.IsSuccessStatusCode == true)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data = (JsonConvert.DeserializeObject<List<Group>>(result)).ToList();
                return data;
            }
            else
            {
                return null;
            }
        }

        private void HomeSearchBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }


        private void HomeSearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(HomeSearchBox.Text))
            {
                HomeimageValid.Source = new BitmapImage(new Uri(Validimageuri, UriKind.Absolute));
            }
            else
            {
                HomeimageValid.Source = new BitmapImage(new Uri(Notvalidimageuri, UriKind.Absolute));
            }
        }

        private void CompanySearchBox_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void CompanySearchBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(CompanySearchBox.Text))
            {
                CompanyimageValid.Source = new BitmapImage(new Uri(Validimageuri, UriKind.Absolute));
            }
            else
            {
                CompanyimageValid.Source = new BitmapImage(new Uri(Notvalidimageuri, UriKind.Absolute));
            }
        }

        private async void OuiCompanies_GotFocus(object sender, RoutedEventArgs e)
        {
            try {
                var results = await GetGroupsData();
                if (results != null)
                {
                    if (App.OuiApp.Groups.Count > 0)
                        App.OuiApp.Groups.Clear();

                    foreach (var Group in results)
                    {
                        if (Group.groupName != null)
                            App.OuiApp.Groups.Add(Group);
                    }

                    App.OuiApp.Groups.OrderBy(i => i.groupName).ToList();
                }
                else
                {
                    MessageDialog msg = new MessageDialog("Error");
                    await msg.ShowAsync();
                    OuiCompanies.LostFocus += OuiCompanies_LostFocus;
                }
            }
            catch (Exception)
            {

            }
            
        }

        private void OuiCompanies_LostFocus(object sender, RoutedEventArgs e)
        {
        //    OuiCompanies.Header = OuiCompanies.PlaceholderText;

            if (!String.IsNullOrWhiteSpace(OuiCompanies.Text))
            {
                ComapniesimageValid.Source = new BitmapImage(new Uri(Validimageuri, UriKind.Absolute));
            }
            else
            {
                ComapniesimageValid.Source = new BitmapImage(new Uri(Notvalidimageuri, UriKind.Absolute));
            }
        }
        private void HomeSearchBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                var SelectedClient = args.ChosenSuggestion as Prediction;
                HomeSearchBox.Text = SelectedClient.description.ToString();
                HomeSearchBox.LostFocus += HomeSearchBox_LostFocus;

            }
            else
            {
                HomeSearchBox.Text = sender.Text;
                HomeSearchBox.LostFocus += HomeSearchBox_LostFocus;
            }
        }

        private async void HomeSearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.CheckCurrent())
            {
                App.OuiApp.GAddress.Clear();
                var search_term = HomeSearchBox.Text.ToLower();
                if (search_term.Length >= 3)
                {
                    await GetAllPredectionsFromGoogle(search_term);
                    var results = App.OuiApp.GAddress
                        .Where(i => i.description.ToString().ToLower().Contains
                        (search_term.ToLower())).OrderBy(i => i.description).ToList();
                    HomeSearchBox.ItemsSource = results;
                }
            }

        }
        private void HomeSearchBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                var SelectedClient = args.SelectedItem as Prediction;
                HomeSearchBox.Text = SelectedClient.description;
                HomeAddress = true;
            }
        }
        private async void CompanySearchBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.CheckCurrent())
            {
                App.OuiApp.GAddress.Clear();
                var search_term = CompanySearchBox.Text.ToLower();
                if (search_term.Length >= 3)
                {
                    await GetAllPredectionsFromGoogle(search_term);
                    var results = App.OuiApp.GAddress
                        .Where(i => i.description.ToString().ToLower().Contains
                        (search_term.ToLower())).OrderBy(i => i.description).ToList();
                    CompanySearchBox.ItemsSource = results;
                }
            }

        }
        private void CompanySearchBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                var SelectedClient = args.SelectedItem as Prediction;
                CompanySearchBox.Text = SelectedClient.description;
                CompanySearchBox.IsSuggestionListOpen = false;
                WorkAddress = true;
            }

        }

        private void txtCarNumber_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void OuiCompanies_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.CheckCurrent())
            {
                var search_term = OuiCompanies.Text.ToLower();
                var results = App.OuiApp.Groups
                        .Where(i => i.groupName.ToString().ToLower().Contains
                        (search_term.ToLower())).OrderBy(i => i.groupName).ToList();
                OuiCompanies.ItemsSource = results;
            }
        }

        private void OuiCompanies_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args.SelectedItem != null)
            {
                var SelectedClient = args.SelectedItem as Group;
                OuiCompanies.Text = SelectedClient.groupName;
                OuiCompanies.IsSuggestionListOpen = false;
            }
        }
        private void Fill_Click(object sender, RoutedEventArgs e)
        {
            Addresscontrol.IsOpen=false;
        }

        private void Pass_Click(object sender, RoutedEventArgs e)
        {
            Addresscontrol.IsOpen = false;
            fourthTitle.Visibility = Visibility.Visible;
            thirdTitle.Visibility = Visibility.Collapsed;
            Address.Visibility = Visibility.Collapsed;
            DriverStack.Visibility = Visibility.Visible;
            Dline3.Visibility = Visibility.Collapsed;
            Dline4.Visibility = Visibility.Visible;
        }

        #endregion

        bool validateBasciuserInfo()
        {
            if (IsMailvalid && IsPassValid && IsPhoneValid)
                return true;
            else
                return false;
        }

        private void GroupImageLoaded(object sender, RoutedEventArgs e)
        {
            var control_image = sender as Image;
            var customer_info = control_image.DataContext as Group;
            if (customer_info != null)
            {
                if (customer_info.imageChecksum != null)
                {
                    BitmapImage image = new BitmapImage();
                    control_image.Source = ImageHelper.base64image(customer_info.imageChecksum);
                }
            }
        }

      
        bool validateProfileuserInfo()
        {
            if (IsDobValid && IsFnValid && IsLnValid && IsGenderValid)
                return true;
            else
            {
                if (String.IsNullOrEmpty(FNtxtBox.Text))
                {
                    FNimageValid.Source = new BitmapImage(new Uri(Notvalidimageuri, UriKind.RelativeOrAbsolute));
                }
                if (String.IsNullOrEmpty(LNtxtBox.Text))
                {
                    LNimageValid.Source = new BitmapImage(new Uri(Notvalidimageuri, UriKind.RelativeOrAbsolute));
                }
                if (Gender == " ")
                {
                    GenimageValid.Source= new BitmapImage(new Uri(Notvalidimageuri, UriKind.RelativeOrAbsolute));
                }
                if(birthYear==" ")
                {
                    YearimageValid.Source= new BitmapImage(new Uri(Notvalidimageuri, UriKind.RelativeOrAbsolute));
                }
                return false;
            }
        }
        bool validateAddresses()
        {
            if (WorkAddress && HomeAddress && !String.IsNullOrEmpty(HomeSearchBox.Text) && !String.IsNullOrEmpty(CompanySearchBox.Text))
                return true;
            else
                return false;
        }

        private async void NavigationButton_Click(object sender, RoutedEventArgs e)
        {
            if (BasicInfo.Visibility == Visibility.Visible)
            {
                if (App.OuiApp.userMode == "DRIVER")
                {
                    Dline1.Visibility = Visibility.Visible;
                    if (validateBasciuserInfo())
                    {
                        BasicInfo.Visibility = Visibility.Collapsed;
                        Dline2.Visibility = Visibility.Visible;
                        Dline1.Visibility = Visibility.Collapsed;
                        ProfileInfo.Visibility = Visibility.Visible;
                        firstTitle.Visibility = Visibility.Collapsed;
                        secondTitle.Visibility = Visibility.Visible;

                    }
                    else
                    {

                    }
                }
                else
                {
                    Pline1.Visibility = Visibility.Visible;

                    if (validateBasciuserInfo())
                    {
                        BasicInfo.Visibility = Visibility.Collapsed;
                        Pline2.Visibility = Visibility.Visible;
                        Pline1.Visibility = Visibility.Collapsed;
                        ProfileInfo.Visibility = Visibility.Visible;
                        firstTitle.Visibility = Visibility.Collapsed;
                        secondTitle.Visibility = Visibility.Visible;
                    }
                    else
                    {

                    }
                }
            }
            else if (ProfileInfo.Visibility == Visibility.Visible)
            {
                if (App.OuiApp.userMode == "DRIVER")
                {
                    if (validateProfileuserInfo())
                    {
                        ProfileInfo.Visibility = Visibility.Collapsed;
                        Dline2.Visibility = Visibility.Collapsed;
                        Dline3.Visibility = Visibility.Visible;
                        Address.Visibility = Visibility.Visible;
                        thirdTitle.Visibility = Visibility.Visible;
                        secondTitle.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                    }
                }
                else
                {
                    if (validateProfileuserInfo())
                    {
                        thirdTitle.Visibility = Visibility.Visible;
                        secondTitle.Visibility = Visibility.Collapsed;
                        ProfileInfo.Visibility = Visibility.Collapsed;
                        Pline2.Visibility = Visibility.Collapsed;
                        Pline3.Visibility = Visibility.Visible;
                        Address.Visibility = Visibility.Visible;
                    }
                    else
                    {
                    }
                }
            }
            else if (Address.Visibility == Visibility.Visible)
            {
                //  string Message = ValidationManager.ValidateAddresses(OuiCompanies.Text, HomeSearchBox.Text, CompanySearchBox.Text);
                if (App.OuiApp.userMode == "DRIVER")
                { 
                     if (validateAddresses())
                        {
                            fourthTitle.Visibility = Visibility.Visible;
                            thirdTitle.Visibility = Visibility.Collapsed;
                            Address.Visibility = Visibility.Collapsed;
                            DriverStack.Visibility = Visibility.Visible;
                            Dline3.Visibility = Visibility.Collapsed;
                            Dline4.Visibility = Visibility.Visible;
                        }
                            else
                        Addresscontrol.IsOpen = true;
                }
                else
                {
                    if (validateAddresses())
                    {
                        fourthTitle.Visibility = Visibility.Visible;
                        thirdTitle.Visibility = Visibility.Collapsed;
                        Address.Visibility = Visibility.Collapsed;
                        DriverStack.Visibility = Visibility.Visible;
                        Dline3.Visibility = Visibility.Collapsed;
                        Dline4.Visibility = Visibility.Visible;
                    }
                    else
                        Addresscontrol.IsOpen = true;

                }
            }

            else
            {
                SendSignUpData();
            }

           
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (BasicInfo.Visibility == Visibility.Visible)
            {
                Frame.Navigate(typeof(SignInPage));
            }
            else if (ProfileInfo.Visibility == Visibility.Visible)
            {
                if (App.OuiApp.userMode == "DRIVER")
                {
                    Dline1.Visibility = Visibility.Visible;
                    BasicInfo.Visibility = Visibility.Visible;
                    Dline2.Visibility = Visibility.Collapsed;
                    ProfileInfo.Visibility = Visibility.Collapsed;
                    firstTitle.Visibility = Visibility.Visible;
                    secondTitle.Visibility = Visibility.Collapsed;
                }
                else
                {
                    Pline1.Visibility = Visibility.Visible;
                    BasicInfo.Visibility = Visibility.Visible;
                    Pline2.Visibility = Visibility.Collapsed;
                    ProfileInfo.Visibility = Visibility.Collapsed;
                    firstTitle.Visibility = Visibility.Visible;
                    secondTitle.Visibility = Visibility.Collapsed;
                }
            }
            else if (Address.Visibility == Visibility.Visible)
            {
                if (App.OuiApp.userMode == "DRIVER")
                {
                    secondTitle.Visibility = Visibility.Visible;
                    thirdTitle.Visibility = Visibility.Collapsed;
                    Dline2.Visibility = Visibility.Visible;
                    ProfileInfo.Visibility = Visibility.Visible;
                    Dline3.Visibility = Visibility.Collapsed;
                    Address.Visibility = Visibility.Collapsed;
                }
                else
                {
                    secondTitle.Visibility = Visibility.Visible;
                    thirdTitle.Visibility = Visibility.Collapsed;
                    Pline2.Visibility = Visibility.Visible;
                    ProfileInfo.Visibility = Visibility.Visible;
                    Pline3.Visibility = Visibility.Collapsed;
                    Address.Visibility = Visibility.Collapsed;
                }
            }
            else if ( DriverStack.Visibility == Visibility.Visible)
            {
                thirdTitle.Visibility = Visibility.Visible;
                fourthTitle.Visibility = Visibility.Collapsed;
                Dline3.Visibility = Visibility.Visible;
                Address.Visibility = Visibility.Visible;
                Dline4.Visibility = Visibility.Collapsed;
                DriverStack.Visibility = Visibility.Collapsed;
            }

        }
        private async void SendSignUpData()
        {
            HttpClient client = null;
            client = new HttpClient();
            #region Build User Object
            User.email = txtEmail.Text.ToString();
            User.firstName = FNtxtBox.Text.ToString();
            User.lastName = LNtxtBox.Text.ToString();
            User.password = txtPassword.Password.ToString();
            User.mobileNo = txtPhone.Text.ToString();
            User.gender = Gender;
            User.birthDate = SelectedbirthYear;
            if (CountrySelected == "France")
                User.country = "FR";
            else if (CountrySelected == "canda")
                User.country = "CA";
            else
                User.country = "";
            User.mode = App.OuiApp.userMode.ToString();
            User.accountType = "LOCAL";//Local or External
            User.newsLetter = "true";// true or False
            User.externalId = null;
            User.accessToken = null;
            if (buffer != null)
                User.Image = buffer;
            else
                User.Image = null;
            User.carMake = listCarBrands.SelectedItem.ToString();
            User.carModel = txtModel.Text.ToString();
            User.carColor = "Blue";// get color 
            User.carPlateNumber = txtCarNumber.Text.ToString();
            User.businessCar = "false";
            User.groupId = "2";
            User.hwLineIds = "1,2,3";
            User.whLineIds = "3,1,2";
            #endregion

            if (User == null) return;
            var SerializeJson = JsonConvert.SerializeObject(User);

            #region Build Signup Request 
                var request = new HttpRequestMessage();
                request.RequestUri = new Uri("http://213.246.61.49/weel-web/api/auth/fullSignup");
                request.Headers.Add("Api-Version", "1.0");
                request.Headers.Add("App-Version", "0.0.1");
                string UniqueDeviceID = DeviceInfo.Instance.Id;
                request.Headers.Add("Device-UniqueId", UniqueDeviceID);
                request.Headers.Add("Timezone-Format", "UTC");
                request.Headers.Add("User-Agent", "Windows");
                if (CountrySelected.ToString() == "France")
                    request.Headers.Add("User-Lang", "FR");
                else if (CountrySelected.ToString() == "Canda")
                    request.Headers.Add("User-Lang", "CA");
                request.Headers.Add("Auth-Token", "");
                request.Method = HttpMethod.Post;
                request.Content = new StringContent(SerializeJson.ToString(),Encoding.UTF8, "application/json");
            #endregion
            //send request 
            var response = await client.SendAsync(request);
            string result = await response.Content.ReadAsStringAsync();

              var Deserializedresult = JsonConvert.DeserializeObject<ExceptionClassManager>(result);
            if (response.StatusCode.Equals("Ok"))
            {
                MessageDialog msg = new MessageDialog("Sign Up Sucseed");
                await msg.ShowAsync();
            }

            else if (CountrySelected == "France")
            {
                MessageDialog error = new MessageDialog(Deserializedresult.userMessage);
                await error.ShowAsync();
            }
            else
            {
                MessageDialog error = new MessageDialog(Deserializedresult.exceptionMessage);
                await error.ShowAsync();
            }
        }

    }
}
