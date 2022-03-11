using ExpReader.Models;
using ExpReader.ViewModels;
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
        }

        private void ScrollToTop(object sender, System.EventArgs e)
        {
            scroll.ScrollToAsync(0, 0, false);
        }
    }
}