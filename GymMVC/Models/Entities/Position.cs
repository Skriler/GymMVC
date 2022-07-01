using System.ComponentModel.DataAnnotations;

namespace GymMVC.Models.Entities
{
    public class Position
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public Position() { }

        public Position(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
