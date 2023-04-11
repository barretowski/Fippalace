using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fippalace.Modelo
{
    class Hospedagens
    {
        int codigo;
        string descricao;
        public Hospedagens()
        {

        }

        public Hospedagens(int codigo, string descricao)
        {
            this.codigo = codigo;
            this.descricao = descricao;
        }

        public int Codigo { get => codigo; set => codigo = value; }
        public string Descricao { get => descricao; set => descricao = value; }

        public override string ToString()
        {
            return descricao;
        }
    }
   
}
