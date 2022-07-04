using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SportComplexMVC.Models.Entities;

namespace SportComplexMVC.Models.ViewModels
{
    public class RegisterViewModel
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

        [Required]
        [MinLength(3), MaxLength(25)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [MinLength(3), MaxLength(25)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        [Display(Name = "Repeat password")]
        public string PasswordRepetition { get; set; }

        [Required]
        [Display(Name = "Gender")]
        public int GenderId { get; set; }
        public List<Gender> Genders { get; set; }
    }
}
