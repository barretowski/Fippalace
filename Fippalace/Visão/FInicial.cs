using System;
using Fippalace.Controle;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fippalace.Visão
{
    public partial class FInicial : Form,IObservada
    {
        private List<IObservador> observadores = new List<IObservador>();
        Controladora controle;
        public FInicial()
        {
            InitializeComponent();
        }

        public void adicionarObservadores(IObservador observador)
        {
            if (observador.GetType() == typeof(Controladora))
            {
                controle = (Controladora)observador;
            }
            observadores.Add(observador);
        }

        public void notificarObservadores()
        {
            throw new NotImplementedException();
        }

        private void CadastroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controle.mostraTelaCadastro();
        }

        private void SobreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controle.mostraTelaSobre();
        }
    }
}
