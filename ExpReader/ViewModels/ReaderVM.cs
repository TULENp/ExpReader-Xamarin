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
using static System.Net.Mime.MediaTypeNames;
using Acr.UserDialogs;

namespace ExpReader.ViewModels
{
    class ReaderVM : BindableObject
    {
        public static readonly int pageChars = 900;
        string text;
        string charbook;
        Book newBook;
        UserBook Stats;
        int currentPage;
        int ReadPages;

        public Book NewBook
        {
            get => newBook;
            set
            {
                newBook = value;
                OnPropertyChanged();
            }
        }
        public int CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
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
            //Stats = JsonConvert.DeserializeObject<UserBook>(Preferences.Get(NewBook.Id.ToString(), string.Empty));
            //UserStats.LastReadBook = NewBook.FileName;
            OpenBook();
            ReadPage();
        }
        public ICommand OpenNextPage => new Command(value =>
        {
            if (currentPage != NewBook.Pages)
            {
                currentPage++;
                if (currentPage != NewBook.Pages)
                {
                    if (currentPage == ReadPages + 1)
                    {
                        DailyTask.UpdateTodayReadPages();
                        ReadPages++;
                    }
                    ReadPercentCheck();

                    ReadPage();
                }
                else
                {
                    ReadLastPage();
                    //Stats.IsRead = true;
                    UserDialogs.Instance.Alert("Read");
                }
            }
        });
        public ICommand OpenPrevPage => new Command(value =>
        {
            if (currentPage != 0)
            {
                currentPage--;
                Text = "";
                ReadPage();
            }
        });

        private void ReadPage()
        {
            int readchar = currentPage * pageChars;
            string pagetext = "";
            int i;
            for (i = readchar; i < readchar + pageChars; i++)
            {
                pagetext += charbook[i];

            }
            if (!(Char.IsWhiteSpace(charbook[i - 1]) || charbook[i - 1] == '-'))
            {
                pagetext += '-';
            }
            Text = pagetext.Replace("</p><p>", " ");
            UpdateBookStats();
        }
        private void ReadLastPage()
        {
            int readchar = currentPage * pageChars;
            int i = readchar;
            string pagetext = "";
            //todo replace NewBook.Pages * pageChars to charbook.Length
            while (i != charbook.Length)
            {
                pagetext += charbook[i];
                i++;
            }
            Text = pagetext.Replace("</p><p>", " ");
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
            Preferences.Set(NewBook.Id.ToString(), JsonConvert.SerializeObject(Stats));
        }
        void ReadPercentCheck()
        {
            int bronze = (NewBook.Pages * 30) / 100;
            int silver = (NewBook.Pages * 60) / 100;
            int gold = NewBook.Pages;

            if (ReadPages >= bronze && ReadPages< silver)
            {
                //присвоить бронзовую обложку
                UserDialogs.Instance.Alert("Bronze");
            }
            else if (ReadPages >= silver && ReadPages < gold)
            {
                //присвоить серебряную обложку
                UserDialogs.Instance.Alert("Silver");
            }
            else if (ReadPages == gold)
            {
                //присвоить золотую обложку
                UserDialogs.Instance.Alert("Gold");
            }

        }
    }
}
