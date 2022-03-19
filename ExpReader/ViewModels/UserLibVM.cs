using Android.Widget;
using DAL.Models;
using ExpReader.Views;
using Newtonsoft.Json;
using ExpReader.AppSettings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using ExpReader.Services;

namespace ExpReader.ViewModels
{
    internal class UserLibVM : BindableObject
    {
        private string BooksFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private int userid;
        private double progressValue;
        private UserStats userStats;
        private Book lastReadBook;
        public Book LastReadBook
        {
            get => lastReadBook;
            set
            {
                lastReadBook = value;
                OnPropertyChanged();
            }
        }
        public double ProgressValue
        {
            get
            {
                return progressValue;
            }
            set
            {
                progressValue = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();
        public UserLibVM()
        {
            userid = Preferences.Get("TempUserId", -1);
            SetUserBooks();
            GetUserBooks();
            SetUserBookStats();
        }
        //Get user's books from db and write it to preferences. 
        public void SetUserBooks()
        {
            List<string> booknames = new List<string>();
            string json = DBService.GetUserBooks(userid).Result;
            List<Book> collection = JsonConvert.DeserializeObject<List<Book>>(json.ToString());
            foreach (var file in collection)
            {
                Preferences.Set(file.FileName, JsonConvert.SerializeObject(file));
                booknames.Add(file.FileName);
            }
            Preferences.Set("BookNames", JsonConvert.SerializeObject(booknames));
        }
        //TODO Add UserBookStats get/set methods.
        //TODO Add Update db method (ondestroy(), onstart()).
        //TODO Add SignUp SignIn VMs and save user into prefernces (id included). 
        //TODO Add LastReadBook.
        //TODO Search and Sort. 
        //TODO Add motivation system (+Daily tasks, Achives, Book rare system/read sys). 
        public void GetUserBooks()
        {
            string json = Preferences.Get("BookNames", string.Empty);
            var collection = JsonConvert.DeserializeObject<List<string>>(json);
            Books.Clear();
            foreach (var name in collection)
            {
                Books.Add(JsonConvert.DeserializeObject<Book>(Preferences.Get(name, string.Empty)));
            }
        }

        // Get userbookstats from db and separately write to preferences.
        public void SetUserBookStats()
        {
            List<string> userstatsids = new List<string>();
            string json = DBService.GetUserBookStats(userid).Result;
            List<UserBook> collection = JsonConvert.DeserializeObject<List<UserBook>>(json.ToString());
            foreach (var file in collection)
            {
                Preferences.Set(file.BookId.ToString(), JsonConvert.SerializeObject(file));
                userstatsids.Add(file.BookId.ToString());
            }
            Preferences.Set("BookStats", JsonConvert.SerializeObject(userstatsids));
        }

        public ICommand OpenBookCommand => new Command<Book>(OpenBook);
        private void OpenBook(Book book)
        {
            string directory = Path.Combine(BooksFolderPath, book.FileName);
            if (!File.Exists(directory))
            {
                try
                {
                    byte[] array = DBService.DownloadBook(book.FileName);
                    File.WriteAllBytes(directory, array);
                    Toast.MakeText(Android.App.Application.Context, "Загрузка...", ToastLength.Long).Show();
                }
                catch (Exception exc) { Toast.MakeText(Android.App.Application.Context, exc.Message, ToastLength.Long).Show(); }
            }
            else
            {
                Shell.Current.Navigation.PushAsync(new ReaderPage(book));
            }
        }
        public ICommand AddBookCommand => new Command(AddBook);


        public async void AddBook()
        {
            try
            {
                var result = await FilePicker.PickAsync();
                if (result != null)
                {
                    if (result.FileName.EndsWith(".txt"))
                    {
                        string filepath = Path.Combine(BooksFolderPath, result.FileName);
                        if (File.Exists(filepath)) Toast.MakeText(Android.App.Application.Context, "Файл уже существует", ToastLength.Long).Show();
                        else
                        {
                            File.Move(result.FullPath, filepath);
                            string txt = File.ReadAllText(result.FullPath);
                            int pages = txt.Length / ReaderVM.pageChars + 1;
                            Books.Add(new Book { Title = result.FileName, Author = "N/A", FileName = result.FileName, Pages = pages });
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Toast.MakeText(Android.App.Application.Context, exc.Message, ToastLength.Long).Show();
            }


        }

        public void SetLastReadBook()
        {
            string str = Settings.userStats;
            userStats = JsonConvert.DeserializeObject<UserStats>(str);
            string st = Preferences.Get(userStats.LastReadBook, string.Empty);
            LastReadBook = JsonConvert.DeserializeObject<Book>(st);
        }
    }
}