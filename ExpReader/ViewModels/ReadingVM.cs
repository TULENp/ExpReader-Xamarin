using ExpReader.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ExpReader.ViewModels
{
    public class ReadingVM : BindableObject
    {
        public ReadingVM(Book book)
        {
            ReadingBook = book;
            Text = $"Title: {ReadingBook.Title}\nAuthor: {ReadingBook.Author}";
        }

        private Book readingBook;
        public Book ReadingBook { 
            get => readingBook; 
            set
            {
                readingBook = value;
                OnPropertyChanged();            
            }
        }

        public string Text { get; set; }
    }
}
