using System.ComponentModel.DataAnnotations;

namespace SportComplexMVC.Models.Entities
{
    public class Gender
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Title { get; set; }

        public Gender() { }

        public Gender(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
