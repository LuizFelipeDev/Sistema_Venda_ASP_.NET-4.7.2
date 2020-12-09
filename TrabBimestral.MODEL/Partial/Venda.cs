using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabBimestral.MODEL
{
    [MetadataType(typeof(MD_Venda))]

    public partial class Venda
    {
        private class MD_Venda
        {

            public int Ven_ID { get; set; }

            [Required]
            [Display(Name = "Código Cliente")]
            public int ID_Cli { get; set; }

            [Required]
            [Display(Name = "Data")]
            public System.DateTime Ven_Data { get; set; }

            [Required]
            [Display(Name = "Venda Fechada")]
            public bool Ven_Fechada { get; set; }
        }

        private decimal _ValorTotal = 0;

        public decimal ValorTotal
        {
            get
            {
                if(this.VendaProduto!= null)
                {
                    foreach(VendaProduto _VP in this.VendaProduto)
                    {
                        _ValorTotal += (_VP.VeP_PrecoVenda * _VP.VeP_Qtd);
                    }
                }
                else
                {
                    _ValorTotal = 0;
                }
                return _ValorTotal;
            }
            set
            {
                _ValorTotal = value;
            }
        }
    }
}
