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
    public partial class FConsulta : Form, IObservada
    {
        private List<IObservador> observadores = new List<IObservador>();
        public DataTable Hospedes;
        public string acao = "NC";
        public FConsulta()
        {
            InitializeComponent();
        }

        private void FConsulta_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //pesquisar
            acao = "P";
            notificarObservadores();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }


        public void adicionarObservadores(IObservador observador)
        {
            observadores.Add(observador);
        }

        public void notificarObservadores()
        {
            foreach (IObservador observador in observadores)
            {

                if (acao == "AH")
                {

                    observador.notificar(acao, dgvPesquisa.CurrentRow.Index);
                }
                else
                {
                    observador.notificar(acao, ttbPesquisar.Text);
                }

            }
        }

       

        private void dgvPesquisa_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPesquisa.CurrentRow != null)
            {
                acao = "AH";
                notificarObservadores();
            }
        }

        private void FConsulta_FormClosing(object sender, FormClosingEventArgs e)
        {
           if(acao == "NC")
           {
                notificarObservadores();
            }
            
        }
    }
}
