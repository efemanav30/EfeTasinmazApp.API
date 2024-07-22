﻿using EfeTasinmazApp.API.Entities.Concrete;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tasinmaz_Proje.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Adress { get; set; }

        [Required]
        public string Role { get; set; }

       // public ICollection<Tasinmaz> Tasinmazlar { get; set; } error 500

    }
}