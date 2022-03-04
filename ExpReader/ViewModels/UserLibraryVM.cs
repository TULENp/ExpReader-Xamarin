using ExpReader.Models;
using ExpReader.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExpReader.ViewModels
{

    class UserLibraryVM: BindableObject
    {
        public ObservableCollection<Book> Books { get; }
        public ICommand TapCommandSotr { get; }
        public ICommand TapCommandRead { get; }

        public UserLibraryVM()
        {
            Books = new ObservableCollection<Book>();
            Books.Add(new Book() { Id = 1, Author = "Анджей Сапковский", Path = "что-то",Title = "Ведьмак" });
            Books.Add(new Book() { Id = 2, Author = "Фрэнк Герберт", Path = "что-то", Title = "Дюна" });
            Books.Add(new Book() { Id = 3, Author = "Джейсон Шрейер", Path = "что-то", Title = "Кровь,пот и пиксели" });
            TapCommandSotr= new Command(OnSettings);
            TapCommandRead = new Command(OnReading);
        }

        private async void OnSettings()
        {
            await Shell.Current.GoToAsync(nameof(SettingsPage));
        }

        private async void OnReading()
        {
            await Shell.Current.GoToAsync(nameof(ReadingPage));
        }
    }
}
