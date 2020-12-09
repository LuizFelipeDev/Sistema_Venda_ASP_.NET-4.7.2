using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Entity;
using System.Net;

namespace TrabBimestral.MODEL.Repositories
{
    public class RepositoryVendaProduto : IDisposable
    {
        private LojaProdutosEntities odb;

        public RepositoryVendaProduto()
        {
            odb = Helper.Data.getContexto();
        }

        public RepositoryVendaProduto(LojaProdutosEntities _odb)
        {
            odb = _odb;
        }

        public int Incluir(VendaProduto oVen)
        {
            odb.VendaProduto.Add(oVen);
            odb.SaveChanges();
            return oVen.VeP_ID;
        }

        public void Alterar(VendaProduto oVen)
        {
            odb.Entry(oVen).State = System.Data.Entity.EntityState.Modified;
            odb.SaveChanges();
        }

        public void Excluir(int id, bool foraContexto = false)
        {
            VendaProduto oVen = odb.VendaProduto.Find(id);
            if (foraContexto)
            {
                odb.VendaProduto.Attach(oVen);
            }
            odb.VendaProduto.Remove(oVen);
            odb.SaveChanges();
        }

        public VendaProduto Selecionar(int? id)
        {
            return (from p in odb.VendaProduto where p.VeP_ID == id select p).FirstOrDefault();
        }

        public List<VendaProduto> SelecionarTodos()
        {
            IQueryable<VendaProduto> iVenda = odb.VendaProduto;
            return iVenda.ToList();
        }

        public List<VendaProduto> SelecionarIdVenda(int id)
        {
            return (from p in odb.VendaProduto where p.ID_Venda == id select p).ToList();
        }


        public void Dispose()
        {
            odb.Dispose();
        }
    }
}
