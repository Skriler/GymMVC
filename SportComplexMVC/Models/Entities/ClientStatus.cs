using System.ComponentModel.DataAnnotations;

namespace SportComplexMVC.Models.Entities
{
    public class ClientStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Client Status")]
        public string Title { get; set; }

        public ClientStatus() { }

        public ClientStatus(int id, string title)
        {
            Id = id;
            Title = title;
        }
    }
}
