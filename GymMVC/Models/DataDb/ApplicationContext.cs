using Microsoft.EntityFrameworkCore;
using GymMVC.Services;
using GymMVC.Models.Entities;

namespace GymMVC.Models.DataDb
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<User> Users { get; set; }
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
            builder.Entity<Gender>().HasData(
                DataCreator.GetGenderList()
                );

            builder.Entity<Position>().HasData(
                DataCreator.GetPositionList()
                );

            builder.Entity<Specialization>().HasData(
                DataCreator.GetSpecializationList()
                );

            builder.Entity<User>().HasData(
                DataCreator.GetUserList()
                );

            builder.Entity<Coach>().HasData(
                DataCreator.GetCoachList()
                );

            builder.Entity<TrainingRoom>().HasData(
                DataCreator.GetTrainingRoomList()
                );
        }
    }
}
