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
using System.Net.Http;

namespace ExpReader.ViewModels
{
    internal class UserLibVM : BindableObject
    {
        Book lastReadBook;
        private string BooksFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Books");

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
        public Book LastReadBook
        {
            get => lastReadBook;
            set
            {
                lastReadBook = value;
                OnPropertyChanged();
            }
        }
        int userid = 1; //TODO Move it to session user and get from
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();
        public UserLibVM()
        {
            SetUserBooks();
            GetUserBooks();
            //SetUserBookStats();
        }
        public void SetUserBooks()
        {
            List<string> booknames = new List<string>();
            HttpClient client = new HttpClient();
            var json = "[{\"Id\":0,\"Title\":\"Преступление и наказание(Pdf)\",\"Author\":\"Достоевский Ф.М.\",\"FileName\":\"prest.pdf\",\"Pages\":10},{\"Id\":1,\"Title\":\"Преступление и наказание(Epub)\",\"Author\":\"Достоевский Ф.М.\",\"FileName\":\"prest.epub\",\"Pages\":0},{\"Id\":2,\"Title\":\"Мастер и маргарита(F2b)\",\"Author\":\"да\",\"FileName\":\"master.fb2\",\"Pages\":0},{\"Id\":3,\"Title\":\"Иэнис\",\"Author\":\"zzz\",\"FileName\":\"ienis.docx\",\"Pages\":0},{\"Id\":4,\"Title\":\"Преступление и наказание(Txt)\",\"Author\":\"Достоевский Ф.М.\",\"FileName\":\"prestup.txt\",\"Pages\":10}]";
            List<Book> collection = JsonConvert.DeserializeObject<List<Book>>(json);
            foreach (var file in collection)
            {
                Preferences.Set(file.FileName, JsonConvert.SerializeObject(file));
                booknames.Add(file.FileName);
            }
            Preferences.Set("BookNames", JsonConvert.SerializeObject(booknames));
        }
        //Get user's books from db and write it to preferences. 
        //public void SetUserBooks()
        //{
        //    List<string> booknames = new List<string>();
        //    string json = DBService.GetUserBooks(userid).Result;
        //    List<Book> collection = JsonConvert.DeserializeObject<List<Book>>(json.ToString());
        //    foreach (var file in collection)
        //    {
        //        Preferences.Set(file.FileName, JsonConvert.SerializeObject(file));
        //        booknames.Add(file.FileName);
        //    }
        //    Preferences.Set("BookNames", JsonConvert.SerializeObject(booknames));
        //}
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
        //todo Add SetUserStats() and GetUserStats()
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
            //todo switch prestup.txt to UserStats.LastReadBook
            LastReadBook = JsonConvert.DeserializeObject<Book>(Preferences.Get("prestup.txt", string.Empty));
        }
        private string SetCover(int pages)
        {
            if (pages < 200)
            {
                return "СoverGreen.png";
            }
            else if (pages >= 200 && pages < 400)
            {
                return "СoverBlue.png";
            }
            else if (pages >= 600)
            {
                return "СoverRed.png";
            }
            return "";
        }
    }
}