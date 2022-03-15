using DAL.Models;
using ExpReader.CustomComponents;
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
        ReaderVM readerVM;
        public ReaderPage(Book book)
        {
            InitializeComponent();
            readerVM = new ReaderVM(book);
            BindingContext = readerVM;
            Shell.SetNavBarIsVisible(this, false);
            Shell.SetTabBarIsVisible(this, false);
            // PdfJsWebView.Uri = Path.Combine(FileSystem.AppDataDirectory, book.Path);
        }
        private void ScrollToTop(object sender, System.EventArgs e)
        {
            scroll.ScrollToAsync(0, 0, false);
            
        }
    }
}