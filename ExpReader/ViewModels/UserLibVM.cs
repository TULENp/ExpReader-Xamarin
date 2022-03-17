using Android.Widget;
using DAL.Models;
using ExpReader.Views;
using Newtonsoft.Json;
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
        private string BooksFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"Books");
        private int userid;
        private double progressValue;
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
            List <Book> collection = JsonConvert.DeserializeObject<List<Book>>(json.ToString());
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
        private void GetUserBooks() 
        {
            string json = Preferences.Get("BookNames", string.Empty);
            var collection = JsonConvert.DeserializeObject<List<string>>(json);
            Books.Clear();
            foreach (var name in collection)
            {
                Books.Add(JsonConvert.DeserializeObject<Book>(Preferences.Get(name, string.Empty)));
            }
        }

        // Get userbookstats drom db and separately write to preferences.
        public void SetUserBookStats()
        {
            string json = DBService.GetUserBookStats(userid).Result;
            List<UserBook> collection = JsonConvert.DeserializeObject<List<UserBook>>(json.ToString());
            foreach (var file in collection)
            {
                Preferences.Set(file.BookId.ToString(), JsonConvert.SerializeObject(file));
            }
        }

        public ICommand OpenBookCommand => new Command<Book>(OpenBook);
        private void OpenBook(Book book)
        {
            Shell.Current.Navigation.PushAsync(new ReaderPage(book));
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
                            Books.Add(new Book { Title = result.FileName, Author = "N/A", FileName=result.FileName, Pages=pages });
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Toast.MakeText(Android.App.Application.Context, exc.Message, ToastLength.Long).Show();
            }
        }
    }
}