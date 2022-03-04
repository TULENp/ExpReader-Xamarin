using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    class UserBook
    {
        [Key]
        public int UserID { get; set; }
        [Key]
        public int BookID { get; set; }
    }
}
