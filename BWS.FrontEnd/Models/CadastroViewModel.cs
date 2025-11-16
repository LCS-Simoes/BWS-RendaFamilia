using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace BWS.FrontEnd.Models
{
    public class CadastroViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Preencha o Nome")]
        [MaxLength(150)]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Preencha o campo de CPF")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Preencha o de Data de Nascimento")]
        public DateOnly DataNascimento { get; set; }
        public Decimal RendaFamilia { get; set; }
    }
}
