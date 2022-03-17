using ExpReader.Services;
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
        UserLibVM userLibVM;
        public UserLibPage()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
            //Shell.SetTabBarIsVisible(this, true);
            HidePanelSort();
            userLibVM = new UserLibVM();
            BindingContext = userLibVM;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var e = DependencyService.Get<IEnvironment>();
            if (App.Current.RequestedTheme == OSAppTheme.Dark)
            {
                e?.SetStatusBarColor(Color.FromHex("#001EDE"), false);
            }
            else
            {
                e?.SetStatusBarColor(Color.FromHex("#2e74ff"), true);
            }
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
            if (PanelSort.IsVisible)
            {
                if (Settings.Theme == 1)
                {
                    TextSort.TextColor = Color.White;
                    ImageSort.Source = ImageSource.FromFile("SortIconWhite.png");
                }
                else
                {
                    TextSort.TextColor = Color.White;
                    ImageSort.Source = ImageSource.FromFile("SortIconWhite.png");
                }
                PanelSortBackGround.InputTransparent = true;
                FabButton.IsVisible = true;
                PanelSort.ScaleTo(0, 250, Easing.CubicIn);
                await Task.Delay(250);
                PanelSort.IsVisible = false;
            }
            else
            {
                if (Settings.Theme == 1)
                {
                    TextSort.TextColor = Color.Black;
                    ImageSort.Source = ImageSource.FromFile("SortIconBlack.png");
                }
                else
                {
                    TextSort.TextColor = Color.FromHex("001EDE");
                    ImageSort.Source = ImageSource.FromFile("SortIcon.png");
                }
                PanelSortBackGround.InputTransparent = false;
                FabButton.IsVisible = false;
                PanelSort.IsVisible = true;
                PanelSort.ScaleTo(1, 250, Easing.CubicOut);
            }

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            TapGestureRecognizer_Tapped(sender, e);
        }
    }
}