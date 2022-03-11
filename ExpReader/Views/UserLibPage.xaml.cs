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
    public partial class UserLibPage : ContentPage
    {
        //private double _ProgressValue;
        //public double ProgressValue
        //{
        //    get
        //    {
        //        return _ProgressValue;
        //    }
        //    set
        //    {
        //        _ProgressValue = value;
        //        OnPropertyChanged();
        //    }
        //}

        public UserLibPage()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
            
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