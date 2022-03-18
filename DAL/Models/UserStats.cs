using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class UserStats
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int User { get; set; }
        public int ReadBooks { get; set; }
        public int ReadPages { get; set; }
        public string LastReadBook { get; set; }
    }
}
