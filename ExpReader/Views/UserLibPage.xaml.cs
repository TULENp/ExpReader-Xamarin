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
        
        public UserLibPage()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
            Shell.SetTabBarIsVisible(this, true);
            HidePanelSort();
            
        }

        private void HidePanelSort()
        {
           PanelSort.Scale = 0;
           PanelSort.IsVisible = false;
        }


        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //await Shell.Current.GoToAsync(nameof(SettingsPage));
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
             if(PanelSort.IsVisible)
             {
                
                 PanelSort.ScaleTo(0, 250, Easing.CubicIn);
                await Task.Delay(250);
                PanelSort.IsVisible = false;
             }
             else
             {
                
                PanelSort.IsVisible = true;
                PanelSort.ScaleTo(1, 250, Easing.CubicOut);
             }

        }

       
    }
}