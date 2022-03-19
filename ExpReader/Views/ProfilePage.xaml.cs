using ExpReader.ViewModels;
using Newtonsoft.Json;
using ExpReader.Services;
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
    public partial class ProfilePage : ContentPage
    {
        ProfileVM profileVM;
        public ProfilePage()
        {
            InitializeComponent();
            profileVM = new ProfileVM();

            Shell.SetNavBarIsVisible(this, false);
            switch(Settings.Theme)
            {
                case 1: Prize.Source = ImageSource.FromFile("PrizeBlue.png");
                    break;
                case 2: Prize.Source = ImageSource.FromFile("PrizeBlueLig.png");
                    break;
            }

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            profileVM.UpdateUserStats();
        }
    }
}