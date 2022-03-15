using ExpReader.ViewModels;
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
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            profileVM.UpdateStats();
        }
    }
}