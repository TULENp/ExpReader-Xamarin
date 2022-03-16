using ExpReader.Services;
using ExpReader.Services.Themes;
using ExpReader.UserStats.DailyTasks;
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
        }

        protected override void OnSleep()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged -= App_RequestedThemeChanged;

        }

        protected override void OnResume()
        {
            TheTheme.SetTheme();
            RequestedThemeChanged += App_RequestedThemeChanged;

            //daily task
            if (DateTime.Today > Preferences.Get("Date", DateTime.MinValue))
            {
                //reset TodayReadPages
                Preferences.Set(nameof(DailyTask.TodayReadPages), 0);
            }
            Preferences.Set("Date", DateTime.Today);
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
