﻿using EstacionamentoBackoffice.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoBackoffice.Business.Interfaces
{
    public interface IFormaPagamentoRepository : IRepository<FormaPagamento>
    {
        Task<FormaPagamento> ObterPorCodigo(string codigo);
    }
}
