using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SportComplexMVC.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Birth date")]
        public DateTime BirthDate { get; set; }

        [Required]
        public int GenderId { get; set; }
        public Gender Gender { get; set; }

        public ApplicationUser() { }

        public ApplicationUser(string firstName, string lastName, DateTime birthDate, string email, string phoneNumber, int genderId)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Email = email;
            PhoneNumber = phoneNumber;
            UserName = email;
            GenderId = genderId;
        }
    }
}
