using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ExpReader.ViewModels
{
    [QueryProperty(nameof(Path), nameof(Path))]
    class ReaderVM : BindableObject
    {
        string path;
        string text;

        public ReaderVM()
        {
            InitBooks();
            //Path = path;
        }

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
