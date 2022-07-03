using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportComplexMVC.Models.Entities;
using SportComplexMVC.Models.DataDb;

namespace SportComplexMVC.Controllers
{
    public class CoachesController : Controller
    {
        private ApplicationContext db;

        public CoachesController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ViewResult> IndexAsync()
        {
            List<Coach> coaches = await db.Coaches
                .Include(c => c.Position)
                .Include(c => c.Specialization)
                .Include(c => c.ApplicationUser)
                .ThenInclude(p => p.Gender)
                .AsNoTracking()
                .ToListAsync();

            return View(coaches);
        }
    }
}
