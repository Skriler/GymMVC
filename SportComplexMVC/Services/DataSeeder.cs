using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportComplexMVC.Enums;
using SportComplexMVC.Models.Entities;
using SportComplexMVC.Models.DataDb;

namespace SportComplexMVC.Services
{
    public static class DataSeeder
    {
        private static readonly string defaultPassword = "Sa333*";

        public static async Task SeedDatabase(
            RoleManager<IdentityRole> roleManager, 
            UserManager<ApplicationUser> userManager, 
            ApplicationContext db)
        {
            await SeedRolesAsync(roleManager);
            await SeedUsersAsync(userManager);
            await SeedUserRolesAsync(userManager, db);
            await SeedCoachesAndClientsAsync(db);
            await SeedPersonalAndGroupTrainings(db);
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (RoleEnum role in Enum.GetValues(typeof(RoleEnum)))
            {
                await roleManager.CreateAsync(new IdentityRole(role.ToString()));
            }
        }

        private static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            List<ApplicationUser> usersForSeed = DataCreator.GetUserList();

            foreach (ApplicationUser userForSeed in usersForSeed)
            {
                if (userManager.Users.All(u => u.Id != userForSeed.Id))
                {
                    var user = await userManager.FindByEmailAsync(userForSeed.Email);

                    if (user == null)
                    {
                        var result = await userManager.CreateAsync(userForSeed, defaultPassword);
                    }

                }
            }
        }

        private static async Task SeedUserRolesAsync(UserManager<ApplicationUser> userManager, ApplicationContext db)
        {
            List<ApplicationUser> users = await db.Users.ToListAsync();
            

            RoleEnum currentRole;

            for (int i = 0; i < users.Count; ++i)
            {
                currentRole = i switch
                {
                    < 5 => RoleEnum.Coach,
                    < 20 => RoleEnum.Client,
                    _ => RoleEnum.Admin
                };

                await userManager.AddToRoleAsync(users[i], currentRole.ToString());
            }
        }

        private static async Task SeedCoachesAndClientsAsync(ApplicationContext db)
        {
            DataCreator.SetUserList(await db.Users.AsNoTracking().ToListAsync());

            bool anyCoaches = await db.Coaches.AnyAsync();
            if (!anyCoaches)
                await db.Coaches.AddRangeAsync(DataCreator.GetCoachList());

            bool anyClients = await db.Clients.AnyAsync();
            if (!anyClients)
                await db.Clients.AddRangeAsync(DataCreator.GetClientList());

            await db.SaveChangesAsync();
        }

        private static async Task SeedPersonalAndGroupTrainings(ApplicationContext db)
        {
            bool anyPersonalTrainings = await db.PersonalTrainings.AnyAsync();
            if (!anyPersonalTrainings)
                await db.PersonalTrainings.AddRangeAsync(DataCreator.GetPersonalTrainingList());

            //bool anyGroupTrainings = await db.GroupTrainings.AnyAsync();
            //if (!anyGroupTrainings)
            //    await db.GroupTrainings.AddRangeAsync(DataCreator.GetGroupTrainingList());

            await db.SaveChangesAsync();
        }
    }
}
