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
    public class RepositoryVenda : IDisposable
    {
        private LojaProdutosEntities odb;

        public RepositoryVenda()
        {
            odb = Helper.Data.getContexto();
        }

        public RepositoryVenda(LojaProdutosEntities _odb)
        {
            odb = _odb;
        }

        public int Incluir(Venda oVen)
        {
            odb.Venda.Add(oVen);
            odb.SaveChanges();
            return oVen.Ven_ID;
        }

        public void Alterar(Venda oVen)
        {
            odb.Entry(oVen).State = System.Data.Entity.EntityState.Modified;
            odb.SaveChanges();
        }

        public void Excluir(int id, bool foraContexto = false)
        {
            Venda oVen = odb.Venda.Find(id);
            if (foraContexto)
            {
                odb.Venda.Attach(oVen);
            }
            odb.Venda.Remove(oVen);
            odb.SaveChanges();
        }

        public Venda Selecionar(int? id)
        {
            return (from p in odb.Venda where p.Ven_ID == id select p).Include(p => p.Cliente).FirstOrDefault();
        }

        public List<Venda> SelecionarPorCliente(int id)
        {
            return (from p in odb.Venda where p.ID_Cli == id select p).Include(p => p.Cliente).ToList();
        }

        public List<Venda> SelecionarPorData(DateTime date)
        {
            return (from p in odb.Venda where p.Ven_Data == date select p).Include(p => p.Cliente).ToList();
        }

        public List<Venda> SelecionarPorVendaFechada(bool venda)
        {
            return (from p in odb.Venda where p.Ven_Fechada == venda select p).Include(p => p.Cliente).ToList();
        }

        public List<Venda> SelecionarTodos()
        {
            IQueryable<Venda> iVenda = odb.Venda.Include(p => p.Cliente);
            return iVenda.ToList();
        }


        public void Dispose()
        {
            odb.Dispose();
        }
    }
}
