using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportComplexMVC.Models.DataDb;
using SportComplexMVC.Models.Entities;
using SportComplexMVC.Models.ViewModels;
using SportComplexMVC.Enums;

namespace SportComplexMVC.Services.DAL
{
    public class ClientsDAL : EntityDAL
    {
        protected readonly UserManager<ApplicationUser> userManager;

        public ClientsDAL(UserManager<ApplicationUser> userManager, ApplicationContext context)
            : base(context)
        {
            this.userManager = userManager;
        }

        public async Task<List<Client>> GetClientListAsync()
        {
            List<Client> clients = await db.Clients
                .Include(c => c.ClientStatus)
                .Include(c => c.ApplicationUser)
                .ThenInclude(p => p.Gender)
                .AsNoTracking()
                .ToListAsync();

            return clients;
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            Client client = await db.Clients
                .Include(c => c.ClientStatus)
                .Include(c => c.ApplicationUser)
                .ThenInclude(p => p.Gender)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            return client;
        }

        public async Task<IdentityResult> AddClientAsync(AddClientViewModel model)
        {
            ApplicationUser user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                BirthDate = model.BirthDate,
                Email = model.Email,
                UserName = model.Email,
                PhoneNumber = model.PhoneNumber,
                GenderId = model.GenderId,
            };

            IdentityResult result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, RoleEnum.Client.ToString());

                Client client = new Client(model.ClientStatusId, user.Id);
                db.Clients.Add(client);

                await db.SaveChangesAsync();
            }

            return result;
        }

        public async Task EditClientAsync(EditClientViewModel model)
        {
            Client client = await db.Clients
                .Include(c => c.ApplicationUser)
                .ThenInclude(p => p.Gender)
                .Where(c => c.Id == model.Id)
                .FirstOrDefaultAsync();

            client.ApplicationUser.FirstName = model.FirstName;
            client.ApplicationUser.LastName = model.LastName;
            client.ApplicationUser.BirthDate = model.BirthDate;
            client.ApplicationUser.Email = model.Email;
            client.ApplicationUser.PhoneNumber = model.PhoneNumber;
            client.ApplicationUser.GenderId = model.GenderId;
            client.ClientStatusId = model.ClientStatusId;

            db.Attach(client).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task DeleteClientAsync(int id)
        {
            Client client = await db.Clients.FindAsync(id);

            if (client != null)
            {
                ApplicationUser user = await db.Users.FindAsync(client.ApplicationUserId);

                db.Clients.Remove(client);
                db.Users.Remove(user);
                await db.SaveChangesAsync();
            }
        }

        public async Task DeleteClientFromGroupAsync(int clientId)
        {
            Client client = await db.Clients.FindAsync(clientId);

            if (client != null)
            {
                client.GroupId = null;
                await db.SaveChangesAsync();
            }
        }

        public async Task AddClientToGroupAsync(ClaimsPrincipal currentUser, int groupId)
        {
            Client client = await GetCurrentClientAsync(currentUser);

            if (client != null)
            {
                client.GroupId = groupId;
                await db.SaveChangesAsync();
            }
        }
    }
}
