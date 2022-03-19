using DAL.Models;
using Newtonsoft.Json;
using System;
using ExpReader.AppSettings;
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

        private UserStats userStats;
        public UserStats UserStats
        {
            get => userStats;
            set
            {
                userStats = value;
                OnPropertyChanged();
            }
        }

        public ProfileVM()
        {
            GetUserBooks();
            UpdateUserStats();
        }
        public void UpdateUserStats()
        {
            UserStats = JsonConvert.DeserializeObject<UserStats>(Settings.userStats);
        }
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
    }
}
