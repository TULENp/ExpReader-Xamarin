using ExpReader.Models;
using ExpReader.ViewModels;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static ExpReader.ViewModels.ReaderVM;

namespace ExpReader.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReaderPage : ContentPage
    {
        public ReaderPage(Book book)
        {
            InitializeComponent();
            BindingContext = new ReaderVM(book);
            Shell.SetNavBarIsVisible(this, false);
            Shell.SetTabBarIsVisible(this, false);
            //PdfJsWebView.Uri = Path.Combine(FileSystem.AppDataDirectory, book.Path);
        }
        private void ScrollToTop(object sender, System.EventArgs e)
        {
            scroll.ScrollToAsync(0, 0, false);
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            HideSettingsPanel();
        }

        private async void HideSettingsPanel()
        {
            SettingsPanel.TranslationY = -80;
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            SettingsPanel.TranslateTo(0, 0, 250, Easing.CubicInOut);
        }

        private void TapGestureRecognizer_Tapped_1(object sender, System.EventArgs e)
        {
            //SettingsPanel.TranslateTo(0, -80, 250, Easing.CubicInOut);
            if((imageBack.IsVisible==true) && (imageGear.IsVisible==true))
            {
                imageBack.TranslateTo(0, -80, 250, Easing.CubicIn);
                imageGear.TranslateTo(0, -80, 250, Easing.CubicIn);
                Task.Delay(250);
                imageBack.IsVisible = false;
                Task.Delay(250);
                imageGear.IsVisible = false;
            }
            else
            {
                imageBack.TranslateTo(0, 0, 250, Easing.CubicInOut);
                imageBack.IsVisible = true;
                imageGear.TranslateTo(0, 0, 250, Easing.CubicInOut);
                imageGear.IsVisible = true;
            }
        }
    }
}