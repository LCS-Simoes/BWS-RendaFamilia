using BWS.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWS.Application.DTOs
{
    public class CadastrarRequest
    {
        [Required]
        [StringLength(150)]
        public string Nome { get; set; }
        [Required]
        [StringLength(11)]
        [RegularExpression(@"^\d+$", ErrorMessage = "CPF deve conter apenas dígitos.")]
        public string Cpf { get; set; }
        [Required]
        public DateOnly DataNascimento { get; set; }
        [Range(0, double.MaxValue)]
        public decimal? RendaFamilia { get; set; }

        public Clientes ToEntity()
        {
            return new Clientes
            {
                Nome = Nome,
                Cpf = Cpf,
                DataNascimento = DataNascimento,
                RendaFamilia = RendaFamilia
            };
        }
    }
}
