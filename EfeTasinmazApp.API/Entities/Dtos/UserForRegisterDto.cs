﻿namespace EfeTasinmazApp.API.Entities.Dtos
{
    public class UserForRegisterDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string Phone { get; set; }
        public string Adress { get; set; }
        public string Role { get; set; }
    }
}