﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fippalace.Controle
{
    public interface IObservador
    {
        void notificar(string acao, params Object[] parametros);
    }
}
