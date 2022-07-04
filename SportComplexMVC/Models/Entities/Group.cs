using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportComplexMVC.Models.Entities
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Group")]
        public string Title { get; set; }

        public List<Client> Clients { get; set; } 
        public Group()
        {
            Clients = new List<Client>();
        }

        public Group(string title)
            : this()
        {
            Title = title;
        }
    }
}
