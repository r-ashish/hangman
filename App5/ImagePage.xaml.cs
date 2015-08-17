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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace HangMan
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ImagePage : Page
    {
        public ImagePage()
        {
            this.InitializeComponent();
            tb1.Text = App5.MainPage.display()[0];
            tb2.Text = App5.MainPage.display()[1];
            tb3.Text = App5.MainPage.display()[2];
            tb4.Text = App5.MainPage.display()[3];
            tb5.Text = App5.MainPage.display()[4];
            tb6.Text = App5.MainPage.display()[5];
            tb7.Text = App5.MainPage.display()[6];
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += Hardwarebuttons_BackPressed_Im;
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= Hardwarebuttons_BackPressed_Im;
        }
        private void Hardwarebuttons_BackPressed_Im(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            if (rootF == null) return;
            if (rootF.CanGoBack)
            {
                rootF.GoBack();
                e.Handled = true;
            }
            else
            {
                rootF.Navigate(typeof(App5.HelpPage));
                e.Handled = true;
            }
        }
        Frame rootF = Window.Current.Content as Frame;
        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            rootF.Navigate(typeof(App5.MainPage));
        }
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            rootF.Navigate(typeof(App5.HelpPage));
        }
    }
}
