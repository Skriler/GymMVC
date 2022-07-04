using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportComplexMVC.Models.DataDb;
using SportComplexMVC.Models.Entities;
using SportComplexMVC.Models.ViewModels;

namespace SportComplexMVC.Services.DAL
{
    public class GroupTrainingsDAL : EntityDAL
    {
        public GroupTrainingsDAL(ApplicationContext context)
            : base(context)
        { }

        public async Task<List<GroupTraining>> GetGroupTrainingListAsync()
        {
            List<GroupTraining> groupTrainings = await db.GroupTrainings
                .Include(p => p.Coach)
                .ThenInclude(c => c.ApplicationUser)
                .Include(p => p.Group)
                .Include(p => p.TrainingRoom)
                .AsNoTracking()
                .ToListAsync();

            return groupTrainings;
        }

        public async Task AddGroupTrainingAsync(AddGroupTrainingViewModel model)
        {
            GroupTraining groupTraining = new GroupTraining(
                model.Date,
                model.CoachId,
                model.GroupId,
                model.TrainingRoomId
                );

            db.GroupTrainings.Add(groupTraining);
            await db.SaveChangesAsync();
        }

        public async Task DeleteGroupTrainingAsync(int id)
        {
            GroupTraining groupTraining = await db.GroupTrainings.FindAsync(id);

            if (groupTraining != null)
            {
                db.GroupTrainings.Remove(groupTraining);
                await db.SaveChangesAsync();
            }
        }
    }
}
