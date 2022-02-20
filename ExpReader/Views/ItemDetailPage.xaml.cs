using ExpReader.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace ExpReader.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}