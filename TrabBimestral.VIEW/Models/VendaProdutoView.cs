using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrabBimestral.MODEL;
using TrabBimestral.MODEL.Repositories;

namespace TrabBimestral.VIEW.Models
{
    public class VendaProdutoView
    {     
        public int idVendaProduto { get; set; }
        public string Produto { get; set; }
        public decimal Preco { get; set; }
        public string Categoria { get; set; }
        public string Fornecedor { get; set; }
        public int Qtd { get; set; }

    }
}