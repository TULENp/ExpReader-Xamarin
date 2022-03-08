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
        [ForeignKey("FK_UserID")]
        public User UserId { get; set; }
    }
}
