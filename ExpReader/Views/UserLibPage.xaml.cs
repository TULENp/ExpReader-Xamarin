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
        
        public UserLibPage()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
            //Shell.SetTabBarIsVisible(this, true);
            HidePanelSort();
            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var e = DependencyService.Get<IEnvironment>();
            if (App.Current.RequestedTheme == OSAppTheme.Dark)
            {
                e?.SetStatusBarColor(Color.FromHex("#0091FF"), false);
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
             if(PanelSort.IsVisible)
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
                    TextSort.TextColor = Color.FromHex("0091FF");
                    ImageSort.Source = ImageSource.FromFile("SortIconBlueLig.png");
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

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _container = BindingContext as UserLibVM;
            //var searchTerm = e.NewTextValue;
            //if (string.IsNullOrWhiteSpace(searchTerm))
            //{
            //    searchTerm = string.Empty;
            //    _container.Books.ToList();
            //}

            //searchTerm = searchTerm.ToLowerInvariant();
            //var filteredItems = _container.Books.Where(i => i.Title.ToLowerInvariant().Contains(searchTerm)).ToList();

            //if(string.IsNullOrWhiteSpace(searchTerm))
            //{
            //    filteredItems = _container.Books.ToList();
            //}

            //foreach(var i in _container.Books.ToList())
            //{
            //    if (!filteredItems.Contains(i))
            //    {
            //        _container.Books.Remove(i);
            //    }
            //    else if(!_container.Books.Contains(i))
            //    {
            //        _container.Books.Add(i);
            //    }
            //}
            if(string.IsNullOrWhiteSpace(e.NewTextValue))
            {
                CollectionBooks.ItemsSource = _container.Books.ToList();
            }
            else
            {
                CollectionBooks.ItemsSource = _container.Books.Where(i => i.Title.ToLowerInvariant().Contains(e.NewTextValue)).ToList();
            }
        }
    }
}