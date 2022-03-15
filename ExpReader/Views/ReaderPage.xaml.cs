using ExpReader.Models;
using ExpReader.Services;
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
            MySlider.Value = System.Convert.ToDouble(Settings.ReaderSlider);
            ReaderText.FontSize = Settings.ReaderSlider;
            MySlider.ValueChanged += MySlider_ValueChanged;
            //PdfJsWebView.Uri = Path.Combine(FileSystem.AppDataDirectory, book.Path);
            switch (Settings.ReaderTheme)
            {
                case 1:
                    RadioWhiteTheme.IsChecked = true;
                    break;
                case 2:
                    RadioBeigeTheme.IsChecked = true;
                    break;
                case 3:
                    RadioBlackTheme.IsChecked = true;
                    break;
            }
        }
        
        private void MySlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            ReaderText.FontSize = System.Convert.ToInt32(MySlider.Value);
            Settings.ReaderSlider = System.Convert.ToInt32(MySlider.Value);
        }

        private void ScrollToTop(object sender, System.EventArgs e)
        {
            scroll.ScrollToAsync(0, 0, false);
        }

        private void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if(RadioWhiteTheme.IsChecked)
            {
                Settings.ReaderTheme = 1;
            }
            if (RadioBeigeTheme.IsChecked)
            {
                Settings.ReaderTheme = 2;
            }
            if (RadioBlackTheme.IsChecked)
            {
                Settings.ReaderTheme = 3;
            }
            SetReaderTheme();
            
        }

        private void SetReaderTheme()
        {   var e = DependencyService.Get<IEnvironment>();
            switch(Settings.ReaderTheme)
            {
                case 1:
                    StackBackGround.BackgroundColor = Color.White;
                    TextBackGround.BackgroundColor = Color.White;
                    ReaderText.TextColor = Color.Black;
                    break;
                case 2:
                    StackBackGround.BackgroundColor = Color.FromHex("f5e6bd");
                    TextBackGround.BackgroundColor = Color.FromHex("f5e6bd");
                    ReaderText.TextColor = Color.Black;
                    break;
                case 3:
                   
                    StackBackGround.BackgroundColor = Color.Black;
                    TextBackGround.BackgroundColor = Color.Black;
                    ReaderText.TextColor = Color.Gray;
                    break;
            }
        }
        private void SetStatusBarTheme()
        {
            var e = DependencyService.Get<IEnvironment>();
            switch (Settings.ReaderTheme)
            {
                case 1:
                    if (App.Current.RequestedTheme == OSAppTheme.Dark)
                    {
                        e?.SetStatusBarColor(Color.White, false);
                    }
                    else
                    {
                        e?.SetStatusBarColor(Color.White, true);
                    }
                    break;
                case 2:
                    if (App.Current.RequestedTheme == OSAppTheme.Dark)
                    {
                        e?.SetStatusBarColor(Color.FromHex("f5e6bd"), false);
                    }
                    else
                    {
                        e?.SetStatusBarColor(Color.FromHex("f5e6bd"), true);
                    }
                    break;
                case 3:
                    if (App.Current.RequestedTheme == OSAppTheme.Dark)
                    {
                        e?.SetStatusBarColor(Color.Black, false);
                    }
                    else
                    {
                        e?.SetStatusBarColor(Color.Black, true);
                    }
                    break;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            HideSettingsPanel();
            SetStatusBarTheme();
            //  Window.DecorView.SystemUiVisibility = StatusBarVisibility.Visible;
        }

        private async void HideSettingsPanel()
        {
            SettingsPanel.TranslationY = -190;
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            SettingsPanel.TranslateTo(0, 0, 250, Easing.CubicInOut);
            PanelBackground.BackgroundColor = Color.FromRgba(55, 55, 55, 99);
            PanelBackground.InputTransparent = false;
            var er = DependencyService.Get<IEnvironment>();
            if (App.Current.RequestedTheme == OSAppTheme.Dark)
            {
                er?.SetStatusBarColor(Color.FromHex("#2e74ff"), false);
            }
            else
            {
                er?.SetStatusBarColor(Color.FromHex("#2e74ff"), true);
            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, System.EventArgs e)
        {
            
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

        private void TapGestureRecognizer_Tapped_2(object sender, System.EventArgs e)
        {
            SettingsPanel.TranslateTo(0, -190, 250, Easing.CubicInOut);
            PanelBackground.BackgroundColor = Color.Transparent;
            PanelBackground.InputTransparent = true;
            SetReaderTheme();
            SetStatusBarTheme();
        }

        private void TapGestureRecognizer_Tapped_3(object sender, System.EventArgs e)
        {
            RadioWhiteTheme.IsChecked = true;
        }

        private void TapGestureRecognizer_Tapped_4(object sender, System.EventArgs e)
        {
            RadioBeigeTheme.IsChecked = true;
        }

        private void TapGestureRecognizer_Tapped_5(object sender, System.EventArgs e)
        {
            RadioBlackTheme.IsChecked = true;
        }

        private async void TapGestureRecognizer_Tapped_6(object sender, System.EventArgs e)
        {
            await Shell.Current.GoToAsync("//Main");
        }
    }
}