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
    public class RepositoryProduto :IDisposable
    {
        private LojaProdutosEntities odb;

        public RepositoryProduto()
        {
            odb = Helper.Data.getContexto();
        }

        public RepositoryProduto(LojaProdutosEntities _odb)
        {
            odb = _odb;
        }

        public void Incluir(Produto oProd)
        {
            odb.Produto.Add(oProd);
            odb.SaveChanges();
        }

        public void Alterar(Produto oProd)
        {
            odb.Entry(oProd).State = System.Data.Entity.EntityState.Modified;
            odb.SaveChanges();
        }

        public void Excluir(int id, bool foraContexto = false)
        {
            Produto oProd = odb.Produto.Find(id);
            if (foraContexto) 
            {
                odb.Produto.Attach(oProd);
            }
            odb.Produto.Remove(oProd);
            odb.SaveChanges();
        }

        public Produto Selecionar(int? id)
        {
            return (from p in odb.Produto where p.Pro_ID == id select p).Include(p => p.Categoria).Include(p => p.Fornecedor).FirstOrDefault();                     
        }
        public List<Produto> SelecionarPorCategoria(int id)
        {
            return (from p in odb.Produto where p.Pro_Categoria == id select p).Include(p => p.Categoria).Include(p => p.Fornecedor).ToList();
        }
        public List<Produto> SelecionarPorFornecedor(int id)
        {
            return (from p in odb.Produto where p.Pro_Fornecedor == id select p).Include(p => p.Categoria).Include(p => p.Fornecedor).ToList();
        }

        public List<Produto> SelecionarTodos()
        {
            IQueryable<Produto> iProd = odb.Produto.Include(p => p.Categoria).Include(p => p.Fornecedor);
            return iProd.ToList();
        }

        public List<Produto> SelecionarPorNome(string Nome)
        {
            return (from p in odb.Produto where p.Pro_Nome.StartsWith(Nome) orderby p.Pro_Nome select p).ToList();
        }

        public List<Produto> SelecionarPorNome2(string Nome)
        {
            return (from p in odb.Produto where p.Pro_Nome == Nome select p).Include(p => p.Categoria).Include(p => p.Fornecedor).ToList();
        }


        public void Dispose()
        {
            odb.Dispose();
        }
    }
}
