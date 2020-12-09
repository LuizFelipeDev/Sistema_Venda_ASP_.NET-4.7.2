using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabBimestral.MODEL.Repositories
{
    public class RepositoryFornecedor
    {
        private LojaProdutosEntities odb;

        public RepositoryFornecedor()
        {
            odb = Helper.Data.getContexto();
        }

        public RepositoryFornecedor(LojaProdutosEntities _odb)
        {
            odb = _odb;
        }

        public void Incluir(Fornecedor oFor)
        {
            odb.Fornecedor.Add(oFor);
            odb.SaveChanges();
        }

        public void Alterar(Fornecedor oFor)
        {
            odb.Entry(oFor).State = System.Data.Entity.EntityState.Modified;
            odb.SaveChanges();
        }

        public void Excluir(int id, bool foraContexto = false)
        {
            Fornecedor oFor = odb.Fornecedor.Find(id);
            if (foraContexto)
            {
                odb.Fornecedor.Attach(oFor);
            }
            odb.Fornecedor.Remove(oFor);
            odb.SaveChanges();
        }

        public Fornecedor Selecionar(int? id)
        {
            return (from p in odb.Fornecedor where p.For_ID == id select p).FirstOrDefault();
        }

        public List<Fornecedor> SelecionarTodos()
        {
            return (from p in odb.Fornecedor orderby p.For_Nome select p).ToList();
        }
        public Fornecedor SelecionarPorNome(string Nome)
        {
            return (from p in odb.Fornecedor where p.For_Nome == Nome select p).FirstOrDefault();
        }

        public void Dispose()
        {
            odb.Dispose();
        }
    }
}
