using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coffee.API.Model.Cadastro
{
    [Table("DadosCadastro")]
    public class CadastroModel
    {
        [Key]
        [Required(ErrorMessage = "O documento é obrigatório")]
        public string DOCUMENTO { get; set; }
        [Required(ErrorMessage = "O telefone é obrigatório")]
        public string TELEFONE { get; set; }
        [Required(ErrorMessage = "O email é obrigatório")]
        public string EMAIL { get; set; }

        [Required(ErrorMessage = "O usuario é obrigatório")]
        public string USUARIO { get; set; }

        public virtual UserModel User { get; set; }
    }
}
