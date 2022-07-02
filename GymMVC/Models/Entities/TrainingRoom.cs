using System.ComponentModel.DataAnnotations;

namespace GymMVC.Models.Entities
{
    public class TrainingRoom
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int Floor { get; set; }

        [Required]
        public int Number { get; set; }

        public TrainingRoom() { }

        public TrainingRoom(int id, string title, int floor, int number)
        {
            Id = id;
            Title = title;
            Floor = floor;
            Number = number;
        }
    }
}
