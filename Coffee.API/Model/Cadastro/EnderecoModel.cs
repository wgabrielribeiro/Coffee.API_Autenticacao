using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Coffee.API.Model.Cadastro
{
    [Table("DadosEndereco")]

    public class EnderecoModel
    {
        [Key]
        public int ID_CAD { get; set; }
        [Required(ErrorMessage = "O USUARIO é obrigatório")]
        public string USUARIO { get; set; }

        [Required(ErrorMessage = "A RUA é obrigatória")]
        public string RUA { get; set; }

        [Required(ErrorMessage = "O NUMERO é obrigatório")]
        public string NUMERO { get; set; }
        public string COMPLEMENTO { get; set; }

        [Required(ErrorMessage = "O CEP é obrigatório")]
        public string CEP { get; set; }
        public string BAIRRO { get; set; }
        public string CIDADE { get; set; }
        public string ESTADO { get; set; }
        public string PAIS { get; set; }

        public virtual UserModel UserModel { get; set; }
        //[JsonIgnore]
        //public int? UserModelId { get; set; }
    }
}
