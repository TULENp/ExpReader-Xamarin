using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class UserBook
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int ReadPages { get; set; }
        public DateTime LastBookDate { get; set; }
    }
}
