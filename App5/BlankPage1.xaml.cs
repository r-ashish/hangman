using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace App5
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BlankPage1 : Page
    {
        public BlankPage1()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        /// 
        private async void write()
        {
            if (int.Parse(App.highScore) < MainPage.score)
            {
                App.highScore = MainPage.score.ToString();
                var folder = await Windows.Storage.ApplicationData.Current.LocalFolder.GetFolderAsync("Data");
                var file = await folder.CreateFileAsync("highscore.txt", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(file, App.highScore);
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string s = e.Parameter.ToString();
            MainPage.score += 6 - MainPage.wrong;
            write();
            if (s == "win")
            {
                action.Content = "Continue";
                result.Text = "You Win";
                lText.Text = "\nCurrent Score : " + MainPage.score + "\nHigh Score : " + App.highScore;
            }
            else
            {
                lText.Text = "\nThe word is : " + MainPage.sLose() + "\nYour Score : " + MainPage.score.ToString() + "\nHigh Score : " + App.highScore;
                MainPage.score = 0;
            }
        }
        Frame rootFram = Window.Current.Content as Frame;
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            rootFram.Navigate(typeof(MainPage),MainPage.score);
            rootFram.BackStack.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            write();
            rootFram.Navigate(typeof(HomePage), "a");
        }
    }
}
