using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Coffee.API.Model.Cadastro
{

    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }


    }
}
