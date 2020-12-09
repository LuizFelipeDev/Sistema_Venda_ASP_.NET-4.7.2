using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabBimestral.MODEL.Repositories
{
    public class RepositoryCategoria
    {
        private LojaProdutosEntities odb;

        public RepositoryCategoria()
        {
            odb = Helper.Data.getContexto();
        }

        public RepositoryCategoria(LojaProdutosEntities _odb)
        {
            odb = _odb;
        }

        public void Incluir(Categoria oCat)
        {
            odb.Categoria.Add(oCat);
            odb.SaveChanges();
        }

        public void Alterar(Categoria oCat)
        {
            odb.Entry(oCat).State = System.Data.Entity.EntityState.Modified;
            odb.SaveChanges();
        }

        public void Excluir(int id, bool foraContexto = false)
        {
            Categoria oCat = odb.Categoria.Find(id);
            if (foraContexto)
            {
                odb.Categoria.Attach(oCat);
            }
            odb.Categoria.Remove(oCat);
            odb.SaveChanges();
        }

        public Categoria Selecionar(int? id)
        {
            return (from p in odb.Categoria where p.Cat_ID == id select p).FirstOrDefault();
        }

        public List<Categoria> SelecionarTodos()
        {
            return (from p in odb.Categoria orderby p.Cat_Nome select p).ToList();
        }

        public Categoria SelecionarPorNome(string Nome)
        {
            return (from p in odb.Categoria where p.Cat_Nome == Nome select p).FirstOrDefault();
        }

        public void Dispose()
        {
            odb.Dispose();
        }
    }
}
