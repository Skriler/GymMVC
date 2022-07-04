using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SportComplexMVC.Models.Entities;

namespace SportComplexMVC.Models.ViewModels
{
    public class EditCoachViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3), MaxLength(30)]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3), MaxLength(30)]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Birth date")]
        public DateTime BirthDate { get; set; }
        public DateTime MinBirthDate { get; set; }
        public DateTime MaxBirthDate { get; set; }

        [Required]
        [MinLength(3), MaxLength(30)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public int GenderId { get; set; }
        public List<Gender> Genders { get; set; }

        [Required]
        [Display(Name = "Position")]
        public int PositionId { get; set; }
        public List<Position> Positions { get; set; }

        [Required]
        [Display(Name = "Specialization")]
        public int SpecializationId { get; set; }
        public List<Specialization> Specializations { get; set; }
    }
}
