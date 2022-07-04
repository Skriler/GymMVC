using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SportComplexMVC.Models.Entities;
using SportComplexMVC.Models.DataDb;
using SportComplexMVC.Services.DAL;

namespace SportComplexMVC.Controllers
{
    [Authorize]
    public class GroupsController : Controller
    {
        private GroupsDAL groupsDAL;

        public GroupsController(ApplicationContext context)
        {
            groupsDAL = new GroupsDAL(context);
        }

        [HttpGet]
        public async Task<ActionResult> DetailsAsync(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "GroupTrainings");

            Group group = await groupsDAL.GetGroupByIdAsync((int)id);

            return View(group);
        }

        [HttpGet]
        public async Task<ViewResult> CreateAsync()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<RedirectToActionResult> DeleteAsync(int? id)
        {
            //if (id != null)
            //    await personalTrainingsDAL.DeletePersonalTrainingAsync((int)id);

            return RedirectToAction("Index");
        }
    }
}
