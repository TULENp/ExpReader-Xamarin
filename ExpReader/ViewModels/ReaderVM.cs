using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Aspose.Words;

namespace ExpReader.ViewModels
{
    class ReaderVM : BindableObject
    {
        string path;
        string text;
        public string Path
        {
            get => path;
            set
            {
                path = value;
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
        public ReaderVM(string newpath)
        {
            Path = newpath;
            InitBooks();
        }
        public async void InitBooks()
        {
            using (var stream = await FileSystem.OpenAppPackageFileAsync(Path))
            {
                using (var reader = new StreamReader(stream))
                {
                    Text = await reader.ReadToEndAsync();
                }
            }
        }
    }
}
