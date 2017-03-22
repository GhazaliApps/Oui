using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
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

namespace OuiHop.Views.MapView
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainMap : Page
    {
        public Geoposition CurrentLocation;
        Geolocator _geolocator;
        public MainMap()
        {
            this.InitializeComponent();
            _geolocator = new Geolocator();
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {

        }

        async private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            bool isInternetConnected = NetworkInterface.GetIsNetworkAvailable();
            if (isInternetConnected)
            {
                var accessStatus = await Geolocator.RequestAccessAsync();
                if (accessStatus != GeolocationAccessStatus.Allowed)
                {
                    MessageDialog NoInternet = new MessageDialog("No internet connection. please check your network connection and try again.");
                    await NoInternet.ShowAsync();
                }
                else
                {
                    CurrentLocation = await _geolocator.GetGeopositionAsync();
                }
            }
        }
    }
}
