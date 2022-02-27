using ExpReader.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms.Shapes;

namespace ExpReader.ViewModels
{
    class ReaderVM
    {

        public string Text { get; set; }
        public ReaderVM()
        {
            InitBooks();
        }
        public async void InitBooks()
        {
            //using (var stream = await FileSystem.OpenAppPackageFileAsync(path))
            //{
            //    using (var reader = new StreamReader(stream))
            //    {
            //        Text = await reader.ReadToEndAsync();
            //    }
            //}
        }
    }
}
