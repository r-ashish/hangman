using App5;
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
    public sealed partial class KnowPage : Page
    {
        public KnowPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += Hardwarebuttons_BackPressed_Kp;
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            Windows.Phone.UI.Input.HardwareButtons.BackPressed -= Hardwarebuttons_BackPressed_Kp;
        }
        private void Hardwarebuttons_BackPressed_Kp(object sender, Windows.Phone.UI.Input.BackPressedEventArgs e)
        {
            if (rootFra == null) return;
            if (rootFra.CanGoBack)
            {
                rootFra.GoBack();
                e.Handled = true;
            }
            else
            {
                rootFra.Navigate(typeof(App5.MainPage),"a");
                e.Handled = true;
            }
        }
        Frame rootFra = Window.Current.Content as Frame;
        private void TextBox_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            iKnow.Text = "";
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (iKnow.Text != "" && iKnow.Text != "I know this word")
            {
                if (iKnow.Text.ToLower().Trim() == App5.MainPage.quest)
                {
                    MainPage.score = MainPage.prevScore + MainPage.quest.Length;
                    rootFra.Navigate(typeof(App5.BlankPage1), "win");
                }
                else
                {
                    MainPage.score -= 6 - MainPage.wrong;
                    rootFra.Navigate(typeof(App5.BlankPage1), "lose");
                }
            }

        }

        private void iKnow_GotFocus(object sender, RoutedEventArgs e)
        {
            iKnow.Text = "";
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            rootFra.Navigate(typeof(App5.MainPage));
        }

        private void iKnow_LostFocus(object sender, RoutedEventArgs e)
        {
            if (iKnow.Text == "") iKnow.Text = "I know this word";
        }
    }
}
