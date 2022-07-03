using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using SportComplexMVC.Models.Entities;
using SportComplexMVC.Models.DataDb;
using SportComplexMVC.Models.ViewModels;
using SportComplexMVC.Services.DAL;

namespace SportComplexMVC.Controllers
{
    public class ClientsController : Controller
    {
        private ClientsDAL clientsDAL;

        public ClientsController(UserManager<ApplicationUser> userManager, ApplicationContext context)
        {
            clientsDAL = new ClientsDAL(userManager, context);
        }

        [HttpGet]
        public async Task<ViewResult> IndexAsync()
        {
            List<Client> clients = await clientsDAL.GetClientListAsync();

            return View(clients);
        }

        [HttpGet]
        public async Task<ActionResult> DetailsAsync(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            Client client = await clientsDAL.GetClientByIdAsync(id);

            if (client == null)
                return RedirectToAction("Index");

            return View(client);
        }

        [HttpGet]
        public async Task<ViewResult> CreateAsync()
        {
            AddClientViewModel clientViewModel = new AddClientViewModel()
            {
                Genders = await clientsDAL.GetGenderListAsync(),
                ClientStatuses = await clientsDAL.GetClientStatusListAsync()
            };

            return View(clientViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> CreateAsync(AddClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await clientsDAL.AddClientAsync(model);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Clients");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            model.Genders = await clientsDAL.GetGenderListAsync();
            model.ClientStatuses = await clientsDAL.GetClientStatusListAsync();

            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> EditAsync(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            Client client = await clientsDAL.GetClientByIdAsync(id);

            if (client == null)
                return RedirectToAction("Index");

            EditClientViewModel clientViewModel = new EditClientViewModel()
            {
                Id = client.Id,
                FirstName = client.ApplicationUser.FirstName,
                LastName = client.ApplicationUser.LastName,
                BirthDate = client.ApplicationUser.BirthDate,
                Email = client.ApplicationUser.Email,
                PhoneNumber = client.ApplicationUser.PhoneNumber,
                GenderId = client.ApplicationUser.GenderId,
                ClientStatusId = client.ClientStatusId,
                Genders = await clientsDAL.GetGenderListAsync(),
                ClientStatuses = await clientsDAL.GetClientStatusListAsync(),
            };

            return View(clientViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditAsync(EditClientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Genders = await clientsDAL.GetGenderListAsync();
                model.ClientStatuses = await clientsDAL.GetClientStatusListAsync();
                return View(model);
            }

            await clientsDAL.EditClientAsync(model);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<RedirectToActionResult> DeleteAsync(int? id)
        {
            if (id != null)
                await clientsDAL.DeleteClientAsync(id);

            return RedirectToAction("Index");
        }
    }
}
