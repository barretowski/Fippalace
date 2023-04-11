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
    class HospedesDAO
    {
        private Banco banco;
        public HospedesDAO(Banco banco)
        {
            this.banco = banco;
        }
        public bool insert(Hospedes hospede)
        {
            bool resultado = false;

            

            try
            {
                banco.Conecta();
                string sql = "INSERT INTO HOSPEDES (CPF_TIT,NOME,DATANASC,DATAENTRADA,DATASAIDA,ESTADO_CIVIL,EMAIL,TELEFONE,CELULAR,CIDADE,ENDERECO,UF,SEXO,CODIGO_HOSPEDAGEM) VALUES " +
                    "                              (@CPF_TIT,@NOME,@DATANASC,@DATAENTRADA,@DATASAIDA,@ESTADO_CIVIL,@EMAIL,@TELEFONE,@CELULAR,@CIDADE,@ENDERECO,@UF,@SEXO,@CODIGO_HOSPEDAGEM)";
                resultado = banco.ExecuteNonQuery(sql, "@CPF_TIT", hospede.Cpf, 
                                                       "@CODIGO_HOSPEDAGEM",hospede.CodQuarto, 
                                                       "@DATANASC", hospede.DataNasc, 
                                                       "@DATAENTRADA", hospede.DataEntrada, 
                                                       "@DATASAIDA", hospede.DataSaida, 
                                                       "@NOME", hospede.Nome, 
                                                       "@EMAIL", hospede.Email, 
                                                       "@ESTADO_CIVIL", hospede.EstadoCivil, 
                                                       "@TELEFONE", hospede.Telefone, 
                                                       "@ENDERECO", hospede.Endereco, 
                                                       "@UF", hospede.Uf,
                                                       "@SEXO", hospede.Sexo, 
                                                       "@CELULAR", hospede.Celular, 
                                                       "@CIDADE", hospede.Cidade);

                
                banco.Desconecta();
            }
            catch (Exception sqle)
            {
                MessageBox.Show(sqle.Message,"erro");
            }
            return resultado;
        }

        public bool update(Hospedes hospede)
        {
            bool resultado = false;



            try
            {
                banco.Conecta();
                string sql = "UPDATE HOSPEDES SET CPF_TIT = @CPF_TIT, " +
                                                  "NOME = @NOME, " +
                                                  "DATANASC = @DATANASC, " +
                                                  "DATAENTRADA = @DATAENTRADA," +
                                                  "DATASAIDA = @DATASAIDA," +
                                                  "ESTADO_CIVIL = @ESTADO_CIVIL," +
                                                  "EMAIL = @EMAIL," +
                                                  "TELEFONE = @TELEFONE," +
                                                  "CELULAR = @CELULAR," +
                                                  "CIDADE = @CIDADE," +
                                                  "ENDERECO = @ENDERECO," +
                                                  "UF = @UF," +
                                                  "SEXO = @SEXO," +
                                                  "CODIGO_HOSPEDAGEM = @CODIGO_HOSPEDAGEM " +
                                                  "WHERE CPF_TIT = @CPF_TIT";
                resultado = banco.ExecuteNonQuery(sql, "@CPF_TIT", hospede.Cpf,
                                                       "@CODIGO_HOSPEDAGEM", hospede.CodQuarto,
                                                       "@DATANASC", hospede.DataNasc,
                                                       "@DATAENTRADA", hospede.DataEntrada,
                                                       "@DATASAIDA", hospede.DataSaida,
                                                       "@NOME", hospede.Nome,
                                                       "@EMAIL", hospede.Email,
                                                       "@ESTADO_CIVIL", hospede.EstadoCivil,
                                                       "@TELEFONE", hospede.Telefone,
                                                       "@ENDERECO", hospede.Endereco,
                                                       "@UF", hospede.Uf,
                                                       "@SEXO", hospede.Sexo,
                                                       "@CELULAR", hospede.Celular,
                                                       "@CIDADE", hospede.Cidade);


                banco.Desconecta();
            }
            catch (Exception sqle)
            {
                MessageBox.Show(sqle.Message, "erro");
            }
            return resultado;
        }
        public bool delete(string cpf)
        {
            bool resultado = false;

            try
            {
                banco.Conecta();
                string sql = "DELETE FROM HOSPEDES WHERE CPF_TIT = @CPF_TIT";
                resultado = banco.ExecuteNonQuery(sql, "@CPF_TIT", cpf);
                banco.Desconecta();
            }
            catch (Exception sqle)
            {

            }


            return resultado;
        }
        public List<Hospedes> consultar(String condicao)
        {
            string sql;
            if (condicao == "")
            {
                sql = "Select * from HOSPEDES";
            }
            else
            {
                sql = "Select * from HOSPEDES where " + condicao;
            }

            List<Hospedes> lista = new List<Hospedes>();
            DataTable dtCliente;
            try
            {
                banco.Conecta();
                bool sucesso = banco.ExecuteQuery(sql, out dtCliente);
                if (sucesso)
                {
                    if (dtCliente.Rows.Count > 0)
                    {

                        foreach (DataRow linha in dtCliente.Rows)
                        {
                            Hospedes hospede = new Hospedes();
                            hospede.Nome = linha["NOME"].ToString();
                            hospede.Cidade = linha["CIDADE"].ToString();
                            hospede.Email = linha["EMAIL"].ToString();
                            hospede.Endereco = linha["ENDERECO"].ToString();
                            hospede.DataEntrada = (DateTime) linha["DATAENTRADA"];
                            hospede.DataNasc = (DateTime)linha["DATANASC"];
                            hospede.DataSaida = (DateTime)linha["DATASAIDA"];
                            hospede.EstadoCivil = linha["ESTADO_CIVIL"].ToString();
                            hospede.CodQuarto = (int)linha["CODIGO_HOSPEDAGEM"];
                            hospede.Sexo = linha["SEXO"].ToString();
                            hospede.Uf = linha["UF"].ToString();
                            hospede.Celular = linha["CELULAR"].ToString();
                            hospede.Cpf = linha["CPF_TIT"].ToString();
                            hospede.Telefone = linha["TELEFONE"].ToString();
                            lista.Add(hospede);
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
