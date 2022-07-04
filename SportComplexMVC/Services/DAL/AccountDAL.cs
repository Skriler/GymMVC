using System;
using SportComplexMVC.Enums;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportComplexMVC.Models.DataDb;
using SportComplexMVC.Models.Entities;
using SportComplexMVC.Models.ViewModels;

namespace SportComplexMVC.Services.DAL
{
    public class AccountDAL : EntityDAL
    {
        protected readonly UserManager<ApplicationUser> userManager;
        protected readonly SignInManager<ApplicationUser> signInManager;

        public AccountDAL(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationContext context)
            : base(context)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterViewModel model)
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
                await signInManager.SignInAsync(user, false);

                Client client = new Client(1, user.Id);
                db.Clients.Add(client);
                await db.SaveChangesAsync();
            }

            return result;
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            SignInResult result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            return result;
        }

        public async Task LogoutAsync()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<ProfileViewModel> GetClientProfileViewModelAsync(ClaimsPrincipal currentUser)
        {
            string userId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            Client client = await db.Clients
                .Include(c => c.ClientStatus)
                .Include(c => c.Group)
                .Include(c => c.ApplicationUser)
                .ThenInclude(u => u.Gender)
                .Where(u => u.ApplicationUser.Id == userId)
                .FirstOrDefaultAsync();

            ProfileViewModel profileViewModel = new ProfileViewModel()
            {
                Id = client.Id,
                FirstName = client.ApplicationUser.FirstName,
                LastName = client.ApplicationUser.LastName,
                BirthDate = client.ApplicationUser.BirthDate,
                Email = client.ApplicationUser.Email,
                PhoneNumber = client.ApplicationUser.PhoneNumber,
                Gender = client.ApplicationUser.Gender.Title,
                ClientStatus = client.ClientStatus.Title,
            };

            if (client.Group != null)
                profileViewModel.Group = client.Group.Title;

            return profileViewModel;
        }

        public async Task<ProfileViewModel> GetCoachProfileViewModelAsync(ClaimsPrincipal currentUser)
        {
            string userId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            Coach coach = await db.Coaches
                .Include(c => c.Position)
                .Include(c => c.Specialization)
                .Include(c => c.ApplicationUser)
                .ThenInclude(u => u.Gender)
                .Where(u => u.ApplicationUser.Id == userId)
                .FirstOrDefaultAsync();

            ProfileViewModel profileViewModel = new ProfileViewModel()
            {
                FirstName = coach.ApplicationUser.FirstName,
                LastName = coach.ApplicationUser.LastName,
                BirthDate = coach.ApplicationUser.BirthDate,
                Email = coach.ApplicationUser.Email,
                PhoneNumber = coach.ApplicationUser.PhoneNumber,
                Gender = coach.ApplicationUser.Gender.Title,
                Position = coach.Position.Title,
                Specialization = coach.Specialization.Title
            };

            return profileViewModel;
        }

        public async Task<ProfileViewModel> GetUserProfileViewModelAsync(ClaimsPrincipal currentUser)
        {
            string userId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            ApplicationUser user = await db.Users
                .Include(c => c.Gender)
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            ProfileViewModel profileViewModel = new ProfileViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender.Title
            };

            return profileViewModel;
        }

        public async Task<EditProfileViewModel> GetEditUserProfileViewModelAsync(ClaimsPrincipal currentUser)
        {
            string userId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            ApplicationUser user = await db.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            EditProfileViewModel editProfileViewModel = new EditProfileViewModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                MinBirthDate = DateTime.Now.AddYears(-70),
                MaxBirthDate = DateTime.Now.AddYears(-18),
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };

            return editProfileViewModel;
        }

        public async Task EditUserAsync(ClaimsPrincipal currentUser, EditProfileViewModel model)
        {
            string userId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            ApplicationUser user = await db.Users
                .Where(u => u.Id == userId)
                .FirstOrDefaultAsync();

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.BirthDate = model.BirthDate;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            db.Attach(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
