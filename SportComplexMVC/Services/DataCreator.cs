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
                new ApplicationUser("Huntley", "Predohl", "hpredohl0@icio.us", 1),
                new ApplicationUser("Carmen", "Mancell", "cmancell1@spotify.com", 1),
                new ApplicationUser("Mel", "Stoeck", "mstoeck2@telegraph.co.uk", 1),
                new ApplicationUser("Orsola", "Feldheim", "ofeldheim3@twitter.com", 2),
                new ApplicationUser("Kirbie", "Kleiser", "kkleiser4@latimes.com", 2),
                new ApplicationUser("Krispin", "Pires", "kpires5@wikispaces.com", 1),
                new ApplicationUser("Brigham", "Denisovich", "bdenisovich6@quantcast.com", 1),
                new ApplicationUser("Fin", "Andryszczak", "fandryszczak7@noaa.gov", 1),
                new ApplicationUser("Brigham", "Tonner", "btonner8@cbc.ca", 1),
                new ApplicationUser("Kinna", "Boutton", "kboutton9@admin.ch", 2),
                new ApplicationUser("Janean", "Shotboult", "jshotboult0@soundcloud.com", 1),
                new ApplicationUser("Erroll", "Hethron", "ehethron1@slate.com", 2),
                new ApplicationUser("Margot", "Thieme", "mthieme2@oaic.gov.au", 2),
                new ApplicationUser("Tudor", "Ancliffe", "tancliffe3@rakuten.co.jp", 1),
                new ApplicationUser("Tony", "Bartrap", "tbartrap4@sohu.com", 1),
                new ApplicationUser("Emmery", "Rue", "erue5@scientificamerican.com", 2),
                new ApplicationUser("Twila", "Conrart", "tconrart6@ftc.gov", 2),
                new ApplicationUser("Rochester", "Colleymore", "rcolleymore7@dmoz.org", 1),
                new ApplicationUser("Erroll", "Cardenosa", "ecardenosa8@toplist.cz", 1),
                new ApplicationUser("Bibbie", "Sizzey", "bsizzey9@umich.edu", 2),
                new ApplicationUser("Bob", "Hoiuk", "Hoiuk@gmail.com", 1),
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
            for (int i = 1; i <= 5; ++i)
            {
                coach = new Coach(rand.Next(1, positionsAmount), rand.Next(1, specializationsAmount), users[i - 1].Id);
                coaches.Add(coach);
            }

            return coaches;
        }

        public static List<Client> GetClientList()
        {
            List<Client> clients = new List<Client>();

            Client client;
            for (int i = 1; i <= 15; ++i)
            {
                client = new Client(rand.Next(1, clientStatusesAmount), users[i + 4].Id);
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
                new GroupTraining(DateTime.Today.AddDays(-1), 2, 5, 1),
                new GroupTraining(DateTime.Today.AddDays(-1), 3, 7, 10),
                new GroupTraining(DateTime.Today.AddDays(-2), 2, 9, 3),
                new GroupTraining(DateTime.Today.AddDays(-2), 4, 2, 6),
                new GroupTraining(DateTime.Today.AddDays(-3), 1, 1, 8),
                new GroupTraining(DateTime.Today.AddDays(-3), 3, 10, 4),
                new GroupTraining(DateTime.Today.AddDays(-4), 2, 11, 5),
                new GroupTraining(DateTime.Today.AddDays(-4), 4, 6, 7),
                new GroupTraining(DateTime.Today.AddDays(-5), 3, 8, 9),
                new GroupTraining(DateTime.Today.AddDays(-5), 4, 2, 2),
            };

            return groupTrainings;
        }
    }
}
