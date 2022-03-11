using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpReadersBack.Controllers
{
    [Route("[Books]")]
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
        public Task<Book> GetBook(int id) => db.Books.FirstOrDefaultAsync(b => b.Id == id);
    }
}
