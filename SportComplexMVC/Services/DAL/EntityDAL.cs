using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportComplexMVC.Models.DataDb;
using SportComplexMVC.Models.Entities;
using SportComplexMVC.Enums;


namespace SportComplexMVC.Services.DAL
{
    public abstract class EntityDAL
    {
        protected readonly UserManager<ApplicationUser> userManager;
        protected readonly ApplicationContext db;

        public EntityDAL(ApplicationContext context)
        {
            db = context;
        }

        public EntityDAL(UserManager<ApplicationUser> userManager, ApplicationContext context)
            : this(context)
        {
            this.userManager = userManager;
        }

        public async Task<List<Gender>> GetGenderListAsync()
        {
            return await db.Genders.AsNoTracking().ToListAsync();
        }

        public async Task<List<Position>> GetPositionListAsync()
        {
            return await db.Positions.AsNoTracking().ToListAsync();
        }

        public async Task<List<Specialization>> GetSpecializationListAsync()
        {
            return await db.Specializations.AsNoTracking().ToListAsync();
        }

        public async Task<List<ClientStatus>> GetClientStatusListAsync()
        {
            return await db.ClientStatuses.AsNoTracking().ToListAsync();
        }

        public async Task<List<TrainingRoom>> GetTrainingRoomListAsync()
        {
            return await db.TrainingRooms.AsNoTracking().ToListAsync();
        }

        public async Task<List<Group>> GetGroupListAsync()
        {
            return await db.Groups.AsNoTracking().ToListAsync();
        }

        public async Task<List<PersonalTraining>> GetPersonalTrainingSimpleListAsync()
        {
            return await db.PersonalTrainings.AsNoTracking().ToListAsync();
        }

        public async Task<List<GroupTraining>> GetGroupTrainingSimpleListAsync()
        {
            return await db.GroupTrainings.AsNoTracking().ToListAsync();
        }

        public async Task<Client> GetCurrentClient(ClaimsPrincipal currentUser)
        {
            Client client = null;

            if (currentUser.IsInRole(RoleEnum.Client.ToString()))
            {
                string userId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

                client = await db.Clients
                    .Include(c => c.ApplicationUser)
                    .Where(u => u.ApplicationUser.Id == userId)
                    .FirstOrDefaultAsync();
            }

            return client;
        }

        public async Task<Coach> GetCurrentCoach(ClaimsPrincipal currentUser)
        {
            Coach coach = null;

            if (currentUser.IsInRole(RoleEnum.Coach.ToString()))
            {
                string userId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

                coach = await db.Coaches
                    .Include(c => c.ApplicationUser)
                    .Where(u => u.ApplicationUser.Id == userId)
                    .FirstOrDefaultAsync();
            }

            return coach;
        }

        public async Task<int> GetGroupIdByClient(ClaimsPrincipal currentUser)
        {
            Client client = await GetCurrentClient(currentUser);

            if (client.GroupId == null)
                return -1;

            return (int)client.GroupId;
        }

        public async Task<List<Client>> GetClientsByGroupId(int groupId)
        {
            List<Client> clients = await db.Clients
                .Where(c => c.GroupId == groupId)
                .AsNoTracking()
                .ToListAsync();

            return clients;
        }
    }
}
