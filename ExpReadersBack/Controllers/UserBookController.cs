using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace ExpReadersBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserBookController : ControllerBase
    {
        private ApplicationContext db;

        public UserBookController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet("GetUserBookStats/{userid}")]
        public async Task<List<UserBook>> GetUserBookStats(int userid)
        {
            var data = from ub in db.UserBooks where ub.UserId == userid select ub;
            return await data.ToListAsync();
        }

        [HttpPost("SetUserBook")]
        public void SetUserBook([FromBody] UserBook userbook)
        {
            db.Update(userbook);
            db.SaveChanges();
        }

        [HttpPost("AddUserBook")]
        public void AddUserBook([FromBody] UserBook userBook)
        {
            db.Add(userBook);
            db.SaveChanges();
        }
    }
}
