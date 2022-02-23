using ExpReader.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace ExpReader.ViewModels
{
    class UserLibraryVM : BindableObject
    {
        public ObservableCollection<Book> Books { get; }
        public UserLibraryVM()
        {
            Books = new ObservableCollection<Book>();
        }

    }
}
