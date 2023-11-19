using EstacionamentoBackoffice.Business.Interfaces;
using EstacionamentoBackoffice.Business.Models;
using EstacionamentoBackoffice.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoBackoffice.Data.Repository
{
    public class CarroRepository : Repository<Carro>, ICarroRepository
    {
        public CarroRepository(MeuDbContext context) : base(context) { }

        public async Task<IQueryable<Carro>> ObterCarroPorPeriodo(DateTime dataHoraInicial, DateTime dataHoraFinal)
        {
            var carros = await Db.Passagens
       .Where(x => x.DataHoraSaida >= dataHoraInicial && x.DataHoraEntrada <= dataHoraFinal)
       .Select(x => x.Carro)
       .ToListAsync();

            return carros.AsQueryable();
        }

        public async Task<IQueryable<Carro>> ObterCarrosAindaNaGaragem(DateTime dataHoraInicial)
        {
            var carros = await Db.Passagens
                .Where(x => x.DataHoraSaida == null && x.DataHoraEntrada == dataHoraInicial)
                .Include(x => x.Carro)
                .Select(x => x.Carro)
                .ToListAsync();

            return carros.AsQueryable();
        }

        public async Task<IQueryable<Carro>> ObterCarrosForaGaragem()
        {
            var carros = await Db.Passagens
                .Where(x => DateTime.Now > x.DataHoraSaida)
                .Include(x => x.Carro)
                .Select(x => x.Carro)
                .ToListAsync();

            return carros.AsQueryable();
        }

        public Task<Carro> ObterPorPlaca(string placa)
        {
            return DbSet.FirstAsync(x => x.Placa == placa);
        }
    }
}
