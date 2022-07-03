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

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(TrainingRoom trainingRoom)
        {
            if (!ModelState.IsValid)
                return View(trainingRoom);

            db.TrainingRooms.Add(trainingRoom);
            await db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> EditAsync(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            TrainingRoom trainingRoom = await db.TrainingRooms.FindAsync(id);

            if (trainingRoom == null)
                return RedirectToAction("Index");

            return View(trainingRoom);
        }

        [HttpPost]
        public async Task<ActionResult> EditAsync(TrainingRoom trainingRoom)
        {
            if (!ModelState.IsValid)
                return View(trainingRoom);

            db.Attach(trainingRoom).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<RedirectToActionResult> DeleteAsync(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            TrainingRoom trainingRoom = await db.TrainingRooms.FindAsync(id);

            if (trainingRoom != null)
            {
                db.TrainingRooms.Remove(trainingRoom);
                await db.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
