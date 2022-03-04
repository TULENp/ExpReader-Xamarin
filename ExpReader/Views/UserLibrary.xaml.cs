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
    public partial class UserLibrary : ContentPage
    {
        private double _ProgressValue;
        public double ProgressValue
        {
            get
            {
                return _ProgressValue;
            }
            set
            {
                _ProgressValue = value;
                OnPropertyChanged();
            }
        }

        public UserLibrary()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
            
            ProgressValue = 0.3;
        }

        public void LOLKEK1()
        {
            
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //await Shell.Current.GoToAsync(nameof(SettingsPage));
        }
    }
}