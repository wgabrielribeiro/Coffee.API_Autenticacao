using System.ComponentModel.DataAnnotations;

namespace Coffee.API.Data.Dtos.Request
{
    public class SolicitaResetRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
