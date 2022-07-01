using System.ComponentModel.DataAnnotations;

namespace GymMVC.Models.Entities
{
    public class TrainingRoom
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Title { get; set; }

        [Required]
        public int Floor { get; set; }

        [Required]
        public int Number { get; set; }
    }
}
