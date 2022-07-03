using System;
using System.Collections.Generic;
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

        public ApplicationUser(string firstName, string lastName, string email, int genderId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            UserName = email;
            GenderId = genderId;
        }
    }
}
