using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportComplexMVC.Models.Entities;

namespace SportComplexMVC.Services
{
    public static class DataChecker
    {
        public static bool CheckIsCoachAvailable(List<PersonalTraining> trainings, DateTime selectedDate, int coachId)
        {
            foreach(PersonalTraining training in trainings)
            {
                if (training.Date == selectedDate && training.CoachId == coachId)
                    return false;
            }

            return true;
        }

        public static bool CheckIsRoomAvailable(List<PersonalTraining> trainings, DateTime selectedDate, int roomId)
        {
            foreach (PersonalTraining training in trainings)
            {
                if (training.Date == selectedDate && training.TrainingRoomId == roomId)
                    return false;
            }

            return true;
        }
    }
}
