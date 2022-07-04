using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using SportComplexMVC.Models.Entities;
using SportComplexMVC.Models.DataDb;
using SportComplexMVC.Models.ViewModels;
using SportComplexMVC.Services.DAL;

namespace SportComplexMVC.Controllers
{
    public class CoachesController : Controller
    {
        private CoachesDAL coachesDAL;

        public CoachesController(UserManager<ApplicationUser> userManager, ApplicationContext context)
        {
            coachesDAL = new CoachesDAL(userManager, context);
        }

        [HttpGet]
        public async Task<ViewResult> IndexAsync()
        {
            List<Coach> coaches = await coachesDAL.GetCoachListAsync();

            return View(coaches);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> DetailsAsync(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            Coach coach = await coachesDAL.GetCoachByIdAsync((int)id);

            if (coach == null)
                return RedirectToAction("Index");

            return View(coach);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ViewResult> CreateAsync()
        {
            AddCoachViewModel coachViewModel = new AddCoachViewModel()
            {
                Genders = await coachesDAL.GetGenderListAsync(),
                Positions = await coachesDAL.GetPositionListAsync(),
                Specializations = await coachesDAL.GetSpecializationListAsync()
            };

            return View(coachViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> CreateAsync(AddCoachViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await coachesDAL.AddCoachAsync(model);
                
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Coaches");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            model.Genders = await coachesDAL.GetGenderListAsync();
            model.Positions = await coachesDAL.GetPositionListAsync();
            model.Specializations = await coachesDAL.GetSpecializationListAsync();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult> EditAsync(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            Coach coach = await coachesDAL.GetCoachByIdAsync((int)id);

            if (coach == null)
                return RedirectToAction("Index");

            EditCoachViewModel coachViewModel = new EditCoachViewModel()
            {
                Id = coach.Id,
                FirstName = coach.ApplicationUser.FirstName,
                LastName = coach.ApplicationUser.LastName,
                BirthDate = coach.ApplicationUser.BirthDate,
                Email = coach.ApplicationUser.Email,
                PhoneNumber = coach.ApplicationUser.PhoneNumber,
                GenderId = coach.ApplicationUser.GenderId,
                PositionId = coach.PositionId,
                SpecializationId = coach.SpecializationId,
                Genders = await coachesDAL.GetGenderListAsync(),
                Positions = await coachesDAL.GetPositionListAsync(),
                Specializations = await coachesDAL.GetSpecializationListAsync()
            };

            return View(coachViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> EditAsync(EditCoachViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genders = await coachesDAL.GetGenderListAsync();
                model.Positions = await coachesDAL.GetPositionListAsync();
                model.Specializations = await coachesDAL.GetSpecializationListAsync();
                return View(model);
            }

            await coachesDAL.EditCoachAsync(model);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<RedirectToActionResult> DeleteAsync(int? id)
        {
            if (id != null)
                await coachesDAL.DeleteCoachAsync((int)id);

            return RedirectToAction("Index");
        }
    }
}
