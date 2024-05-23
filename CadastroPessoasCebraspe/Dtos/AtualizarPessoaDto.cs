using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CadastroPessoasCebraspe.Dtos
{
    public class AtualizarPessoaDto
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string RG { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}