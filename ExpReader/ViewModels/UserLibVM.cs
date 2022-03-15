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
        //List<Book> collection;
        double progressValue;

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

        public ICommand OpenBookCommand => new Command<Book>(OpenBook);

        public UserLibVM()
        {
            SetUserBooks();
            GetUserBooks();
        }
        public void SetUserBooks()
        {
            List<string> booknames = new List<string>();
            HttpClient client = new HttpClient();
            var json = "[{\"Id\":0,\"Title\":\"(МЯУ)Преступление и наказание(Pdf)\",\"Author\":\"Достоевский Ф.М.\",\"FileName\":\"prest.pdf\",\"Pages\":0},{\"Id\":1,\"Title\":\"Преступление и наказание(Epub)\",\"Author\":\"Достоевский Ф.М.\",\"FileName\":\"prest.epub\",\"Pages\":0},{\"Id\":2,\"Title\":\"Мастер и маргарита(F2b)\",\"Author\":\"да\",\"FileName\":\"master.fb2\",\"Pages\":0},{\"Id\":3,\"Title\":\"Иэнис\",\"Author\":\"zzz\",\"FileName\":\"ienis.docx\",\"Pages\":0},{\"Id\":4,\"Title\":\"Преступление и наказание(Txt)\",\"Author\":\"Достоевский Ф.М.\",\"FileName\":\"prestup.txt\",\"Pages\":0}]";
            List<Book> collection = JsonConvert.DeserializeObject<List<Book>>(json);
            foreach (var file in collection)
            {
                Preferences.Set(file.FileName, JsonConvert.SerializeObject(file));
                booknames.Add(file.FileName);
            }
            Preferences.Set("BookNames", JsonConvert.SerializeObject(booknames));
        }
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
        private void OpenBook(Book book)
        {
            Shell.Current.Navigation.PushAsync(new ReaderPage(book));
        }
    }
}