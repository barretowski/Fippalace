using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fippalace.Modelo;
namespace Fippalace.DB
{
    class AcompanhantesDAO
    {

        private Banco banco;
        public AcompanhantesDAO(Banco banco)
        {
            this.banco = banco;
        }

        public bool insert(Acompanhantes acompanhante)
        {
            bool resultado = false;

            try
            {
                banco.Conecta();
                string sql = "insert into ACOMPANHANTES (CPF_DEP, CPF_TIT, NOME, GRAU_PARENTESCO, SEXO, DATANASC)" +
                             "values(@CPF_DEP,@CPF_TIT, @NOME, @GRAU_PARENTESCO, @SEXO,@DATANASC)";
                resultado = banco.ExecuteNonQuery(sql, "@CPF_DEP", acompanhante.Cpf, "@CPF_TIT", acompanhante.CpfTitular, "@NOME", acompanhante.Nome, "@GRAU_PARENTESCO", acompanhante.Parentesco, "@SEXO", acompanhante.Sexo, "@DATANASC", acompanhante.DataNascimento);

                banco.Desconecta();
            }
            catch (SqlException sqle)
            {
                MessageBox.Show(sqle.Message, "ERRO");
            }


            return resultado;
        }
   

        public bool update(Acompanhantes acompanhante)
        {
            bool resultado = false;

            try
            {
                banco.Conecta();
                string sql = "UPDATE ACOMPANHANTES  SET   CPF_TIT = @CPF_TIT, NOME = @NOME, GRAU_PARENTESCO = @GRAU_PARENTESCO, SEXO = @SEXO,DATANASC = @DATANASC WHERE CPF_DEP = @CPF_DEP";
                resultado = banco.ExecuteNonQuery(sql, "@CPF_DEP", acompanhante.Cpf, "@CPF_TIT", acompanhante.CpfTitular, "@NOME", acompanhante.Nome, "@GRAU_PARENTESCO", acompanhante.Parentesco, "@SEXO", acompanhante.Sexo, "@DATANASC", acompanhante.DataNascimento);
                banco.Desconecta();
            }
            catch (SqlException sqle)
            {

            }


            return resultado;
        }

        public bool delete(string cpf)
        {
            bool resultado = false;

            try
            {
                banco.Conecta();
                string sql = "DELETE FROM ACOMPANHANTES WHERE CPF_DEP = @CPF_DEP";
                resultado = banco.ExecuteNonQuery(sql, "@CPF_DEP", cpf);
                banco.Desconecta();
            }
            catch (SqlException sqle)
            {

            }


            return resultado;
        }

        public List<Acompanhantes> consultar(String condicao)
        {
            string sql;
            if (condicao == "")
            {
                sql = "Select * from ACOMPANHANTES";
            }
            else
            {
                sql = "Select * from ACOMPANHANTES where " + condicao;
            }

            List<Acompanhantes> lista = new List<Acompanhantes>();
            DataTable dt;
            try
            {
                banco.Conecta();
                bool sucesso = banco.ExecuteQuery(sql, out dt);
                if (sucesso)
                {
                    if (dt.Rows.Count > 0)
                    {
                      
                        foreach (DataRow linha in dt.Rows)
                        {
                            Acompanhantes acompa = new Acompanhantes();
                            acompa.Nome = linha["NOME"].ToString();
                            acompa.Parentesco = linha["GRAU_PARENTESCO"].ToString();
                            acompa.Sexo = linha["SEXO"].ToString();
                            acompa.DataNascimento = (DateTime)linha["DATANASC"];
                            acompa.Cpf = linha["CPF_DEP"].ToString();
                            acompa.CpfTitular = linha["CPF_TIT"].ToString();
                            lista.Add(acompa);
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

