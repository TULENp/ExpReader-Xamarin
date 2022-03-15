using DAL.Models;
using ExpReader.Views;
using Newtonsoft.Json;
using Org.Apache.Http.Authentication;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using static Android.Icu.Text.CaseMap;
using static Android.Resource;

namespace ExpReader.ViewModels
{
    internal class UserLibVM : BindableObject
    {
        private string MainIp = "192.168.0.103";
        private string BooksFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"Books");

        private double _ProgressValue;
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

        public UserLibVM()
        {
            Shell.Current.Navigation.PushAsync(new ReaderPage(book));
        }
        public void SetUserBooks()
        {
            string booknames = "";
            HttpClient client = new HttpClient();
            var json = "[{\"Id\":0,\"Title\":\"(МЯУ)Преступление и наказание(Pdf)\",\"Author\":\"Достоевский Ф.М.\",\"FileName\":\"prest.pdf\",\"Pages\":0},{\"Id\":1,\"Title\":\"Преступление и наказание(Epub)\",\"Author\":\"Достоевский Ф.М.\",\"FileName\":\"prest.epub\",\"Pages\":0},{\"Id\":2,\"Title\":\"Мастер и маргарита(F2b)\",\"Author\":\"да\",\"FileName\":\"master.fb2\",\"Pages\":0},{\"Id\":3,\"Title\":\"Иэнис\",\"Author\":\"zzz\",\"FileName\":\"ienis.docx\",\"Pages\":0},{\"Id\":4,\"Title\":\"Преступление и наказание(Txt)\",\"Author\":\"Достоевский Ф.М.\",\"FileName\":\"prestup.txt\",\"Pages\":0}]";
            Preferences.Set("UserLibrary", json);
            List<Book> collection = JsonConvert.DeserializeObject<List<Book>>(json);
            foreach (var file in collection)
            {
                Preferences.Set(file.FileName, JsonConvert.SerializeObject(file));
            }
        }
        private void GetUserBooks()
        {
            string json = Preferences.Get("UserLibrary", string.Empty);
            var collection = JsonConvert.DeserializeObject<List<Book>>(json);
            Books.Clear();
            foreach (var name in collection)
            {
                Books.Add(JsonConvert.DeserializeObject<Book>(Preferences.Get(name.FileName, string.Empty)));
            }
            
        }
        private void OpenBook(Book book)
        {
            Shell.Current.Navigation.PushAsync(new ReaderPage(book));

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