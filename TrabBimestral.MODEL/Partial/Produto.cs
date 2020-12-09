using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabBimestral.MODEL
{

    [MetadataType(typeof(MD_Produto))]

    public partial class Produto
    {
        private class MD_Produto
        {
            public int Pro_ID { get; set; }

            [Required]
            [Display(Name = "Nome do Produto")]
            public string Pro_Nome { get; set; }

            [Required]
            [Display(Name = "Categoria")]
            public int Pro_Categoria { get; set; }

            [Required]
            [Display(Name = "Fornecedor")]
            public int Pro_Fornecedor { get; set; }

            [Required]
            [Display(Name = "Preço (R$)")]
            public decimal Pro_Preco { get; set; }


            public virtual Categoria Categoria { get; set; }

            
            public virtual Fornecedor Fornecedor { get; set; }


        }
    }
}

