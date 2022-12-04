using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Coffee.API.Model.Cadastro
{
    [Table("CoffeeUser")]
    public class UserModel
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        public string NOME { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória")]
        public DateTime DT_NASCIMENTO { get; set; }

        public DateTime DTCADASTRO { get; set; }

        [Key]
        [Required(ErrorMessage = "O usuario é obrigatório")]
        public string USUARIO { get; set; }

        public virtual CadastroModel Cadastro { get; set; }
        public virtual EnderecoModel Endereco { get; set; }
    }
}
