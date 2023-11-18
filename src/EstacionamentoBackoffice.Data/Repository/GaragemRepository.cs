using EstacionamentoBackoffice.Business.Interfaces;
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
    public class GaragemRepository : Repository<Garagem>, IGaragemRepository
    {
        public GaragemRepository(MeuDbContext context) : base(context) { }

        public Task<Garagem> ObterPorCodigo(string codigo)
        {
           return DbSet.FirstAsync(x => x.Codigo == codigo);
        }
    }
}
