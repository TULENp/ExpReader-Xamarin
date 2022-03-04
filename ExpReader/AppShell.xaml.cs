using ExpReader.ViewModels;
using ExpReader.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ExpReader
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
           Routing.RegisterRoute(nameof(ReadingPage), typeof(ReadingPage));
           Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
        }

    }
}
