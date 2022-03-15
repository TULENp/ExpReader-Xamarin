using ExpReader.Services;
using ExpReader.Services.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                case 1: SwitchTheme.IsToggled = false;
                    break;
                case 2: SwitchTheme.IsToggled = true;
                    break;
            }
        }

        private void SwitchTheme_Toggled(object sender, ToggledEventArgs e)
        {
            switch(e.Value)
            {
                case false: Settings.Theme = 1;
                    break;
                case true: Settings.Theme = 2;
                    break;
            }
            TheTheme.SetTheme();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}