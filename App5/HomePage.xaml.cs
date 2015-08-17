using HangMan;
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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App5
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
        }
        private async void hideStatusBar()
        {
            await Windows.UI.ViewManagement.StatusBar.GetForCurrentView().HideAsync();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            pick.ChildTransitions = new TransitionCollection { new PaneThemeTransition { Edge = EdgeTransitionLocation.Bottom } };
            hideStatusBar();
            if(e.Parameter==null)
                show1.Begin();
            else
            {
                high.Text += App.highScore;
                Play.Opacity = 1;
                high.Opacity = 1;
                load.Opacity = 0;
                progress.Opacity = 0;
                title.Opacity = 1;
                imageBlock.Opacity = 1;
                Play.IsEnabled = true;
            }
            Play.Transitions = new TransitionCollection { new PaneThemeTransition { Edge = EdgeTransitionLocation.Bottom } };
        }
        Frame rootF = Window.Current.Content as Frame;
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            pick.IsOpen = true;
        }
        private void help_Click(object sender, RoutedEventArgs e)
        {
            rootF.Navigate(typeof(HelpPage));
        }

        private void show1_Completed(object sender, object e)
        {
            show2.Begin();
        }

        private void show2_Completed(object sender, object e)
        {
            show4.Begin();
        }

        private void show4_Completed(object sender, object e)
        {
            load.Opacity = 0;
            progress.Opacity = 0;
            show3.Begin();
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            rootF.Navigate(typeof(aboutPage));
        }

        private void show3_Completed(object sender, object e)
        {
            high.Text += App.highScore;
            high.Opacity = 1;
            Play.IsEnabled = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainPage.selectedInd = categ.SelectedIndex;
            rootF.Navigate(typeof(MainPage),0);
        }
    }
}
