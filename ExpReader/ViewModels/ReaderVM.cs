using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
//using Aspose.Words; //todo add nuget 
using System.Runtime.InteropServices.ComTypes;
using Android.Content.Res;
using ExpReader.Models;

namespace ExpReader.ViewModels
{
    class ReaderVM : BindableObject
    {
        Book newbook;
        string text;
        public Book newBook
        {
            get => newbook;
            set
            {
                newbook = value;
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
        public ReaderVM(Book newbook)
        {
            newBook = newbook;
            OpenBook();
        }
        public async void OpenBook()
        {
            //reading txt
            using (var stream = await FileSystem.OpenAppPackageFileAsync(newBook.Path))
            {
                using (var reader = new StreamReader(stream))
                {
                    Text = await reader.ReadToEndAsync();
                }
            }


            //todo need to use OpenRead or smth else Method(not FileSystem.OpenAppPackageFileAsync(Path)) to open file
            //var dir = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData);

            // using (Stream stream = File.OpenRead(dir+Path))
            // {
            //     Document doc = new Document(stream);
            //     Text = doc.GetText();
            // }
        }
    }
}
