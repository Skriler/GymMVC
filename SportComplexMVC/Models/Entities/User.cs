using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SportComplexMVC.Models.Entities
{
    public class User : IdentityUser
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

        public User() { }

        public User(string firstName, string lastName, string email, int genderId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            GenderId = genderId;
        }
    }
}
