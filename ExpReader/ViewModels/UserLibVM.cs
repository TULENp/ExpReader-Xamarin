using Android.Widget;
using ExpReader.Models;
using ExpReader.Views;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ExpReader.ViewModels
{
    class UserLibVM : BindableObject
    {
        private string MainIp = "192.168.0.103";
        private string BooksFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"Books");

        private double _ProgressValue;
        public double ProgressValue
        {
            get
            {
                return _ProgressValue;
            }
            set
            {
                _ProgressValue = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();
        ObservableCollection<Book> db { get; set; }
        public UserLibVM()
        {
            LoadBooks();
            GetUserBooks();
        }
        public async void LoadBooks()
        {
            try
            {
                HttpClient client = new HttpClient();
                string json = await client.GetStringAsync($"http://{MainIp}/Books/GetUserBooks/1");
                Preferences.Set("UserLib", json);
            } catch {}
        }
        public void GetUserBooks()
        {
            string json = Preferences.Get("UserLib", string.Empty);
            var collection = JsonConvert.DeserializeObject<ObservableCollection<Book>>(json);
            Books.Clear();
            foreach (var book in collection)
            {
                Books.Add(book);
            }
        }

        public ICommand OpenBookCommand => new Command<Book>(OpenBook);

        public void OpenBook(Book book)
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
                            Books.Add(new Book { Title = result.FileName, Author = "N/A", CurrentPage = 0, ReadPages = 0, Path = filepath });
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
