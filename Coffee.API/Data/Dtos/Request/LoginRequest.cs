using System.ComponentModel.DataAnnotations;

namespace Coffee.API.Data.Dtos.Request
{
    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }


    }
}
