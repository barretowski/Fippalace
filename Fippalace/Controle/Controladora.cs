using Fippalace.Visão;
using Fippalace.Modelo;
using Fippalace.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace Fippalace.Controle
{
    class Controladora: IObservador
    {
        private static Controladora instancia = null;
        private static Object trava = new object();
        FAcompanhante fAcompanhante;
        List<Hospedes> hospedes;

        FConsulta fConsulta;
        FCadastro fCadastro;
        FInicial fInicial;
        FSobre fSobre;
        private Controladora()
        {

        }
        public void show()
        {
            fInicial = new FInicial();
            fInicial.adicionarObservadores(this);
            fInicial.ShowDialog();
        }
        public void mostraTelaSobre()
        {
            fSobre = new FSobre();
            fSobre.ShowDialog();
        }
        public void mostrarTelaAcompanhante()
        {
            //carregar o banco
            fAcompanhante = new FAcompanhante();
            fAcompanhante.adicionarObservadores(this);
            fAcompanhante.ShowDialog();
        }
        public void mostrarTelaConsulta()
        {
            //carregar o banco
            fConsulta = new FConsulta();
            fConsulta.adicionarObservadores(this);
            fConsulta.ShowDialog();
        }
        public void mostraTelaCadastro()
        {
            fCadastro = new FCadastro();
            fCadastro.adicionarObservadores(this);
            listarHospedagens();
            fCadastro.ShowDialog();
        }
        
        public static Controladora obterInstancia()
        {
            lock(trava)
            {
                if (instancia == null)
                    instancia = new Controladora();

                return instancia;
            }
        }
        public void notificar(string acao, params object[] parametros)
        {
            //abrir banco
            switch (acao)
            {
                case "I":
                    inserirHospede(parametros);
                    fCadastro.acao = "N";
                     break;
                case "IA": //INSERIR ACOMPANHANTE
                    inserirAcompanhante(fCadastro.mtbCPF.Text, parametros);
                    fAcompanhante.Close();
                    listaAcompahantes();
               
                    break;
                case "EA": //EXCLUE ACOMPANHANTE
                    exclueAcompanhante(parametros[0].ToString());
                    listaAcompahantes();
                  
                    break;
                case "A":
                    atualizarHospede(parametros);
                    fCadastro.acao = "P";
                    break;

                case "E":
                    execluirHospede(parametros);
                    fCadastro.acao = "N";
                    fCadastro.limpar();
                    break;
                case "P":
                    consultaHospede(parametros);
                    
                    break;
                case "NC":
                    fCadastro.acao = "N";
                    break;
                case "AH":
                        int pos = (int)parametros[0];
                        
                        fCadastro.preencheFormulario(fConsulta.Hospedes.Rows[pos]);
                        listaAcompahantes();
                        fConsulta.Close();
                    break;



            }


        }
        public void listarHospedagens()
        {
            HospedagemDAO daoHospe = new HospedagemDAO(new BancoSQLServer());
            List <Hospedagens> listaHospe = daoHospe.consultar("");

            foreach (Hospedagens hosp in listaHospe)
            {
                fCadastro.cbQuarto.Items.Add(hosp);
            }

        }

        //HOSPEDE----------------------------------------------------------------------------------------
        public bool inserirHospede(object[] parametros)
        {
            bool inserido = false;
            HospedesDAO daoHospedes = new HospedesDAO(new BancoSQLServer());
            Hospedes hospede = new Hospedes();
            if (parametros[0] != null && parametros[1] != null && parametros[2] != null && parametros[3] != null &&
               parametros[7] != null && parametros[9] != null && parametros[10] != null && parametros[11] != null &&
               parametros[12] != null && parametros[13] != null)
            {
                hospede.Nome = parametros[0].ToString();
                hospede.Cidade = parametros[1].ToString();
                hospede.Email = parametros[2].ToString();
                hospede.Endereco = parametros[3].ToString();
                hospede.DataEntrada = (DateTime)parametros[4];
                hospede.DataNasc = (DateTime)parametros[5];
                hospede.DataSaida = (DateTime)parametros[6];
                hospede.EstadoCivil = parametros[7].ToString();
                hospede.CodQuarto = ((Hospedagens)parametros[8]).Codigo;
                hospede.Sexo = parametros[9].ToString();
                hospede.Uf = parametros[10].ToString();
                hospede.Celular = parametros[11].ToString();
                hospede.Cpf = parametros[12].ToString();
                hospede.Telefone = parametros[13].ToString();
                inserido = daoHospedes.insert(hospede);
            }
            else
                MessageBox.Show("Todos os campos devem ser preenchidos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            return inserido;
        }
        public bool atualizarHospede(object[] parametros)
        {
            bool inserido = false;
            HospedesDAO daoHospedes = new HospedesDAO(new BancoSQLServer());
            Hospedes hospede = new Hospedes();
            hospede.Nome = parametros[0].ToString();
            hospede.Cidade = parametros[1].ToString();
            hospede.Email = parametros[2].ToString();
            hospede.Endereco = parametros[3].ToString();
            hospede.DataEntrada = (DateTime)parametros[4];
            hospede.DataNasc = (DateTime)parametros[5];
            hospede.DataSaida = (DateTime)parametros[6];
            hospede.EstadoCivil = parametros[7].ToString();
            hospede.CodQuarto = ((Hospedagens)parametros[8]).Codigo;
            hospede.Sexo = parametros[9].ToString();
            hospede.Uf = parametros[10].ToString();
            hospede.Celular = parametros[11].ToString();
            hospede.Cpf = parametros[12].ToString();
            hospede.Telefone = parametros[13].ToString();
            inserido = daoHospedes.update(hospede);

            return inserido;
        }
        public void execluirHospede(object[] parametros)
        {
			//BANCO DE DADOS FOI CRIADO COM  "ON DELETE CASCADE" PORTANTO O BANCO APAGA TODOS OS ACOMPANHANTES DO HOSPEDE 
            HospedesDAO daoHospedes = new HospedesDAO(new BancoSQLServer());
            daoHospedes.delete(parametros[12].ToString());
        }
        public void consultaHospede(object[] parametros)
        {
            HospedesDAO daoHospedes = new HospedesDAO(new BancoSQLServer());
            HospedagemDAO daoHospedagem = new HospedagemDAO(new BancoSQLServer());
            hospedes = daoHospedes.consultar("NOME  LIKE '%" + fConsulta.ttbPesquisar.Text + "%'");
            fConsulta.dgvPesquisa.Rows.Clear();
            DataTable dtHospede = new DataTable();
            dtHospede.Columns.Add("cpf");
            dtHospede.Columns.Add("nome");
            dtHospede.Columns.Add("cidade");
            dtHospede.Columns.Add("quarto", typeof(Hospedagens));
            dtHospede.Columns.Add("datanasc", typeof(DateTime));
            dtHospede.Columns.Add("dataentrada", typeof(DateTime));
            dtHospede.Columns.Add("datasaida", typeof(DateTime));
            dtHospede.Columns.Add("uf");
            dtHospede.Columns.Add("sexo");
            dtHospede.Columns.Add("endereco");
            dtHospede.Columns.Add("celular");
            dtHospede.Columns.Add("telefone");
            dtHospede.Columns.Add("estado_civil");
            dtHospede.Columns.Add("email");

            foreach (Hospedes hosp in hospedes)
            {
                DataRow linha = dtHospede.NewRow();
                linha["cpf"] = hosp.Cpf;
                linha["nome"] = hosp.Nome;
                linha["cidade"] = hosp.Cidade;
                linha["quarto"] = daoHospedagem.consultar("CODIGO = "+ hosp.CodQuarto)[0];
                linha["datanasc"] = hosp.DataNasc;
                linha["dataentrada"] = hosp.DataEntrada;
                linha["datasaida"] = hosp.DataSaida;
                linha["uf"] = hosp.Uf;
                linha["endereco"] = hosp.Endereco;
                linha["sexo"] = hosp.Sexo;
                linha["telefone"] = hosp.Telefone;
                linha["celular"] = hosp.Celular;
                linha["estado_civil"] = hosp.EstadoCivil;
                linha["email"] =hosp.Email;
                dtHospede.Rows.Add(linha);
            }
            fConsulta.Hospedes = dtHospede;
            fConsulta.dgvPesquisa.DataSource = fConsulta.Hospedes;

        }

        //ACOMPANHANTE----------------------------------------------------------------------------------------------------------------------

        public void inserirAcompanhante(string CpfTitular,object[] parametros)
        {
            Acompanhantes acompa = new Acompanhantes();
            AcompanhantesDAO daoAcompa = new AcompanhantesDAO(new BancoSQLServer());
            acompa.Nome = parametros[0].ToString();
            acompa.Parentesco = parametros[1].ToString();
            acompa.Sexo = parametros[2].ToString();
            acompa.DataNascimento = (DateTime)parametros[3];
            acompa.Cpf = parametros[4].ToString();
            acompa.CpfTitular = CpfTitular;
            daoAcompa.insert(acompa);

            /*
             ttbNome.Text,
             cbParente.SelectedIndex,
             cbSexo.Text,
             dtpNascimento.Value,
             mtbCPF.Text
             */
        }
        public void listaAcompahantes() 
        {
            AcompanhantesDAO daoAcompa = new AcompanhantesDAO(new BancoSQLServer());
            DataTable dtAcompa = new DataTable();
            DataRow linha;
            dtAcompa.Columns.Add("cpf");
            dtAcompa.Columns.Add("nome");
            dtAcompa.Columns.Add("sexo");
            dtAcompa.Columns.Add("nascimento", typeof(DateTime));
            dtAcompa.Columns.Add("parentesco");
            fCadastro.dtAcompa = dtAcompa;
            foreach (Acompanhantes acompa in daoAcompa.consultar("CPF_TIT = '"+fCadastro.mtbCPF.Text+"'"))
            {
                linha = dtAcompa.NewRow();
                linha["cpf"] = acompa.Cpf;
                linha["nome"] = acompa.Nome;
                linha["sexo"] = acompa.Sexo;
                linha["nascimento"] = acompa.DataNascimento;
                linha["parentesco"] = acompa.Parentesco;
                dtAcompa.Rows.Add(linha);
            }
            fCadastro.dgvAcompanhantes.DataSource = dtAcompa;


        }

        public void exclueAcompanhante(string cpf)
        {
            AcompanhantesDAO daoAcompa = new AcompanhantesDAO(new BancoSQLServer());
            daoAcompa.delete(cpf);
        }

    }
}
