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
    public class AccountController : Controller
    {
        private ApplicationContext db;

        public AccountController(ApplicationContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<ViewResult> IndexAsync()
        {
            Person person = await db.People.FindAsync(1);

            return View(person);
        }
    }
}
