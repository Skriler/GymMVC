using System.ComponentModel.DataAnnotations;

namespace SportComplexMVC.Models.Entities
{
    public class Specialization
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Specialization")]
        public string Title { get; set; }

        public Specialization() { }

        public Specialization(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
