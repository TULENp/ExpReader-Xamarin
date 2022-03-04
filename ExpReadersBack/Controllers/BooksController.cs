using Microsoft.AspNetCore.Mvc;

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
    }
}
