using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabBimestral.MODEL.Repos
{
    public class RepositoryUsuario : IDisposable
    {
        private LojaProdutosEntities odb;


        public LojaProdutosEntities Contexto
        {
            get { return odb; }
            set { odb = value; }
        }
        public RepositoryUsuario()
        {
            odb = Helper.Data.getContexto();
        }

        public RepositoryUsuario(LojaProdutosEntities _odb)
        {
            odb = _odb;
        }

        public Usuario SelecionarPorEmail(string email)
        {
            return (from p in odb.Usuario where p.Usu_Email == email select p).FirstOrDefault();
        }

        public void Dispose()
        {
            odb.Dispose();
        }
    }
}