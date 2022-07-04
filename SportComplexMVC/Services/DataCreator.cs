using System;
using System.Collections.Generic;
using SportComplexMVC.Enums;
using SportComplexMVC.Models.Entities;

namespace SportComplexMVC.Services
{
    public static class DataCreator
    {
        private static int positionsAmount;
        private static int specializationsAmount;
        private static int clientStatusesAmount;

        private static readonly Random rand;
        private static List<ApplicationUser> users;

        static DataCreator()
        {
            rand = new Random();
            users = CreateUserList();
        }

        private static List<ApplicationUser> CreateUserList()
        {
            List<ApplicationUser> peopleInfo = new List<ApplicationUser>()
            {
                new ApplicationUser("Huntley", "Predohl", DateTime.Today.AddYears(-30), "hpredohl0@icio.us", "+380-395-553-127", 1),
                new ApplicationUser("Carmen", "Mancell", DateTime.Today.AddYears(-20), "cmancell1@spotify.com", "+380-635-552-885", 1),
                new ApplicationUser("Mel", "Stoeck", DateTime.Today.AddYears(-36), "mstoeck2@telegraph.co.uk", "+380-424-555-521", 1),
                new ApplicationUser("Orsola", "Feldheim", DateTime.Today.AddYears(-19), "ofeldheim3@twitter.com", "+380-505-552-811", 2),
                new ApplicationUser("Kirbie", "Kleiser", DateTime.Today.AddYears(-32), "kkleiser4@latimes.com", "+380-396-455-517", 2),
                new ApplicationUser("Krispin", "Pires", DateTime.Today.AddYears(-24), "kpires5@wikispaces.com", "+380-950-555-957", 1),
                new ApplicationUser("Brigham", "Denisovich", DateTime.Today.AddYears(-40), "bdenisovich6@quantcast.com", "+380-505-556-541", 1),
                new ApplicationUser("Fin", "Andryszczak", DateTime.Today.AddYears(-38), "fandryszczak7@noaa.gov", "+380-395-557-732", 1),
                new ApplicationUser("Brigham", "Tonner", DateTime.Today.AddYears(-28), "btonner8@cbc.ca", "+380-505-554-179", 1),
                new ApplicationUser("Kinna", "Boutton", DateTime.Today.AddYears(-29), "kboutton9@admin.ch", "+380-635-555-321", 2),
                new ApplicationUser("Janean", "Shotboult", DateTime.Today.AddYears(-22), "jshotboult0@soundcloud.com", "+380-321-655-574", 1),
                new ApplicationUser("Erroll", "Hethron", DateTime.Today.AddYears(-34), "ehethron1@slate.com", "+380-395-554-507", 2),
                new ApplicationUser("Margot", "Thieme", DateTime.Today.AddYears(-45), "mthieme2@oaic.gov.au", "+380-395-555-783", 2),
                new ApplicationUser("Tudor", "Ancliffe", DateTime.Today.AddYears(-40), "tancliffe3@rakuten.co.jp", "+380-395-551-649", 1),
                new ApplicationUser("Tony", "Bartrap", DateTime.Today.AddYears(-53), "tbartrap4@sohu.com", "+380-395-557-306", 1),
                new ApplicationUser("Emmery", "Rue", DateTime.Today.AddYears(-39), "erue5@scientificamerican.com", "+380-635-558-014", 2),
                new ApplicationUser("Twila", "Conrart", DateTime.Today.AddYears(-23), "tconrart6@ftc.gov", "+380-635-555-884", 2),
                new ApplicationUser("Rochester", "Colleymore", DateTime.Today.AddYears(-42), "rcolleymore7@dmoz.org", "+380-505-655-569", 1),
                new ApplicationUser("Erroll", "Cardenosa", DateTime.Today.AddYears(-23), "ecardenosa8@toplist.cz", "+380-635-551-600", 1),
                new ApplicationUser("Bibbie", "Sizzey", DateTime.Today.AddYears(-35), "bsizzey9@umich.edu", "+380-234-955-551", 2),
                new ApplicationUser("Bob", "Hoiuk", DateTime.Today.AddYears(-36), "Hoiuk@gmail.com", "+380-395-552-248", 1),
            };

            return peopleInfo;
        }

        public static List<Gender> GetGenderList()
        {
            List<Gender> genders = new List<Gender>();
            int currentId = 1;

            foreach (GenderEnum gender in Enum.GetValues(typeof(GenderEnum)))
            {
                genders.Add(new Gender(currentId++, gender.ToString()));
            }

            return genders;
        }

        public static List<Position> GetPositionList()
        {
            List<Position> positions = new List<Position>();
            int currentId = 1;

            foreach (PositionEnum position in Enum.GetValues(typeof(PositionEnum)))
            {
                positions.Add(new Position(currentId++, position.ToString()));
            }

            positionsAmount = positions.Count;

            return positions;
        }

