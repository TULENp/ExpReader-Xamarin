using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace ExpReader.Services
{
   public static class Settings
    {
        const  int theme = 1;
        public static int Theme
        {
            get => Preferences.Get(nameof(Theme), theme);
            set => Preferences.Set(nameof(Theme), value);
        }
        
    }
}
