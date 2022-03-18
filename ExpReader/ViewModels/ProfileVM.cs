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
        public void UpdateStats()
        {
        }
    }
}
