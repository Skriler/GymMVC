using System;
using System.ComponentModel.DataAnnotations;

namespace SportComplexMVC.Models.ViewModels
{
    public class EditProfileViewModel
    {
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
    }
}
