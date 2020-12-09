using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrabBimestral.MODEL.Helper
{
    public class Data
    {
        public static LojaProdutosEntities getContexto()
        {
            LojaProdutosEntities odb = new LojaProdutosEntities();
            odb.Configuration.ProxyCreationEnabled = false;
            return odb;
        }
    }
}
