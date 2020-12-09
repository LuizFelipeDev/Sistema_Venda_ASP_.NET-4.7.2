using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabBimestral.MODEL
{

    [MetadataType(typeof(MD_Cliente))]

    public partial class Cliente
    {
        private class MD_Cliente
        {
            public int Cli_ID { get; set; }

            [Required]
            [Display(Name = "Nome")]
            public string Cli_Nome { get; set; }

            [Required]
            [Display(Name = "Email")]
            public string Cli_Email { get; set; }

            [Required]
            [MaxLength(11, ErrorMessage = "O campo CPF deve conter 11 Dígitos")]
            [Display(Name = "CPF")]
            public string Cli_CPF { get; set; }
        }
    }
}
