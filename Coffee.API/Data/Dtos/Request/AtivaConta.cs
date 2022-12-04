using System.ComponentModel.DataAnnotations;

namespace Coffee.API.Data.Dtos.Request
{
    public class AtivaConta
    {
        [Required]
        public int UsuarioId { get; set; }

        [Required]
        public string CodigoAtivacao { get; set; }     

    }
}
