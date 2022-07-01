using Microsoft.EntityFrameworkCore;
using GymMVC.Services;
using GymMVC.Models.Entities;

namespace GymMVC.Models.DataDb
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<PersonInfo> PeopleInfo { get; set; }
        public DbSet<Gender> Genders { get; set; } 
        public DbSet<Position> Positions { get; set; } 
        public DbSet<Specialization> Specializations { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Gender>().HasData(
                DataHelper.GetGenderList()
                );

            builder.Entity<Position>().HasData(
                DataHelper.GetPositionList()
                );

            builder.Entity<Specialization>().HasData(
                DataHelper.GetSpecializationList()
                );

            builder.Entity<PersonInfo>().HasData(
                DataHelper.GetPersonInfoList()
                );

            builder.Entity<Coach>().HasData(
                DataHelper.GetCoachList()
                );
        }
    }
}
