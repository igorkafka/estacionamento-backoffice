﻿using EstacionamentoBackoffice.Business.Interfaces;
using EstacionamentoBackoffice.Business.Models;
using EstacionamentoBackoffice.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoBackoffice.Data.Repository
{
    public class FormaPagamentoRepository : Repository<FormaPagamento>, IFormaPagamentoRepository
    {
        public FormaPagamentoRepository(MeuDbContext context) : base(context) { }

        public Task<FormaPagamento> ObterPorCodigo(string codigo)
        {
            return DbSet.FirstAsync(x => x.Codigo == codigo);
        }
    }
}
