using ExpReader.Services;
using ExpReader.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpReader.AppSettings;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpReader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LibPage : ContentPage
    {
        
        public LibPage()
        {
            InitializeComponent();
            Shell.SetNavBarIsVisible(this, false);
            HidePanelSort();
            
        }

        private void HidePanelSort()
        {
            PanelSort.Scale = 0;
            PanelSort.IsVisible = false;

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
                PanelSort.ScaleTo(0, 250, Easing.CubicIn);
                await Task.Delay(250);
                PanelSort.IsVisible = false;
            }
            else
            {
                if(Settings.Theme==1)
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
                PanelSort.IsVisible = true;
                PanelSort.ScaleTo(1, 250, Easing.CubicOut);
            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            TapGestureRecognizer_Tapped(sender,e);
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            var _container = BindingContext as LibVM;
            var sortedBooks = _container.Books.OrderBy(x => x.Title).ToList();

            for (var i = 0; i < sortedBooks.Count; i++)
                _container.Books.Move(_container.Books.IndexOf(sortedBooks[i]), i);
            TapGestureRecognizer_Tapped(sender, e);
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            var _container = BindingContext as LibVM;
            var sortedBooks = _container.Books.OrderByDescending(x => x.Title).ToList();

            for (var i = 0; i < sortedBooks.Count; i++)
                _container.Books.Move(_container.Books.IndexOf(sortedBooks[i]), i);
            TapGestureRecognizer_Tapped(sender, e);
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var _container = BindingContext as LibVM;

            if (string.IsNullOrWhiteSpace(e.NewTextValue))
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