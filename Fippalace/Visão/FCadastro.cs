using Fippalace.Controle;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fippalace
{
    public partial class FCadastro : Form,IObservada
    {
        Controladora controle;
        public DataTable dtAcompa;

        private List<IObservador> observadores = new List<IObservador>();
        public DataRow acompanhante;
        public string acao;
        public FCadastro()
        {
            acao = "N";
            InitializeComponent();
            habilitarBotoes(acao);
            dtAcompa = new DataTable();
            dtAcompa.Columns.Add("cpf");
            dtAcompa.Columns.Add("nome");
            dtAcompa.Columns.Add("sexo");
            dtAcompa.Columns.Add("nascimento", typeof(DateTime));
            dtAcompa.Columns.Add("parentesco");
            



        }


        public void adicionarObservadores(IObservador observador)
        {
            if(observador.GetType() == typeof(Controladora))
            {
                controle = (Controladora)observador; 
            }
            observadores.Add(observador);
        }

        public void notificarObservadores(string acao)
        {
            foreach(IObservador observador in observadores)
            {
                if(acao!="P" && acao != "EA")
                {
                    observador.notificar(acao, ttbNome.Text, ttbCidade.Text, ttbEmail.Text, ttbEndereco.Text,
                        dtpEntrada.Value, dtpNasc.Value, dtpSaida.Value, cbCivil.SelectedItem, cbQuarto.SelectedItem,
                        cbSexo.SelectedItem, cbUF.SelectedItem, mtbCel.Text, mtbCPF.Text, mtbFixo.Text);
                }
                else if(acao == "EA")
                {
                    observador.notificar(acao, acompanhante["cpf"]);
                }
                
            }
        }

        // string acao = "N";
        public void habilitarBotoes(string acao)
        {
            ttbCidade.Enabled = "IA".IndexOf(acao)>=0;
            ttbEmail.Enabled= "IA".IndexOf(acao) >= 0;
            ttbEndereco.Enabled= "IA".IndexOf(acao) >= 0;
            ttbNome.Enabled= "IA".IndexOf(acao) >= 0;
            cbCivil.Enabled= "IA".IndexOf(acao) >= 0;
            cbQuarto.Enabled= "IA".IndexOf(acao) >= 0;
            cbSexo.Enabled= "IA".IndexOf(acao) >= 0;
            cbUF.Enabled= "IA".IndexOf(acao) >= 0;
            mtbCel.Enabled= "IA".IndexOf(acao) >= 0;
            mtbCPF.Enabled= "IA".IndexOf(acao) >= 0;
            mtbFixo.Enabled= "IA".IndexOf(acao) >= 0;
            dtpEntrada.Enabled= "IA".IndexOf(acao) >= 0;
            dtpNasc.Enabled= "IA".IndexOf(acao) >= 0;
            dtpSaida.Enabled= "IA".IndexOf(acao) >= 0;
            btnAdicionar.Enabled= "A".IndexOf(acao) >= 0;
            btnExcluir.Enabled= "IA".IndexOf(acao) >= 0;
            btnRemover.Enabled= "".IndexOf(acao) >= 0;
            btnConfirmar.Enabled= "IAE".IndexOf(acao) >= 0;
            btnAlterar.Enabled= "P".IndexOf(acao) >= 0;
            btnCancelar.Enabled= "IAEP".IndexOf(acao) >= 0;
            btnInserir.Enabled= "CN".IndexOf(acao) >= 0;
            btnPesquisar.Enabled = "CN".IndexOf(acao) >= 0;
            dgvAcompanhantes.Enabled = "A".IndexOf(acao) >= 0;
        }
        public void limpar()
        {
            ttbCidade.Text = "";
            ttbEmail.Text = "";
            ttbEndereco.Text = "";
            ttbNome.Text = "";
            cbCivil.Text = "";
            cbQuarto.Text = "";
            cbSexo.Text = "";
            cbUF.Text = "";
            mtbCel.Text = "";
            mtbCPF.Text = "";
            mtbFixo.Text = "";
            dtpEntrada.Text = "";
            dtpNasc.Text = "";
            dtpSaida.Text = "";
            dtAcompa.Rows.Clear();
        }
        private void BtnAdicionar_Click(object sender, EventArgs e)
        {
            controle.mostrarTelaAcompanhante();
        }

        private void BtnInserir_Click(object sender, EventArgs e)
        {
            acao = "I";
            habilitarBotoes(acao);
            limpar();
            
        }

        private void BtnPesquisar_Click(object sender, EventArgs e)
        {
            acao = "P";
          
            controle.mostrarTelaConsulta();
            habilitarBotoes(acao);
        }

        private void BtnAlterar_Click(object sender, EventArgs e)
        {
            acao = "A";
            habilitarBotoes(acao);
            
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            acao = "E";
            habilitarBotoes(acao);
           
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            
            
            notificarObservadores(acao);
            habilitarBotoes(acao);
            
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            acao = "N";
            habilitarBotoes(acao);
            limpar();
        }
        public void addItemTtbCidade()
        {
           
        }

        public void notificarObservadores()
        {
            throw new NotImplementedException();
        }
        
        public void preencheFormulario(DataRow hospede)
        {
            ttbCidade.Text = hospede["cidade"].ToString();
            ttbEmail.Text = hospede["email"].ToString();
            ttbEndereco.Text = hospede["endereco"].ToString();
            ttbNome.Text = hospede["nome"].ToString();
            cbCivil.SelectedIndex = cbCivil.Items.IndexOf(hospede["estado_civil"].ToString());
            cbQuarto.SelectedItem = hospede["quarto"];
            cbSexo.SelectedIndex = cbSexo.Items.IndexOf(hospede["sexo"].ToString());
            cbUF.Text = hospede["uf"].ToString();
            mtbCel.Text = hospede["celular"].ToString();
            mtbCPF.Text = hospede["cpf"].ToString(); 
            mtbFixo.Text = hospede["telefone"].ToString();
            dtpEntrada.Value = (DateTime)hospede["dataentrada"];
            dtpNasc.Value = (DateTime)hospede["datanasc"];
            dtpSaida.Value = (DateTime)hospede["datasaida"];
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            acao = "EA";
            notificarObservadores(acao);

        }

        private void dgvAcompanhantes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvAcompanhantes.CurrentRow != null)
            {
                int pos = dgvAcompanhantes.CurrentRow.Index;
                acompanhante = dtAcompa.Rows[pos];
                btnRemover.Enabled = true;
            }
        }
    }
}
