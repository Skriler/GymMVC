using System;
using System.ComponentModel.DataAnnotations;

namespace GymMVC.Models.Entities
{
    public class Coach
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int PersonInfoId { get; set; }
        public PersonInfo PersonInfo { get; set; }

        public int PositionId { get; set; }
        public Position Position { get; set; }

        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }

        public Coach() { }

        public Coach(int id, int personInfoId, int positionId, int specializationId)
        {
            Id = id;
            PersonInfoId = personInfoId;
            PositionId = positionId;
            SpecializationId = specializationId;
        }
    }
}
