using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fippalace.Modelo
{
    class Acompanhantes
    {
        string nome, sexo, parentesco, cpf, cpfTitular;

        DateTime dataNascimento;

        public Acompanhantes()
        {

        }

        public Acompanhantes(string nome, string sexo, string parentesco, string cpf,string cpfTitular, DateTime dataNascimento)
        {
            this.nome = nome;
            this.sexo = sexo;
            this.parentesco = parentesco;
            this.cpf = cpf;
            this.dataNascimento = dataNascimento;
            this.cpfTitular = cpfTitular;
        }

        public string Nome { get => nome; set => nome = value; }
        public string Sexo { get => sexo; set => sexo = value; }
        public string Parentesco { get => parentesco; set => parentesco = value; }
        public string Cpf { get => cpf; set => cpf = value; }
        public string CpfTitular { get => cpfTitular; set => cpfTitular = value; }
        public DateTime DataNascimento { get => dataNascimento; set => dataNascimento = value; }
    }
}
