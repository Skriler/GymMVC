using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SportComplexMVC.Services;
using SportComplexMVC.Models.Entities;

namespace SportComplexMVC.Models.DataDb
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Position> Positions { get; set; } 
        public DbSet<Specialization> Specializations { get; set; }
        public DbSet<TrainingRoom> TrainingRooms { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Gender>().HasData(
                DataCreator.GetGenderList()
                );

            builder.Entity<Position>().HasData(
                DataCreator.GetPositionList()
                );

            builder.Entity<Specialization>().HasData(
                DataCreator.GetSpecializationList()
                );

            //builder.Entity<User>().HasData(
            //    DataCreator.GetUserList()
            //    );

            //builder.Entity<Coach>().HasData(
            //    DataCreator.GetCoachList()
            //    );

            builder.Entity<TrainingRoom>().HasData(
                DataCreator.GetTrainingRoomList()
                );
        }
    }
}