        public static List<Specialization> GetSpecializationList()
        {
            List<Specialization> specializations = new List<Specialization>();
            int currentId = 1;

            foreach (SpecializationEnum specialization in Enum.GetValues(typeof(SpecializationEnum)))
            {
                specializations.Add(new Specialization(currentId++, specialization.ToString()));
            }

            specializationsAmount = specializations.Count;

            return specializations;
        }

        public static List<ClientStatus> GetClientStatusList()
        {
            List<ClientStatus> clientStatuses = new List<ClientStatus>();
            int currentId = 1;

            foreach (ClientStatusEnum clientStatus in Enum.GetValues(typeof(ClientStatusEnum)))
            {
                clientStatuses.Add(new ClientStatus(currentId++, clientStatus.ToString()));
            }

            clientStatusesAmount = clientStatuses.Count;

            return clientStatuses;
        }

        public static List<ApplicationUser> GetUserList()
        {
            return users;
        }

        public static void SetUserList(List<ApplicationUser> users)
        {
            DataCreator.users = users;
        }

        public static List<Coach> GetCoachList()
        {
            List<Coach> coaches = new List<Coach>();

            Coach coach;
            for (int i = 0; i < 5; ++i)
            {
                coach = new Coach(rand.Next(1, positionsAmount), rand.Next(1, specializationsAmount), users[i].Id);
                coaches.Add(coach);
            }

            return coaches;
        }

        public static List<Group> GetGroupList()
        {
            List<Group> groups = new List<Group>()
            {
                new Group("NSA172"),
                new Group("HMK195"),
                new Group("LKY163"),
                new Group("BGH127"),
            };

            return groups;
        }

        public static List<Client> GetClientList(List<Group> groups)
        {
            List<Client> clients = new List<Client>();
            Client client;
            int groupNumber;

            for (int i = 0; i < 15; ++i)
            {
                groupNumber = i % groups.Count;

                client = new Client(rand.Next(1, clientStatusesAmount), users[i + 5].Id);
                client.GroupId = groups[groupNumber].Id;

                clients.Add(client);   
            }

            return clients;
        }

        public static List<TrainingRoom> GetTrainingRoomList()
        {
            List<TrainingRoom> trainingRooms = new List<TrainingRoom>()
            {
                new TrainingRoom(1, "First fitness room", 0, 5),
                new TrainingRoom(2, "Swimming pool", 0, 7),
                new TrainingRoom(3, "Volleyball Court", 0, 8),
                new TrainingRoom(4, "First gymnastic room", 1, 12),
                new TrainingRoom(5, "First yoga room", 1, 13),
                new TrainingRoom(6, "Second yoga room", 1, 18),
                new TrainingRoom(7, "Second gymnastic room", 1, 19),
                new TrainingRoom(8, "Second fitness room", 2, 22),
                new TrainingRoom(9, "Third gymnastic room", 2, 24),
                new TrainingRoom(10, "Third fitness room", 2, 27),
            };

            return trainingRooms;
        }

        public static List<PersonalTraining> GetPersonalTrainingList()
        {
            List<PersonalTraining> personalTrainings = new List<PersonalTraining>()
            {
                new PersonalTraining(DateTime.Today.AddDays(2), 2, 5, 1),
                new PersonalTraining(DateTime.Today.AddDays(2), 3, 7, 10),
                new PersonalTraining(DateTime.Today.AddDays(1), 2, 9, 3),
                new PersonalTraining(DateTime.Today.AddDays(1), 4, 2, 6),
                new PersonalTraining(DateTime.Today, 1, 1, 8),
                new PersonalTraining(DateTime.Today, 3, 10, 4),
                new PersonalTraining(DateTime.Today.AddDays(-1), 2, 11, 5),
                new PersonalTraining(DateTime.Today.AddDays(-1), 4, 6, 7),
                new PersonalTraining(DateTime.Today.AddDays(-2), 3, 8, 9),
                new PersonalTraining(DateTime.Today.AddDays(-2), 4, 2, 2),
            };

            return personalTrainings;
        }

        public static List<GroupTraining> GetGroupTrainingList()
        {
            List<GroupTraining> groupTrainings = new List<GroupTraining>()
            {
                new GroupTraining(DateTime.Today.AddDays(2), 1, 1, 2),
                new GroupTraining(DateTime.Today.AddDays(2), 4, 3, 7),
                new GroupTraining(DateTime.Today.AddDays(1), 1, 2, 7),
                new GroupTraining(DateTime.Today.AddDays(1), 3, 1, 1),
                new GroupTraining(DateTime.Today, 4, 1, 10),
                new GroupTraining(DateTime.Today, 5, 2, 9),
                new GroupTraining(DateTime.Today.AddDays(-1), 1, 4, 4),
                new GroupTraining(DateTime.Today.AddDays(-1), 5, 1, 3),
                new GroupTraining(DateTime.Today.AddDays(-2), 1, 2, 5),
                new GroupTraining(DateTime.Today.AddDays(-2), 2, 4, 8),
            };

            return groupTrainings;
        }
    }
}
