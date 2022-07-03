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

        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (RoleEnum role in Enum.GetValues(typeof(RoleEnum)))
            {
                await roleManager.CreateAsync(new IdentityRole(role.ToString()));
            }
        }

        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            List<ApplicationUser> usersForSeed = DataCreator.GetUserList();
            RoleEnum currentRole = RoleEnum.Client;
            int counter = 1;

            foreach (ApplicationUser userForSeed in usersForSeed)
            {
                if (userManager.Users.All(u => u.Id != userForSeed.Id))
                {
                    var user = await userManager.FindByEmailAsync(userForSeed.Email);

                    if (user == null)
                    {
                        var result = await userManager.CreateAsync(userForSeed, defaultPassword);

                        if (counter++ <= 5)
                            currentRole = RoleEnum.Coach;
                        else
                            currentRole = RoleEnum.Client;

                        await userManager.AddToRoleAsync(userForSeed, currentRole.ToString());
                    }

                }
            }
        }

        public static async Task SeedCoachesAndClientsAsync(ApplicationContext db)
        {
            DataCreator.SetUserList(await db.Users.AsNoTracking().ToListAsync());

            bool anyCoaches = await db.Coaches.AnyAsync();
            if (!anyCoaches)
                await db .Coaches.AddRangeAsync(DataCreator.GetCoachList());

            bool anyClients = await db.Clients.AnyAsync();
            if (!anyClients)
                await db.Clients.AddRangeAsync(DataCreator.GetClientList());

            await db.SaveChangesAsync();
        }
    }
}
