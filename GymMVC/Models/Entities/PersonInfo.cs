using System;
using System.ComponentModel.DataAnnotations;

namespace GymMVC.Models.Entities
{
    public class PersonInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public int GenderId { get; set; }
        public Gender Gender { get; set; }

        public PersonInfo() { }

        public PersonInfo(int id, string firstName, string lastName, string email, int genderId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            GenderId = genderId;
        }
    }
}
