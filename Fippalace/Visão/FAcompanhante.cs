using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fippalace.Controle;

namespace Fippalace.Visão
{
    public partial class FAcompanhante : Form, IObservada
    {
        private List<IObservador> observadores = new List<IObservador>();
        string acao = "N";
        public FAcompanhante()      
        {
            InitializeComponent();
            habilitarBotoes(acao);
        }

        public void adicionarObservadores(IObservador observador)
        {
            observadores.Add(observador);
        }
        /*
         ttbNome.Enabled 
        cbParente.Enabled 
        cbSexo.Enabled 
        dtpNascimento.Enabled 
        mtbCPF.Enabled 
        btnCancelar.Enabled 
        btnIncluir.Enabled 
        btnNovo.Enabled 
             */
        public void notificarObservadores()
        {
            foreach (IObservador observador in observadores)
            {
        
                    observador.notificar(acao, ttbNome.Text,
                                                cbParente.Text,
                                                cbSexo.Text,
                                                dtpNascimento.Value,
                                                mtbCPF.Text
                                                );

            }
        }
        void limpar()
        {
            ttbNome.Text = "";
            cbParente.SelectedItem = "";
            cbSexo.SelectedItem = "";
            dtpNascimento.Text = "";
        }
        void habilitarBotoes(string acao)
        {
            ttbNome.Enabled = "B".IndexOf(acao)>=0;
            cbParente.Enabled = "B".IndexOf(acao) >= 0;
            cbSexo.Enabled = "B".IndexOf(acao)>=0;
            dtpNascimento.Enabled = "B".IndexOf(acao)>=0;
            mtbCPF.Enabled = "B".IndexOf(acao) >=0;
            btnCancelar.Enabled = "B".IndexOf(acao) >= 0;
            btnIncluir.Enabled = "B".IndexOf(acao) >= 0;
            btnNovo.Enabled = "NI".IndexOf(acao) >= 0;

        }
        private void BtnIncluir_Click(object sender, EventArgs e)
        {
            acao = "IA";
            habilitarBotoes(acao);
            notificarObservadores();
        }

        private void BtnNovo_Click(object sender, EventArgs e)
        {
            acao = "B";
            limpar();
            habilitarBotoes(acao);
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            acao = "N";
            habilitarBotoes(acao);
            limpar();
        }
    }
}
