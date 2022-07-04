using System;
using System.ComponentModel.DataAnnotations;

namespace SportComplexMVC.Models.Entities
{
    public class PersonalTraining
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int CoachId { get; set; }
        public Coach Coach { get; set; }

        [Required]
        public int ClientId { get; set; }
        public Client Client { get; set; }

        [Required]
        public int TrainingRoomId { get; set; }
        public TrainingRoom TrainingRoom { get; set; }

        public PersonalTraining() { }

        public PersonalTraining(DateTime date, int coachId, int clientId, int trainingRoomId)
        {
            Date = date;
            CoachId = coachId;
            ClientId = clientId;
            TrainingRoomId = trainingRoomId;
        }
    }
}
