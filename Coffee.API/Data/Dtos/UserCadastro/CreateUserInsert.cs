using Coffee.API.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coffee.API.Data.Dtos.UserCad
{
    public class CreateUserInsert
    { 
        [Required(ErrorMessage ="Username é orbigatorio")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email é orbigatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password é orbigatorio")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "RePassword é orbigatorio")]
        [Compare("Password")]
        public string RePassword { get; set; }

    }
}
