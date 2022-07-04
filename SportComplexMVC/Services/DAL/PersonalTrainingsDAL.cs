using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SportComplexMVC.Models.DataDb;
using SportComplexMVC.Models.Entities;
using SportComplexMVC.Models.ViewModels;

namespace SportComplexMVC.Services.DAL
{
    public class PersonalTrainingsDAL : EntityDAL
    {
        public PersonalTrainingsDAL(ApplicationContext context)
            : base(context)
        { }

        public async Task<List<PersonalTraining>> GetPersonalTrainingListAsync()
        {
            List<PersonalTraining> personalTrainings = await db.PersonalTrainings
                .Include(p => p.Coach)
                .ThenInclude(c => c.ApplicationUser)
                .Include(p => p.Client)
                .ThenInclude(c => c.ApplicationUser)
                .Include(p => p.TrainingRoom)
                .AsNoTracking()
                .ToListAsync();

            return personalTrainings;
        }

        public async Task<List<PersonalTraining>> GetPersonalTrainingListByClientAsync(int clientId)
        {
            List<PersonalTraining> personalTrainings = await db.PersonalTrainings
                .Include(p => p.Coach)
                .ThenInclude(c => c.ApplicationUser)
                .Include(p => p.TrainingRoom)
                .Where(p => p.ClientId == clientId)
                .AsNoTracking()
                .ToListAsync();

            return personalTrainings;
        }

        public async Task<List<PersonalTraining>> GetPersonalTrainingListByCoachAsync(int coachId)
        {
            List<PersonalTraining> personalTrainings = await db.PersonalTrainings
                .Include(p => p.Client)
                .ThenInclude(c => c.ApplicationUser)
                .Include(p => p.TrainingRoom)
                .Where(p => p.CoachId == coachId)
                .AsNoTracking()
                .ToListAsync();

            return personalTrainings;
        }

        public async Task AddPersonalTrainingAsync(AddPersonalTrainingViewModel model)
        {
            PersonalTraining personalTraining = new PersonalTraining(
                model.Date, 
                model.CoachId, 
                model.ClientId, 
                model.TrainingRoomId
                );

            db.PersonalTrainings.Add(personalTraining);
            await db.SaveChangesAsync();
        }

        public async Task DeletePersonalTrainingAsync(int id)
        {
            PersonalTraining personalTraining = await db.PersonalTrainings.FindAsync(id);

            if (personalTraining != null)
            {
                db.PersonalTrainings.Remove(personalTraining);
                await db.SaveChangesAsync();
            }
        }
    }
}
