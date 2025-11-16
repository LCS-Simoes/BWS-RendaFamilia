using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BWS.Application.DTOs
{
    public class AtualizarRequest
    {
        [Required]
        [StringLength(150)]
        public string Nome { get; set; }

        [Required]
        public DateOnly DataNascimento { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? RendaFamilia { get; set; }
    }
}
    