using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SportComplexMVC.Models.Entities;

namespace SportComplexMVC.Models.ViewModels
{
    public class AddPersonalTrainingViewModel
    {
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get ; set; }
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public int CoachId { get; set; }

        [Required]
        public int TrainingRoomId { get; set; }
        public List<TrainingRoom> TrainingRooms { get; set; }
    }
}
