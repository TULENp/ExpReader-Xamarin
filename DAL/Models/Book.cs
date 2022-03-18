using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Pages { get; set; }
        public string FileName { get; set; }
    }
}
