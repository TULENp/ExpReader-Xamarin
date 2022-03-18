using DAL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ExpReader.ViewModels
{
    class ProfileVM : BindableObject
    {
        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();
        ObservableCollection<Book> db { get; set; }

        private int readPages;
        public int ReadPages
        {
            get => readPages;
            set
            {
                readPages = value;
                OnPropertyChanged();
            }
        }

        public ProfileVM()
        {
            UpdateStats();
        }
        private void GetReadBooks()
        {
            //string json = Preferences.Get("BookNames", string.Empty);
            //var collection = JsonConvert.DeserializeObject<List<string>>(json);
            //Books.Clear();
            //foreach (var name in collection)
            //{
            //    Book book = (JsonConvert.DeserializeObject<Book>(Preferences.Get(name, string.Empty)));
            //    if (book.Is)
            //    {
            //        Books.Add(book);
            //    }

        }
    }
    public void UpdateStats()
    {
        Book book = JsonConvert.DeserializeObject<Book>(Preferences.Get("prestup.txt", string.Empty));
        ReadPages = book.Pages;
    }

}

