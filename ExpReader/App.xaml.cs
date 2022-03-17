using ExpReader.Services;
using ExpReader.Services.Themes;
using ExpReader.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ExpReader
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            TheTheme.SetTheme();
            //DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();

        }

        protected override void OnStart()
        {
            OnResume();
            try
            {
                DBService.UpdateDb();
            }
            catch { }
        }

        protected override void OnSleep()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged -= App_RequestedThemeChanged;
            try
            {
                DBService.UpdateDb();
            } catch { }
        }

        protected override void OnResume()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged += App_RequestedThemeChanged;
        }
        

        private void App_RequestedThemeChanged(object sender, AppThemeChangedEventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                TheTheme.SetTheme();
            });
        }
    }
}
