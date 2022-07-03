using System.ComponentModel.DataAnnotations;

namespace SportComplexMVC.Models.Entities
{
    public class Coach
    {
        [Key]
        public int Id { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }

        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public Coach() { }

        public Coach(int id, int positionId, int specializationId, string applicationUserId)
        {
            Id = id;
            PositionId = positionId;
            SpecializationId = specializationId;
            ApplicationUserId = applicationUserId;
        }
    }
}
