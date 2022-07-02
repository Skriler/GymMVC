using System.ComponentModel.DataAnnotations;

namespace SportComplexMVC.Models.Entities
{
    public class Coach
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }

        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }

        public Coach() { }

        public Coach(int id, int userId, int positionId, int specializationId)
        {
            Id = id;
            UserId = userId;
            PositionId = positionId;
            SpecializationId = specializationId;
        }
    }
}
