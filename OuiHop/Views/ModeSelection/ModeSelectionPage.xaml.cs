using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace OuiHop.Views.ModeSelection
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ModeSelectionPage : Page
    {
        public ModeSelectionPage()
        {
            this.InitializeComponent();
        }
       
        private void TermsLink_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(WebPage.Web));
        }

        private void User_Tapped(object sender, TappedRoutedEventArgs e)
        {

            if (App.OuiApp != null)
            {
                if (App.OuiApp.userMode == null)
                    App.OuiApp.userMode = "Pedestarian";
                else
                    App.OuiApp.userMode = "Pedestarian";
            }
            Frame.Navigate(typeof(OuiHop.Views.GetStartedPedestarian.GetStarted));

        }

        private void Driver_Tapped_1(object sender, TappedRoutedEventArgs e)
        {
            if (App.OuiApp != null)
            {
                if (App.OuiApp.userMode == null)
                    App.OuiApp.userMode = "Driver";
                else
                    App.OuiApp.userMode = "Driver";
            }
            Frame.Navigate(typeof(OuiHop.Views.GetStartedDriver.GetStarted));
        }
    }
}
