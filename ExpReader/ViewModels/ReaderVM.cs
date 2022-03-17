using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Runtime.InteropServices.ComTypes;
//using Android.Content.Res;
using System;
//using Android.Media;
//using SkiaSharp;
//using Java.Nio;
using System.Collections.Generic;
using System.Windows.Input;
using DAL.Models;
using Newtonsoft.Json;

namespace ExpReader.ViewModels
{
    class ReaderVM : BindableObject
    {
        Book newBook;
        string text;
        string charbook;
        public readonly static int pageChars = 900;
        int ReadPages;
        UserBook stats;

        //int NewBook.Pages;
        //public int NewBook.Pages
        //{
        //    get => NewBook.Pages; set
        //    {
        //        NewBook.Pages = value;
        //        OnPropertyChanged();
        //    }
        //}
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
            stats = JsonConvert.DeserializeObject<UserBook>(Preferences.Get(NewBook.Id.ToString(), string.Empty));
        }
        public ICommand OpenNextPage => new Command(value =>
        {
            NewBook.Pages++;
            ReadPages++;
            Text = "";
            ReadPage();

        });
        public ICommand OpenPrevPage => new Command(value =>
        {
            if (NewBook.Pages != 0)
            {
                NewBook.Pages--;
                Text = "";
                ReadPage();
            }
        });
        public void UpdateBookStats()
        {
            Preferences.Set(NewBook.FileName, JsonConvert.SerializeObject(NewBook));
        }
        private void ReadPage()
        {
            int readchar = NewBook.Pages * pageChars;
            int i;
            for (i = readchar; i < readchar + pageChars; i++)
            {
                Text += charbook[i];
            }
            if (!(Char.IsWhiteSpace(charbook[i - 1]) || charbook[i - 1] == '-'))
            {
                Text += '-';
            }
            UpdateBookStats();
        }
        private async void OpenBook()
        {
            using (var stream = await FileSystem.OpenAppPackageFileAsync(NewBook.FileName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    charbook = reader.ReadToEnd();
                }
            }
        }


    }
}
