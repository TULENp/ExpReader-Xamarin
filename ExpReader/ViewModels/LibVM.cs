using DAL.Models;
using ExpReader.Services;
using ExpReader.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExpReader.ViewModels
{
    class LibVM:BindableObject
    {
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();
        public LibVM()
        {
            GetAllBooks();
        }
        public ICommand AddBookCommand => new Command<Book>(AddBook);

        public void AddBook(Book book)
        {

        }

        public void GetAllBooks()
        {
            string json = DBService.GetAllBooks().Result;
            List<Book> collection = JsonConvert.DeserializeObject<List<Book>>(json.ToString());
            foreach (var book in collection)
            {
                Books.Add(book);
            }
        }
    }
}
