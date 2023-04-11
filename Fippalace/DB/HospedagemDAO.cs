using System;
using System.Collections.Generic;
using System.Linq;
using Fippalace.Modelo;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Fippalace.DB
{
    class HospedagemDAO
    {
        private Banco banco;
        public HospedagemDAO(Banco banco)
        {
            this.banco = banco;
        }

        public bool insert(Hospedagens hospedagem)
        {
            bool resultado = false;

            try
            {
                banco.Conecta();
                string sql = "insert into HOSPEDAGENS (DESCRICAO)" +
                             "values(@descricao)";
                resultado = banco.ExecuteNonQuery(sql, "@descricao", hospedagem.Descricao);
                hospedagem.Codigo = banco.GetIdentity();
                banco.Desconecta();
            }
            catch (SqlException sqle)
            {

            }


            return resultado;
        }
        public bool update(Hospedagens hospedagem)
        {
            bool resultado = false;

            try
            {
                banco.Conecta();
                string sql = "UPDATE HOSPEDAGENS  SET  DESCRICAO = @DESCRICAO WHERE CODIGO = @CODIGO";
                resultado = banco.ExecuteNonQuery(sql, "@descricao", hospedagem.Descricao, "@CODIGO", hospedagem.Codigo);
                hospedagem.Codigo = banco.GetIdentity();
                banco.Desconecta();
            }
            catch (SqlException sqle)
            {

            }


            return resultado;
        }

        public bool delete(int codigo)
        {
            bool resultado = false;

            try
            {
                banco.Conecta();
                string sql = "DELETE FROM HOSPEDAGENS WHERE CODIGO = @CODIGO";
                resultado = banco.ExecuteNonQuery(sql, "@CODIGO", codigo);
                banco.Desconecta();
            }
            catch (SqlException sqle)
            {

            }


            return resultado;
        }

        public List<Hospedagens> consultar(String condicao)
        {
            string sql;
            if (condicao == "")
            {
                sql = "Select * from HOSPEDAGENS";
            }
            else
            {
                sql = "Select * from HOSPEDAGENS where " + condicao;
            }
            
            List<Hospedagens> lista = new List<Hospedagens>();
            DataTable dtCliente;
            try
            {
                banco.Conecta();
                bool sucesso = banco.ExecuteQuery(sql, out dtCliente);
                if (sucesso)
                {
                    if (dtCliente.Rows.Count > 0)
                    {
                      
                        foreach(DataRow linha in dtCliente.Rows)
                        {
                            Hospedagens hospedagem = new Hospedagens((int)linha["CODIGO"], linha["DESCRICAO"].ToString());
                            lista.Add(hospedagem);
                        }
                    }
                }
                banco.Desconecta();
            }
            catch (SqlException sqle)
            {

            }
            return lista;
        }
    }
}
