using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    class Book
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
    }
}
