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
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using System.Threading.Tasks;
using Windows.Graphics.Display;
using OuiHop.Common;
using OuiHop.Views.ModeSelection;
using OuiHop.Views.SignIn;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace OuiHop.Views.OuiSplash
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OuiSplashScreen : Page
    {
        internal Rect splashImageRect; // Rect to store splash screen image coordinates.
        internal bool dismissed = false; // Variable to track splash screen dismissal status.
        internal Frame rootFrame;
        private SplashScreen splash; // Variable to hold the splash screen object. 
        private double ScaleFactor; // Variable to hold the Device SacleFactor .
        public OuiSplashScreen(SplashScreen splashscreen, bool loadState)
        {
            InitializeComponent();
            DismissExtendedSplash();
            Window.Current.SizeChanged += Current_SizeChanged;
            ScaleFactor = (double)DisplayInformation.GetForCurrentView().ResolutionScale / 100;
            splash = splashscreen;
            if (splash != null)
            {
                // Register an event handler to be executed when the splash screen has been dismissed. 
                splash.Dismissed += Splash_Dismissed;
                // Retrieve the window coordinates of the splash screen image.
                splashImageRect = splash.ImageLocation;
                PositionImage();
            }
           // RestoreStateAsync(loadState);
        }

     async private void RestoreStateAsync(bool loadState)
        {
            await SuspensionManager.RestoreAsync();
        }

        void PositionImage()
        {
            extendedSplashImage.SetValue(Canvas.LeftProperty, splashImageRect.X);
            extendedSplashImage.SetValue(Canvas.TopProperty, splashImageRect.Y);
            extendedSplashImage.Height = splashImageRect.Height/ScaleFactor;
            extendedSplashImage.Width = splashImageRect.Width/ScaleFactor;
        }
        private void Splash_Dismissed(SplashScreen sender, object args)
        {
            dismissed = true;
        }

        private void Current_SizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            if (splash != null)
            {
                splashImageRect = splash.ImageLocation;
                PositionImage();
            }
        }

      async void DismissExtendedSplash()
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            rootFrame = new Frame();
            MainPage selectionPage = new MainPage();
            rootFrame.Content = selectionPage;
            Window.Current.Content = rootFrame;
            // Navigate to mainpage
            rootFrame.Navigate(typeof(MainPage));
        }

    }
}
