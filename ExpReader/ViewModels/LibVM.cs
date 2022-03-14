using ExpReader.Models;
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
            InitBooks();
        }
        public ICommand OpenBookCommand => new Command<Book>(OpenBook);

        public void OpenBook(Book book)
        {
            Shell.Current.Navigation.PushAsync(new ReaderPage(book));

        }


        private void InitBooks()
        {
            //todo Move this collection to db or somewhere else
            db = new ObservableCollection<Book>()
            {
                new Book
                {
                    Id = 0,
                    Title = "Преступление и наказание(Pdf)",
                    Author = "Достоевский Ф.М.fffffffffffffffffffffffffffffffffff",
                    Path = "prest.pdf"
                },
                new Book
                {
                    Id = 1,
                    Title = "Преступление и наказание(Epub)",
                    Author = "Достоевский Ф.М.",
                   Path = "prest.epub"
                },
                new Book
                {
                    Id = 2,
                    Title = "Мастер и маргарита(F2b)",
                    Author = "да",
                    Path = "master.fb2"
                },
                new Book
                {
                    Id = 3,
                    Title = "Иэнис",
                    Author = "zzz",
                    Path = "ienis.docx"
                },
                new Book
                {
                    Id = 4,
                    Title = "Преступление и наказание(Txt)",
                    Author = "Достоевский Ф.М.",
                    Path = "prestup.txt",
                    ReadPages = 0,
            CurrentPage = 0
                }
    };

            foreach (var file in db)
            {
                Books.Add(file);
            }
        }
    }
}
