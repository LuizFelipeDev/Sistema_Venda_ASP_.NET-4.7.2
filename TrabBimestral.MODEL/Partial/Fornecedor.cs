using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabBimestral.MODEL
{
    [MetadataType(typeof(MD_Fornecedor))]
    public partial class Fornecedor
    {
        private class MD_Fornecedor
        {
            public int For_ID { get; set; }

            [Required]
            [Display(Name = "Fornecedor")]
            public string For_Nome { get; set; }
        }
    }
}

