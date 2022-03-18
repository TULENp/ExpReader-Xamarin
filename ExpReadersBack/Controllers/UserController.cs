using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpReadersBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ApplicationContext db;
        public UserController(ApplicationContext context)
        {
            db = context;
        }
        [HttpPost("SignUp")]
        public ActionResult SignUp(User loguser)
        {
            var _user = db.Users.FirstOrDefault(u => u.Login == loguser.Login);
            if (_user == null)
            {
                db.Users.Add(loguser);
                db.SaveChanges();
                return Ok("Регистрация прошла успешно");
            }
            return BadRequest("Пользователь с такими данными уже зарегистрирован");
        }

        [HttpGet("SignIn")]
        public ActionResult<User> SignIn(string login, string password)
        {
            var _user = db.Users.FirstOrDefault(u => u.Login == login && u.Password == password);
            if (_user != null) 
                return Ok(_user); 
            else return BadRequest("Данные введены некорректно");
        }
    }
}
