using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrabBimestral.MODEL;
using TrabBimestral.MODEL.Repositories;

namespace TrabBimestral.VIEW.Models
{
    public class VendaView : Venda
    {

       public string Nome { get; set; }

        public int Pro_ID { get; set; }

        public string VeP_Qtd { get; set; }

        public decimal VeP_PrecoVenda { get; set; }
        public decimal ValorVenda { get; set; }

        public string oProduto{ get;set; }

        public VendaProdutoView oVendaProdutoView { get; set; }

        public decimal Preco { get; set; }
        public string Categoria { get; set; }
        public string Fornecedor { get; set; }
        public int Qtd { get; set; }

        public int idExcluirProduto { get; set; }

        public bool finalizarEdicao { get; set; }

       
    }
}