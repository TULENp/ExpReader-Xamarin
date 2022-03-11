using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using System.Runtime.InteropServices.ComTypes;
using Android.Content.Res;
using ExpReader.Models;
using System;
using Aspose.Words;
using Android.Media;
using SkiaSharp;
using Java.Nio;
using System.Collections.Generic;
using System.Windows.Input;

namespace ExpReader.ViewModels
{
    class ReaderVM : BindableObject
    {
        Book newBook;
        string text;
        string charbook;

        List<string> BookLines = new List<string>();
        readonly int pageLines = 3;
        readonly int pageChars = 750;
        //int CurrentPage;
        public Book NewBook
        {
            get => newBook;
            set
            {
                newBook = value;
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
        public ReaderVM(Book book)
        {
            NewBook = book;
            
            OpenBook();
            ReadPage();
        }
        public ICommand OpenNextPage => new Command(value =>
        {
            NewBook.CurrentPage++; 
            NewBook.ReadPages++;
            Text = "";
            ReadPage();
        });
        public ICommand OpenPrevPage => new Command(value =>
        {
            if (NewBook.CurrentPage != 0)
            {
                NewBook.CurrentPage--;
                Text = "";
                ReadPage();
            }
        });

        public void ReadPage()
        {
            
            int readchar = NewBook.CurrentPage * pageChars;
            int i;
            //while (!Char.IsSeparator(charbook[readchar]))
            //{
            //    readchar++;
            //}

            for (i = readchar; i < readchar + pageChars; i++)
            {
                Text += charbook[i];
            }
            if (!(Char.IsWhiteSpace(charbook[i - 1]) || charbook[i - 1] == '-'))
            {
                Text += '-';
            }
            //while (!Char.IsSeparator(charbook[i]))
            //{
            //    Text += charbook[i];
            //    i++;
            //}

            //todo problem - lines are different. I need to split them to fixed lines
            //int readlines = NewBook.ReadPages * pageLines;
            //for (int i = readlines; i < readlines + pageLines; i++)
            //{
            //    Text += BookLines[i] + "\n";
            //}
        }
        public async void OpenBook()
        {
            using (var stream = await FileSystem.OpenAppPackageFileAsync(NewBook.Path))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    charbook = reader.ReadToEnd();
                }
            }
            //using (var stream = await FileSystem.OpenAppPackageFileAsync(NewBook.Path))
            //{
            //    using (StreamReader reader = new StreamReader(stream))
            //    {
            //        while (!reader.EndOfStream)
            //        {
            //            BookLines.Add(await reader.ReadLineAsync());
            //        }
            //    }
            //}


            //AssetManager assetManager = Android.App.Application.Context.Assets;
            //using (Stream stream = assetManager.Open(NewBook.Path))
            //{
            //    Document doc = new Document(stream);
            //    Text = doc.GetText();
            //}
            //string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);\


            //var file = await FilePicker.PickAsync();
            //using (var stream = File.OpenRead(file.FullPath))
            //{
            //    Document doc = new Document(stream);
            //    Text = doc.GetText();
            //}

            //Document doc = new Document(Android.App.Application.Context.Assets + NewBook.Path);
            //Text = doc.GetText();
            //reading txt

        }

    }
}
