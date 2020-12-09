using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabBimestral.MODEL.Repositories
{
    public class RepositoryCliente: IDisposable
    {
        private LojaProdutosEntities odb;


        public LojaProdutosEntities Contexto
        {
            get { return odb; }
            set { odb = value; }
        }
        public RepositoryCliente()
        {
            odb = Helper.Data.getContexto();
        }

        public RepositoryCliente(LojaProdutosEntities _odb)
        {
            odb = _odb;
        }

        public void Incluir(Cliente oCli)
        {
            odb.Cliente.Add(oCli);
            odb.SaveChanges();
        }

        public void Alterar(Cliente oCli)
        {
            odb.Entry(oCli).State = System.Data.Entity.EntityState.Modified;
            odb.SaveChanges();
        }

        public void Excluir(int id, bool foraContexto = false)
        {
            Cliente oCli = odb.Cliente.Find(id);
            if (foraContexto)
            {
                odb.Cliente.Attach(oCli);
            }
            odb.Cliente.Remove(oCli);
            odb.SaveChanges();
        }

        public Cliente Selecionar(int? id)
        {
            return (from p in odb.Cliente where p.Cli_ID == id select p).FirstOrDefault();
        }

        public List<Cliente> SelecionarTodos()
        {
            return (from p in odb.Cliente orderby p.Cli_Nome select p).ToList();
        }

        public List<Cliente> SelecionarPorNome(string Nome)
        {
            return (from p in odb.Cliente where p.Cli_Nome.StartsWith(Nome) orderby p.Cli_Nome select p).ToList();
        }

        public Cliente SelecionarUnicoPorNome(string Nome)
        {
            return (from p in odb.Cliente where p.Cli_Nome == Nome  select p).FirstOrDefault();
        }

        public void Dispose()
        {
            odb.Dispose();
        }
    }
}
