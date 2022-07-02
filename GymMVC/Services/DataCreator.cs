using System;
using System.Collections.Generic;
using GymMVC.Enums;
using GymMVC.Models.Entities;

namespace GymMVC.Services
{
    public static class DataCreator
    {
        private static List<User> users;

        static DataCreator()
        {
            users = CreateUserList();
        }

        private static List<User> CreateUserList()
        {
            List<User> peopleInfo = new List<User>()
            {
                new User(1, "Huntley", "Predohl", "hpredohl0@icio.us", 1),
                new User(2, "Carmen", "Mancell", "cmancell1@spotify.com", 1),
                new User(3, "Mel", "Stoeck", "mstoeck2@telegraph.co.uk", 1),
                new User(4, "Orsola", "Feldheim", "ofeldheim3@twitter.com", 2),
                new User(5, "Kirbie", "Kleiser", "kkleiser4@latimes.com", 2),
                new User(6, "Krispin", "Pires", "kpires5@wikispaces.com", 1),
                new User(7, "Brigham", "Denisovich", "bdenisovich6@quantcast.com", 1),
                new User(8, "Fin", "Andryszczak", "fandryszczak7@noaa.gov", 1),
                new User(9, "Brigham", "Tonner", "btonner8@cbc.ca", 1),
                new User(10, "Kinna", "Boutton", "kboutton9@admin.ch", 2),
                new User(11, "Janean", "Shotboult", "jshotboult0@soundcloud.com", 1),
                new User(12, "Erroll", "Hethron", "ehethron1@slate.com", 2),
                new User(13, "Margot", "Thieme", "mthieme2@oaic.gov.au", 2),
                new User(14, "Tudor", "Ancliffe", "tancliffe3@rakuten.co.jp", 1),
                new User(15, "Tony", "Bartrap", "tbartrap4@sohu.com", 1),
                new User(16, "Emmery", "Rue", "erue5@scientificamerican.com", 2),
                new User(17, "Twila", "Conrart", "tconrart6@ftc.gov", 2),
                new User(18, "Rochester", "Colleymore", "rcolleymore7@dmoz.org", 1),
                new User(19, "Erroll", "Cardenosa", "ecardenosa8@toplist.cz", 1),
                new User(20, "Bibbie", "Sizzey", "bsizzey9@umich.edu", 2)
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

            return specializations;
        }

        public static List<User> GetUserList()
        {
            return users;
        }

        public static List<Coach> GetCoachList()
        {
            List<Coach> coaches = new List<Coach>()
            {
                new Coach(1, 1, 1, 1),
                new Coach(2, 2, 1, 2),
                new Coach(3, 3, 2, 3),
                new Coach(4, 4, 2, 4),
                new Coach(5, 5, 3, 5),
            };

            return coaches;
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
    }
}
