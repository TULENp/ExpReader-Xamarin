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
        public Uri Cover { get; set; }
        public string Path { get; set; }
    }
}
