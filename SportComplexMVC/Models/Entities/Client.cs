using System.ComponentModel.DataAnnotations;

namespace SportComplexMVC.Models.Entities
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        public int ClientStatusId { get; set; }
        public ClientStatus ClientStatus { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public Client() { }

        public Client(int clientStatusId, string applicationUserId)
        {
            ClientStatusId = clientStatusId;
            ApplicationUserId = applicationUserId;
        }
    }
}
