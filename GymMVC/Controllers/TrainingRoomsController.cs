using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymMVC.Models.Entities;
using GymMVC.Models.DataDb;

namespace SportComplexMVC.Controllers
{
    public class TrainingRoomsController : Controller
    {
        private ApplicationContext db;

        public TrainingRoomsController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ViewResult> IndexAsync()
        {
            List<TrainingRoom> trainingRooms = await db.TrainingRooms.AsNoTracking().ToListAsync();

            return View(trainingRooms);
        }
    }
}
