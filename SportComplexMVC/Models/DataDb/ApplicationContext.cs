using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using SportComplexMVC.Services;
using SportComplexMVC.Models.Entities;

namespace SportComplexMVC.Models.DataDb
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Position> Positions { get; set; } 
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<ClientStatus> ClientStatuses { get; set; }
        public DbSet<TrainingRoom> TrainingRooms { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<PersonalTraining> PersonalTrainings { get; set; }
        public DbSet<GroupTraining> GroupTrainings { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().ToTable(name: "Users");
            builder.Entity<IdentityRole>().ToTable(name: "Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable(name: "UserRoles");

            builder.Entity<Gender>().HasData(
                DataCreator.GetGenderList()
                );

            builder.Entity<Position>().HasData(
                DataCreator.GetPositionList()
                );

            builder.Entity<Specialization>().HasData(
                DataCreator.GetSpecializationList()
                );

            builder.Entity<ClientStatus>().HasData(
                DataCreator.GetClientStatusList()
                );

            builder.Entity<TrainingRoom>().HasData(
                DataCreator.GetTrainingRoomList()
                );
        }
    }
}
