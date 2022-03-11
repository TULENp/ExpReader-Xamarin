using ExpReader.Models;
using ExpReader.ViewModels;
using System.IO;
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
    }
}