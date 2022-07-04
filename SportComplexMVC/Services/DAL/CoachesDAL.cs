using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportComplexMVC.Models.DataDb;
using SportComplexMVC.Models.Entities;
using SportComplexMVC.Models.ViewModels;
using SportComplexMVC.Enums;

namespace SportComplexMVC.Services.DAL
{
    public class CoachesDAL : EntityDAL
    {
        protected readonly UserManager<ApplicationUser> userManager;

        public CoachesDAL(UserManager<ApplicationUser> userManager, ApplicationContext context)
            : base(context)
        {
            this.userManager = userManager;
        }

        public async Task<List<Coach>> GetCoachListAsync()
        {
            List<Coach> coaches = await db.Coaches
                .Include(c => c.Position)
                .Include(c => c.Specialization)
                .Include(c => c.ApplicationUser)
                .ThenInclude(p => p.Gender)
                .AsNoTracking()
                .ToListAsync();

            return coaches;
        }

        public async Task<Coach> GetCoachByIdAsync(int id)
        {
            Coach coach = await db.Coaches
                .Include(c => c.Position)
                .Include(c => c.Specialization)
                .Include(c => c.ApplicationUser)
                .ThenInclude(p => p.Gender)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();

            return coach;
        }

        public async Task<IdentityResult> AddCoachAsync(AddCoachViewModel model)
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
                await userManager.AddToRoleAsync(user, RoleEnum.Coach.ToString());

                Coach coach = new Coach(model.PositionId, model.SpecializationId, user.Id);
                db.Coaches.Add(coach);

                await db.SaveChangesAsync();
            }

            return result;
        }

        public async Task EditCoachAsync(EditCoachViewModel model)
        {
            Coach coach = await db.Coaches
                .Include(c => c.ApplicationUser)
                .ThenInclude(p => p.Gender)
                .Where(c => c.Id == model.Id)
                .FirstOrDefaultAsync();

            coach.ApplicationUser.FirstName = model.FirstName;
            coach.ApplicationUser.LastName = model.LastName;
            coach.ApplicationUser.BirthDate = model.BirthDate;
            coach.ApplicationUser.Email = model.Email;
            coach.ApplicationUser.PhoneNumber = model.PhoneNumber;
            coach.ApplicationUser.GenderId = model.GenderId;
            coach.PositionId = model.PositionId;
            coach.SpecializationId = model.SpecializationId;

            db.Attach(coach).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task DeleteCoachAsync(int id)
        {
            Coach coach = await db.Coaches.FindAsync(id);

            if (coach != null)
            {
                ApplicationUser user = await db.Users.FindAsync(coach.ApplicationUserId);

                db.Coaches.Remove(coach);
                db.Users.Remove(user);
                await db.SaveChangesAsync();
            }
        }
    }
}
