using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportComplexMVC.Models.DataDb;
using SportComplexMVC.Models.Entities;

namespace SportComplexMVC.Services.DAL
{
    public abstract class EntityDAL
    {
        protected readonly UserManager<ApplicationUser> userManager;
        protected readonly ApplicationContext db;

        public EntityDAL(UserManager<ApplicationUser> userManager, ApplicationContext context)
        {
            this.userManager = userManager;
            db = context;
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
    }
}
