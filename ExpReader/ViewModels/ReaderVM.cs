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
using static System.Net.Mime.MediaTypeNames;
using Acr.UserDialogs;
using ExpReader.DailyTasks;

namespace ExpReader.ViewModels
{
    class ReaderVM : BindableObject
    {
        public static readonly int pageChars = 900;
        string text;
        string charbook;
        Book newBook;
        UserBook Stats;
        UserStats userStats;
        string BooksFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Books");

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
            Stats = JsonConvert.DeserializeObject<UserBook>(Preferences.Get(NewBook.Id.ToString(), string.Empty));
            userStats = JsonConvert.DeserializeObject<UserStats>(Preferences.Get("UserStats", string.Empty));
            userStats.LastReadBook = NewBook.FileName;
            OpenBook();
            ReadPage();
            UpdateUserStats();
        }
        //public ICommand OpenNextPage => new Command(value =>
        //{
        //    if (Stats.CurrentPage != NewBook.Pages)
        //    {
        //        if (Stats.CurrentPage == Stats.ReadPages)
        //        {
        //            DailyTask.UpdateTodayReadPages();
        //            Stats.ReadPages++;
        //        }
        //        Stats.CurrentPage++;
        //        Text = "";
        //        ReadPercentCheck();
        //        ReadPage();

        //    }
        //    else
        //    {
        //        Stats.IsRead = true;
        //        UpdateBookStats();
        //        UserDialogs.Instance.Alert("Read");
        //    }
        //});

        public ICommand OpenNextPage => new Command(value =>
        {
            if (Stats.CurrentPage != NewBook.Pages)
            {
                Stats.CurrentPage++;
                if (Stats.CurrentPage != NewBook.Pages)
                {
                    if (Stats.CurrentPage == Stats.ReadPages + 1)
                    {
                        DailyTask.UpdateTodayReadPages();
                        Stats.ReadPages++;
                    }
                    ReadPercentCheck();
                    ReadPage();
                }
                else
                {
                    ReadLastPage();
                    Stats.IsRead = true;
                    UpdateBookStats();
                }
            }
        });

        private void ReadLastPage()
        {
            int readchar = (Stats.CurrentPage - 1) * pageChars;
            int i = readchar;
            string pagetext = "";
            while (i != charbook.Length - 1)
            {
                pagetext += charbook[i];
                i++;
            }
            Text = pagetext.Replace("</p><p>", " ");
            UpdateBookStats();
        }


        public ICommand OpenPrevPage => new Command(value =>
        {
            if (Stats.CurrentPage != 0)
            {
                currentPage--;
                Text = "";
                ReadPage();
            }
        });

        private void ReadPage()
        {
            int readchar = Stats.CurrentPage * pageChars;
            string pagetext = "";
            int i = readchar;
            for (i = readchar; i < readchar + pageChars && i < charbook.Length; i++)
            {
                pagetext += charbook[i];
            }
            //if (!(Char.IsWhiteSpace(charbook[i - 1]) || charbook[i - 1] == '-'))
            //{
            //    pagetext += '-';
            //}
            Text = pagetext.Replace("</p><p>", " ");
            UpdateBookStats();
        }
        private void OpenBook()
        {
            // TODO Change Path 
            string path = Path.Combine(BooksFolderPath, NewBook.FileName);
            using (var stream = File.OpenRead(path))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    charbook = reader.ReadToEnd();
                    //int pagestest = charbook.Length / ReaderVM.pageChars + 1;
                }
            }
        }
        void UpdateBookStats()
        {
            Preferences.Set(NewBook.Id.ToString(), JsonConvert.SerializeObject(Stats));
        }
        void UpdateUserStats()
        {
            Settings.userStats = JsonConvert.SerializeObject(userStats);
        }
        void ReadPercentCheck()
        {
            int bronze = (NewBook.Pages * 30) / 100;
            int silver = (NewBook.Pages * 60) / 100;
            int gold = NewBook.Pages;

            if (Stats.ReadPages == bronze)
            {
                //присвоить бронзовую обложку
                UserDialogs.Instance.Alert("Bronze");
            }
            else if (Stats.ReadPages == silver)
            {
                //присвоить серебряную обложку
                UserDialogs.Instance.Alert("Silver");
            }
            else if (Stats.ReadPages == gold)
            {
                //присвоить золотую обложку
                UserDialogs.Instance.Alert("Gold");
            }

        }
    }
}