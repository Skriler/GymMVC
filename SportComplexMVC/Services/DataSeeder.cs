using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SportComplexMVC.Enums;
using SportComplexMVC.Models.Entities;

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

            foreach (ApplicationUser userForSeed in usersForSeed)
            {
                if (userManager.Users.All(u => u.Id != userForSeed.Id))
                {
                    var user = await userManager.FindByEmailAsync(userForSeed.Email);

                    if (user == null)
                    {
                        await userManager.CreateAsync(userForSeed, defaultPassword);

                        foreach (RoleEnum role in Enum.GetValues(typeof(RoleEnum)))
                        {
                            await userManager.AddToRoleAsync(userForSeed, role.ToString());
                        }
                    }

                }
            }
        }
    }
}
