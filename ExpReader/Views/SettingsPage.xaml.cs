using ExpReader.AppSettings;
using ExpReader.AppSettings.Themes;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpReader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
            switch (Settings.Theme)
            {
                case 1:
                    SwitchTheme.IsToggled = false;
                    break;
                case 2:
                    SwitchTheme.IsToggled = true;
                    break;
            }

            switch (Settings.DailyTask)
            {
                case 60:
                    min.IsChecked = true;
                    break;
                case 120:
                    mid.IsChecked = true;
                    break;
                case 240:
                    max.IsChecked = true;
                    break;
            }
        }

        private void SwitchTheme_Toggled(object sender, ToggledEventArgs e)
        {
            switch (e.Value)
            {
                case false:
                    Settings.Theme = 1;
                    break;
                case true:
                    Settings.Theme = 2;
                    break;
            }
            TheTheme.SetTheme();
        }


        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            Settings.DailyTask = 60;
        }

        private void RadioButton_CheckedChanged_1(object sender, CheckedChangedEventArgs e)
        {
            Settings.DailyTask = 120;
        }

        private void RadioButton_CheckedChanged_2(object sender, CheckedChangedEventArgs e)
        {
            Settings.DailyTask = 240;
        }
    }
}