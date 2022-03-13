using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Runtime.InteropServices.ComTypes;
//using Android.Content.Res;
using ExpReader.Models;
using System;
//using Android.Media;
//using SkiaSharp;
//using Java.Nio;
using System.Collections.Generic;
using System.Windows.Input;

namespace ExpReader.ViewModels
{
    class ReaderVM : BindableObject
    {
        Book newBook;
        string text;
        string charbook;
        readonly int pageChars = 900;
        public Book NewBook
        {
            get => newBook;
            set
            {
                newBook = value;
                OnPropertyChanged();
            }
        }
        public string Text
        {
            get => text;
            set
            {
                text = value;
                OnPropertyChanged();
            }
        }
        public ReaderVM(Book book)
        {
            NewBook = book;
            OpenBook();
            ReadPage();
        }
        public ICommand OpenNextPage => new Command(value =>
        {
            NewBook.CurrentPage++;
            NewBook.ReadPages++;
            Text = "";
            ReadPage();
        });
        public ICommand OpenPrevPage => new Command(value =>
        {
            if (NewBook.CurrentPage != 0)
            {
                NewBook.CurrentPage--;
                Text = "";
                ReadPage();
            }
        });

        public void ReadPage()
        {
            int readchar = NewBook.CurrentPage * pageChars;
            int i;
            for (i = readchar; i < readchar + pageChars; i++)
            {
                Text += charbook[i];
            }
            if (!(Char.IsWhiteSpace(charbook[i - 1]) || charbook[i - 1] == '-'))
            {
                Text += '-';
            }
        }
        public async void OpenBook()
        {
            using (var stream = await FileSystem.OpenAppPackageFileAsync(NewBook.Path))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    charbook = reader.ReadToEnd();
                }
            }
        }
    }
}
