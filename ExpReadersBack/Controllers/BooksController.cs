using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpReadersBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private ApplicationContext db;
        public BooksController(ApplicationContext context)
        {
            db = context;
        }
        
        [HttpGet("GetAllBooks")]
        public async Task<ActionResult<List<Book>>> GetAllBooks()
        {
            return await db.Books.ToListAsync();
        }

        [HttpGet("GetBook")]
        public async Task<Book> GetBook(int id) => await db.Books.FirstOrDefaultAsync(b => b.Id == id);

        [HttpGet("GetUserBooks/{userid}")]
        public async Task<List<Book>> GetUserBook(int userid)
        {
            var data = from b in db.Books
                       join ub in db.UserBooks on b.Id equals ub.BookId where ub.UserId == userid
                       select b;
            return await data.ToListAsync();
        }
    }
}
