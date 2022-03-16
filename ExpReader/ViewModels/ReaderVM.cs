using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using ExpReader.Services;
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
using ExpReader.UserStats.DailyTasks;

namespace ExpReader.ViewModels
{
    class ReaderVM : BindableObject
    {
        readonly int pageChars = 900;
        Book newBook;
        string text;
        string charbook;
        int ReadPages;
        int CurrentPage;

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
            if (CurrentPage == ReadPages)
            {
                DailyTask.UpdateTodayReadPages();
                ReadPages++;
                CurrentPage++;
            }
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
        void UpdateBookStats()
        {
            Preferences.Set(NewBook.FileName, JsonConvert.SerializeObject(NewBook));
        }


    }
}
