using ExpReader.Models;
using ExpReader.Services;
using ExpReader.ViewModels;
using System.IO;
using System.Threading;
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
            //Thread.Sleep(1000);
            //Task.Delay(5000);
            //ShowDailyMessage();
        }
        
        private void MySlider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            ReaderText.FontSize = System.Convert.ToInt32(MySlider.Value);
            Settings.ReaderSlider = System.Convert.ToInt32(MySlider.Value);
        }

        public void ScrollToTop(object sender, System.EventArgs e)
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
                    TextBackGround.BackgroundColor = Color.White;
                    ReaderText.TextColor = Color.Black;
                    break;
                case 2:
                    TextBackGround.BackgroundColor = Color.FromHex("f5e6bd");
                    ReaderText.TextColor = Color.Black;
                    break;
                case 3:
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
            SettingsPanel.TranslationY = 190;
        }

        private async void ShowDailyMessage()
        {
            MessagePanel.TranslateTo(0, 0, 250, Easing.BounceOut);
            await Task.Delay(5000);
            MessagePanel.TranslateTo(0, -150, 250, Easing.BounceOut);
        }

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            if(SettingsPanel.TranslationY ==190)
            {
                imageGear.Source = ImageSource.FromFile("gear.png");
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
            else
            {
                TapGestureRecognizer_Tapped_2(sender, e);
            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, System.EventArgs e)
        {
            
            if(UpperPannel.IsVisible==true)
            {
                UpperPannel.TranslateTo(0, -110, 250, Easing.CubicIn);
                Task.Delay(250);
                UpperPannel.IsVisible = false;
                SetStatusBarTheme();
            }
            else
            {
                UpperPannel.TranslateTo(0, 0, 250, Easing.CubicInOut);
                UpperPannel.IsVisible = true;
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
        }

        private void TapGestureRecognizer_Tapped_2(object sender, System.EventArgs e)
        {
            SettingsPanel.TranslateTo(0, 190, 250, Easing.CubicInOut);
            imageGear.Source = ImageSource.FromFile("gearWhite.png");
            PanelBackground.BackgroundColor = Color.Transparent;
            PanelBackground.InputTransparent = true;
            SetReaderTheme();
           // SetStatusBarTheme();
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