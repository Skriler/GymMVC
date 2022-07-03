using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SportComplexMVC.Models.Entities
{
    public class Group
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<Client> Clients { get; set; } 
    }
}
