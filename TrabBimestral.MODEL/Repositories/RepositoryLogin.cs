using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabBimestral.MODEL.Repos
{
    public class RepositoryLogin : IDisposable
    {
        private LojaProdutosEntities odb;


        public LojaProdutosEntities Contexto
        {
            get { return odb; }
            set { odb = value; }
        }
        public RepositoryLogin()
        {
            odb = Helper.Data.getContexto();
        }

        public RepositoryLogin(LojaProdutosEntities _odb)
        {
            odb = _odb;
        }

        public bool VerificaLogin(string login, string pass)
        {
            var userLogin = (from user in odb.Usuario where user.Usu_Email == login && user.Usu_Senha == pass select user).FirstOrDefault();
            if (userLogin != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Dispose()
        {
            odb.Dispose();
        }
    }
}
