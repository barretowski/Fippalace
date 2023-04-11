using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fippalace.Modelo
{
    [Serializable]
    class Hospedes
    {
        private int codQuarto;
        private DateTime dataNasc, dataEntrada, dataSaida;
        private string cpf,nome, email, estadoCivil, telefone, endereco,uf,sexo, celular, cidade;

        public  Hospedes()
        {
           
        }

        public Hospedes(string cpf, int codQuarto, DateTime dataNasc, DateTime dataEntrada, DateTime dataSaida, string nome, string email, string estadoCivil, string telefone, string endereco, string uf, string sexo, string celular, string cidade)
        {
            this.cpf = cpf;
            this.codQuarto = codQuarto;
            this.dataNasc = dataNasc;
            this.dataEntrada = dataEntrada;
            this.dataSaida = dataSaida;
            this.nome = nome;
            this.email = email;
            this.estadoCivil = estadoCivil;
            this.telefone = telefone;
            this.endereco = endereco;
            this.uf = uf;
            this.sexo = sexo;
            this.celular = celular;
            this.cidade = cidade;
        }

        public string Cpf { get => cpf; set => cpf = value; }
        public int CodQuarto { get => codQuarto; set => codQuarto = value; }
        public DateTime DataNasc { get => dataNasc; set => dataNasc = value; }
        public DateTime DataEntrada { get => dataEntrada; set => dataEntrada = value; }
        public DateTime DataSaida { get => dataSaida; set => dataSaida = value; }
        public string Nome { get => nome; set => nome = value; }
        public string Email { get => email; set => email = value; }
        public string EstadoCivil { get => estadoCivil; set => estadoCivil = value; }
        public string Telefone { get => telefone; set => telefone = value; }
        public string Endereco { get => endereco; set => endereco = value; }
        public string Uf { get => uf; set => uf = value; }
        public string Sexo { get => sexo; set => sexo = value; }
        public string Celular { get => celular; set => celular = value; }
        public string Cidade { get => cidade; set => cidade = value; }
    }
}

    
