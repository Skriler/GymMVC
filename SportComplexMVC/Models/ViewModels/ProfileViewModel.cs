using System;
using System.ComponentModel.DataAnnotations;

namespace SportComplexMVC.Models.ViewModels
{
    public class ProfileViewModel
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

        [Required]
        [MinLength(3), MaxLength(30)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Display(Name = "Client Status")]
        public string ClientStatus { get; set; }

        [Display(Name = "Group")]
        public string Group { get; set; }

        [Display(Name = "Position")]
        public string Position { get; set; }

        [Display(Name = "Specialization")]
        public string Specialization { get; set; }
    }
}
