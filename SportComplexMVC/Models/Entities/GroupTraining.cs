using System;
using System.ComponentModel.DataAnnotations;

namespace SportComplexMVC.Models.Entities
{
    public class GroupTraining
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        public int CoachId { get; set; }
        public Coach Coach { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public int TrainingRoomId { get; set; }
        public TrainingRoom TrainingRoom { get; set; }

        public GroupTraining(DateTime date, int coachId, int groupId, int trainingRoomId)
        {
            Date = date;
            CoachId = coachId;
            GroupId = groupId;
            TrainingRoomId = trainingRoomId;
        }
    }
}
