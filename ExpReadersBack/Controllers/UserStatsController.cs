using Microsoft.AspNetCore.Mvc;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ExpReadersBack.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserStatsController : ControllerBase
    {
        private ApplicationContext db;
        public UserStatsController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet("GetUserStats/{userid}")]
        public async Task<UserStats> GetUserStats(int userid)
        {
            return await db.UserStatistics.FirstOrDefaultAsync(us => us.User == userid);
        }
    }
}
