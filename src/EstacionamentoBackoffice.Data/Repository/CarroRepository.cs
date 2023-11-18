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
    public class CarroRepository : Repository<Carro>, ICarroRepository
    {
        public CarroRepository(MeuDbContext context) : base(context) { }

        public Task<Carro> ObterPorPlaca(string placa)
        {
            return DbSet.FirstAsync(x => x.Placa == placa);
        }
    }
}
