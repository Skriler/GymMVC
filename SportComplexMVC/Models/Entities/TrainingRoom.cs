using System.ComponentModel.DataAnnotations;

namespace SportComplexMVC.Models.Entities
{
    public class TrainingRoom
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Floor")]
        public int Floor { get; set; }

        [Required]
        [Display(Name = "Number")]
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
