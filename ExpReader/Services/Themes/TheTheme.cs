using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ExpReader.Services.Themes
{
   public static class TheTheme
    {
        public static void SetTheme()
        {
            switch(Settings.Theme)
            {
                case 1:App.Current.UserAppTheme = OSAppTheme.Light;
                    break;
                case 2:
                    App.Current.UserAppTheme = OSAppTheme.Dark;
                    break;
            }
            var e = DependencyService.Get<IEnvironment>();
            if(App.Current.RequestedTheme == OSAppTheme.Dark)
            {
                e?.SetStatusBarColor(Color.FromHex("#0091FF"),false);
            }
            else
            {
                e?.SetStatusBarColor(Color.FromHex("#2e74ff"), true);
            }
        }
    }
}
