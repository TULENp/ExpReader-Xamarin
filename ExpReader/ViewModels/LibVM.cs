using DAL.Models;
using ExpReader.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExpReader.ViewModels
{
    class LibVM:BindableObject
    {
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();
        ObservableCollection<Book> db { get; set; }
        public LibVM()
        {
            
        }
        public ICommand OpenBookCommand => new Command<Book>(OpenBook);

        public void OpenBook(Book book)
        {
            Shell.Current.Navigation.PushAsync(new ReaderPage(book));

        }
    }
}
