using System;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    class UserBook
    {
        [Key]
        public int UserID { get; set; }
        [Key]
        public int BookID { get; set; }
        public int ReadPages { get; set; }
        public DateTime LastBookDate { get; set; }
    }
}
