using ExpReader.Models;
using ExpReader.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExpReader.ViewModels
{
    class UserLibVM : BindableObject
    {

        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book>();
        ObservableCollection<Book> db { get; set; }
        public UserLibVM()
        {
            InitBooks();
        }

        public ICommand OpenBookCommand => new Command<Book>(OpenBook);

        public void OpenBook(Book book)
        {
            Shell.Current.Navigation.PushAsync(new ReaderPage(book));

        }

        private void InitBooks()
        {
            //todo Move this collection to db or somewhere else
            db = new ObservableCollection<Book>()
            {
                new Book
                {
                    Id = 0,
                    Title = "Преступление и наказание(Pdf)",
                    Author = "Достоевский Ф.М.",
                    Cover = new Uri("https://sun9-78.userapi.com/impg/ZAIRkdW85lMCo-IbM93iu5yeMNEQdNTk9t3wZQ/9KOLEdAridM.jpg?size=448x664&quality=96&sign=b75b2d5971eb40c18b7915c4646368ef&type=album"),
                    Path = "prest.pdf"
                },
                new Book
                {
                    Id = 1,
                    Title = "Преступление и наказание(Epub)",
                    Author = "Достоевский Ф.М.",
                    Cover = new Uri("https://sun9-78.userapi.com/impg/ZAIRkdW85lMCo-IbM93iu5yeMNEQdNTk9t3wZQ/9KOLEdAridM.jpg?size=448x664&quality=96&sign=b75b2d5971eb40c18b7915c4646368ef&type=album"),
                    Path = "prest.epub"
                },
                new Book
                {
                    Id = 2,
                    Title = "Мастер и маргарита(F2b)",
                    Author = "Достоевский Ф.М.",
                    Cover = new Uri("https://sun9-78.userapi.com/impg/ZAIRkdW85lMCo-IbM93iu5yeMNEQdNTk9t3wZQ/9KOLEdAridM.jpg?size=448x664&quality=96&sign=b75b2d5971eb40c18b7915c4646368ef&type=album"),
                    Path = "master.fb2"
                },
                new Book
                {
                    Id = 3,
                    Title = "Путешествия Души",
                    Author = "М. Ньютон",
                    Cover = new Uri("https://sun9-78.userapi.com/impg/ZAIRkdW85lMCo-IbM93iu5yeMNEQdNTk9t3wZQ/9KOLEdAridM.jpg?size=448x664&quality=96&sign=b75b2d5971eb40c18b7915c4646368ef&type=album"),
                    Path = "avidreaders.ru__puteshestviya-dushi.txt"
                },
                new Book
                {
                    Id = 4,
                    Title = "Укрощение строптивой",
                    Author = "Юлия Витальевна Шилова",
                    Cover = new Uri("https://sun9-78.userapi.com/impg/ZAIRkdW85lMCo-IbM93iu5yeMNEQdNTk9t3wZQ/9KOLEdAridM.jpg?size=448x664&quality=96&sign=b75b2d5971eb40c18b7915c4646368ef&type=album"),
                    Path = "213286.txt"
                },
                new Book
                {
                    Id = 5,
                    Title = "Иэнис",
                    Author = "Юлия Витальевна Шилова",
                    Cover = new Uri("https://sun9-78.userapi.com/impg/ZAIRkdW85lMCo-IbM93iu5yeMNEQdNTk9t3wZQ/9KOLEdAridM.jpg?size=448x664&quality=96&sign=b75b2d5971eb40c18b7915c4646368ef&type=album"),
                    Path = "ienis.docx"
                }
            };

            foreach (var file in db)
            {
                Books.Add(file);
            }
        }
    }
}
