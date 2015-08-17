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
    public sealed partial class BlankPage3 : Page
    {
        public BLankPage3()
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
        }
        Frame rootFra = Window.Current.Content as Frame;
        private void TextBox_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            iKnow.Text = "";
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (iKnow.Text== App5.MainPage.quest)
                rootFra.Navigate(typeof(App5.BlankPage2));
            else
                rootFra.Navigate(typeof(App5.BlankPage1));
        }
    }
}
