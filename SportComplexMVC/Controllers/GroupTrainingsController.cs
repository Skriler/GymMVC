using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SportComplexMVC.Models.Entities;
using SportComplexMVC.Models.DataDb;
using SportComplexMVC.Services.DAL;
using SportComplexMVC.Models.ViewModels;
using SportComplexMVC.Services;

namespace SportComplexMVC.Controllers
{
    [Authorize]
    public class GroupTrainingsController : Controller
    {
        private GroupTrainingsDAL groupTrainingsDAL;
        public GroupTrainingsController(ApplicationContext context)
        {
            groupTrainingsDAL = new GroupTrainingsDAL(context);
        }

        [HttpGet]
        public async Task<ViewResult> IndexAsync()
        {
            List<GroupTraining> groupTrainings = await groupTrainingsDAL.GetGroupTrainingListAsync();

            return View(groupTrainings);
        }

        [Authorize(Roles = "Coach")]
        [HttpGet]
        public async Task<ActionResult> CreateAsync()
        {
            Coach coach = await groupTrainingsDAL.GetCurrentCoach(User);

            AddGroupTrainingViewModel addViewModel = new AddGroupTrainingViewModel()
            {
                Date = DateTime.Today.AddDays(1),
                MinDate = DateTime.Today.AddDays(1),
                MaxDate = DateTime.Today.AddDays(20),
                CoachId = coach.Id,
                Groups = await groupTrainingsDAL.GetGroupListAsync(),
                TrainingRooms = await groupTrainingsDAL.GetTrainingRoomListAsync()
            };

            return View(addViewModel);
        }

        [Authorize(Roles = "Coach")]
        [HttpPost]
        public async Task<ActionResult> CreateAsync(AddGroupTrainingViewModel model)
        {
            model.Groups = await groupTrainingsDAL.GetGroupListAsync();
            model.TrainingRooms = await groupTrainingsDAL.GetTrainingRoomListAsync();

            if (!ModelState.IsValid)
                return View(model);

            if (!DataChecker.CheckIsDateAvailable(
                    await groupTrainingsDAL.GetPersonalTrainingSimpleListAsync(),
                    await groupTrainingsDAL.GetGroupTrainingSimpleListAsync(),
                    model.Date,
                    model.CoachId,
                    model.TrainingRoomId,
                    await groupTrainingsDAL.GetClientsByGroupId(model.GroupId),
                    model.GroupId
                    ))
            {
                ModelState.AddModelError(string.Empty, "This time is not available");
                return View(model);
            }

            await groupTrainingsDAL.AddGroupTrainingAsync(model);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<RedirectToActionResult> DeleteAsync(int? id)
        {
            if (id != null)
                await groupTrainingsDAL.DeleteGroupTrainingAsync((int)id);

            return RedirectToAction("Index");
        }
    }
}
