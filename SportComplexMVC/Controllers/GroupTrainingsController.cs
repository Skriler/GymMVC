using System;
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
    public class GroupTrainingsController : Controller
    {

        public GroupTrainingsController(ApplicationContext context)
        {

        }

        //[HttpGet]
        //public async Task<ViewResult> IndexAsync()
        //{
        //    List<GroupTraining> groupTrainings = await personalTrainingsDAL.GetPersonalTrainingListAsync();

        //    return View(groupTrainings);
        //}
    }
}
