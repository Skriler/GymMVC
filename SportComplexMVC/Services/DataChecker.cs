using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using SportComplexMVC.Models.Entities;

namespace SportComplexMVC.Services
{
    public static class DataChecker
    {
        public static bool CheckIsDateAvailable(
            List<PersonalTraining> personalTrainings, 
            List<GroupTraining> groupTrainings, 
            DateTime selectedDate, 
            int coachId, 
            int roomId,
            List<Client> clients,
            int groupId
            )
        {
            foreach(PersonalTraining personalTraining in personalTrainings)
            {
                if (personalTraining.Date == selectedDate && personalTraining.CoachId == coachId)
                    return false;

                if (personalTraining.Date == selectedDate && personalTraining.TrainingRoomId == roomId)
                    return false;

                if (personalTraining.Date == selectedDate && !CheckIsClientsAvailableAtDate(personalTraining, clients))
                    return false; 
            }

            foreach (GroupTraining groupTraining in groupTrainings)
            {
                if (groupTraining.Date == selectedDate && groupTraining.CoachId == coachId)
                    return false;

                if (groupTraining.Date == selectedDate && groupTraining.TrainingRoomId == roomId)
                    return false;

                if (groupTraining.Date == selectedDate && groupTraining.GroupId == groupId)
                    return false;
            }

            return true;
        }

        private static bool CheckIsClientsAvailableAtDate(PersonalTraining personalTraining, List<Client> clients)
        {
            foreach (Client client in clients)
            {
                if (personalTraining.ClientId == client.Id)
                    return false;
            }

            return true;
        }

        public static bool CheckIsCurrentClientInGroup(Client client)
        {
            if (client.GroupId == null)
                return false;

            return true;
        }
    }
}
