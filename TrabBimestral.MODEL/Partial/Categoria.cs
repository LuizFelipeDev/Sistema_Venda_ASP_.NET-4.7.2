using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabBimestral.MODEL
{

    [MetadataType(typeof(MD_Categoria))]

    public partial class Categoria
    {

        private class MD_Categoria
        {
            public int Cat_ID { get; set; }

            [Required]
            [Display(Name = "Categoria")]
            public string Cat_Nome { get; set; }
        }

    }
}
