using System;
using System.Collections.Generic;
using System.Text;

namespace ExpReader.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Path { get; set; }
        public int CurrentPage { get; set; }
        public int ReadPages { get; set; }
    }
}
