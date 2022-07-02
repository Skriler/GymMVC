using System.ComponentModel.DataAnnotations;

namespace SportComplexMVC.Models.Entities
{
    public class Position
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Position")]
        public string Title { get; set; }

        public Position() { }

        public Position(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
