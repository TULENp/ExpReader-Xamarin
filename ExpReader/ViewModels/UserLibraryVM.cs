﻿using Android.Widget;
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

    class UserLibraryVM: BindableObject
    {
        public ObservableCollection<Book> Books { get; }

        public UserLibraryVM()
        {
            Books = new ObservableCollection<Book>();
            Books.Add(new Book() { Id = 1, Author = "Анджей Сапковский", Path = "что-то",Title = "Ведьмак" });
            Books.Add(new Book() { Id = 2, Author = "Фрэнк Герберт", Path = "что-то", Title = "Дюна" });
            Books.Add(new Book() { Id = 3, Author = "Джейсон Шрейер", Path = "что-то", Title = "Кровь,пот и пиксели" });
        }

        public ICommand OpenBookCommand => new Command<Book>(OpenBook);

        public async void OpenBook(Book book)
        {
            await Shell.Current.Navigation.PushAsync(new ReadingPage(book));
        }
    }
}
