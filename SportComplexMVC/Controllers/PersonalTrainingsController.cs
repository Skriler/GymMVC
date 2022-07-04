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
using SportComplexMVC.Enums;

namespace SportComplexMVC.Controllers
{
    [Authorize]
    public class PersonalTrainingsController : Controller
    {
        private PersonalTrainingsDAL personalTrainingsDAL;

        public PersonalTrainingsController(ApplicationContext context)
        {
            personalTrainingsDAL = new PersonalTrainingsDAL(context);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ViewResult> IndexAsync()
        {
            List<PersonalTraining> personalTrainings = await personalTrainingsDAL.GetPersonalTrainingListAsync();

            return View(personalTrainings);
        }

        [Authorize(Roles = "Client, Coach")]
        [HttpGet]
        public async Task<ActionResult> TrainingsByCurrentUser()
        {
            List<PersonalTraining> personalTrainings;

            if (User.IsInRole(RoleEnum.Client.ToString()))
            {
                Client client = await personalTrainingsDAL.GetCurrentClient(User);

                if (client == null)
                    return RedirectToAction("Index", "Home");

                personalTrainings = await personalTrainingsDAL.GetPersonalTrainingListByClientAsync(client.Id);
            }
            else
            {
                Coach coach = await personalTrainingsDAL.GetCurrentCoach(User);

                if (coach == null)
                    return RedirectToAction("Index", "Home");

                personalTrainings = await personalTrainingsDAL.GetPersonalTrainingListByCoachAsync(coach.Id);
            }

            return View(personalTrainings);
        }

        [Authorize(Roles = "Client")]
        [HttpGet]
        public async Task<ActionResult> CreateAsync(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");

            Client client = await personalTrainingsDAL.GetCurrentClient(User);

            AddPersonalTrainingViewModel addViewModel = new AddPersonalTrainingViewModel()
            {
                Date = DateTime.Today.AddDays(1),
                MinDate = DateTime.Today.AddDays(1),
                MaxDate = DateTime.Today.AddDays(20),
                ClientId = client.Id,
                CoachId = (int)id,
                TrainingRooms = await personalTrainingsDAL.GetTrainingRoomListAsync()
            };

            return View(addViewModel);
        }

        [Authorize(Roles = "Client")]
        [HttpPost]
        public async Task<ActionResult> CreateAsync(AddPersonalTrainingViewModel model)
        {
            model.TrainingRooms = await personalTrainingsDAL.GetTrainingRoomListAsync();

            if (!ModelState.IsValid)
                return View(model);

            if (!DataChecker.CheckIsDateAvailable(
                    await personalTrainingsDAL.GetPersonalTrainingSimpleListAsync(),
                    await personalTrainingsDAL.GetGroupTrainingSimpleListAsync(),
                    model.Date, 
                    model.CoachId,
                    model.TrainingRoomId,
                    new List<Client> { personalTrainingsDAL.GetCurrentClient(User).Result } ,
                    await personalTrainingsDAL.GetGroupIdByClient(User)
                    ))
            {
                ModelState.AddModelError(string.Empty, "This time is not available");
                return View(model);
            }

            await personalTrainingsDAL.AddPersonalTrainingAsync(model);

            return RedirectToAction("TrainingsByCurrentUser", "PersonalTrainings");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<RedirectToActionResult> DeleteAsync(int? id)
        {
            if (id != null)
                await personalTrainingsDAL.DeletePersonalTrainingAsync((int)id);

            return RedirectToAction("Index");
        }
    }
}
