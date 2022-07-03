using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SportComplexMVC.Models.Entities;

namespace SportComplexMVC.Models.ViewModels
{
    public class EditClientViewModel
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
        public int GenderId { get; set; }
        public List<Gender> Genders { get; set; }

        [Required]
        [Display(Name = "Client Status")]
        public int ClientStatusId { get; set; }
        public List<ClientStatus> ClientStatuses { get; set; }
    }
}
