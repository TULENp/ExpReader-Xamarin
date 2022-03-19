
using ExpReader.Services;
using ExpReader.DailyTasks;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using ExpReader.AppSettings.Themes;

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
        private void UpdateDb()
        {
            var stats = JsonConvert.DeserializeObject<List<string>>(Preferences.Get("BookStats", string.Empty));
            foreach (var statId in stats)
            {
                var stat = Preferences.Get(statId, string.Empty);
                //SetUserBook(stat);
            }
        }
    }
}
