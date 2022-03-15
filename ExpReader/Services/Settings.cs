using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace ExpReader.Services
{
   public static class Settings
    {
        const  int theme = 1;
        const int readerTheme = 1;
        public static int Theme
        {
            get => Preferences.Get(nameof(Theme), theme);
            set => Preferences.Set(nameof(Theme), value);
        }

        public static int ReaderTheme
        {
            get => Preferences.Get(nameof(ReaderTheme), readerTheme);
            set => Preferences.Set(nameof(ReaderTheme), value);
        }
    }
}
