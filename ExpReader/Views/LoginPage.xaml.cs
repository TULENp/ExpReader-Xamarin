using ExpReader.AppSettings;
using ExpReader.Services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpReader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
            //Shell.SetTabBarIsVisible(this, false);
        }

     

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                // int userid = DBService.GetUserId(LogEntry.Text, PasEntry.Text).Result;
                int userid = 1; 
                string userstats = DBService.GetUserStats(userid);
                Settings.userStats = userstats;
                Preferences.Set("TempUserId", userid);
                await Shell.Current.GoToAsync("//Main");
            } catch { }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
        }
    }
}