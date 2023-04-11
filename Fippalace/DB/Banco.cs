using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Fippalace.DB
{
    public abstract class Banco
    {
        public abstract bool Conecta();
        public abstract void Desconecta();
        public abstract void BeginTransaction();
        public abstract void RollbackTransaction();
        public abstract void CommitTransaction();
        public abstract bool ExecuteQuery(String sql, out DataTable dt, params Object[] parametros);
        public abstract bool ExecuteNonQuery(String sql, params Object[] parametros);
        public abstract int GetIdentity();
    }


}
