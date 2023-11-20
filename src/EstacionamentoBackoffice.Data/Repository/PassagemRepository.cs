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
    public class PassagemRepository : Repository<Passagem>, IPassagemRepository
    {
        public PassagemRepository(MeuDbContext context) : base(context) { }

        public async Task AdicionarPassagem(Passagem passagem)
        {
            using (var context = Db)
            {
              

                // Attach the entity as added (detached)
                context.Attach(passagem).State = EntityState.Added;

                await context.SaveChangesAsync();
            }
        }

        public async Task AtualizarPassagem(Passagem passagem)
        {
            using (var context = Db)
            {


                // Attach the entity as added (detached)
                context.Attach(passagem).State = EntityState.Modified;

                await context.SaveChangesAsync();
            }
        }

        public async Task<List<Passagem>> ObterFechamento(DateTime dataInicial, DateTime dataFinal, string codigoPagamento)
        {
            return await DbSet.Where(x => x.DataHoraSaida >= dataInicial && x.DataHoraEntrada <= dataFinal && x.FormaPagamento.Codigo == codigoPagamento).ToListAsync();

        }
        public async Task<List<Passagem>> ObterFechamentoNaoMensalista(DateTime dataInicial, DateTime dataFinal)
        {
            return await DbSet.Where(x => x.DataHoraSaida >= dataInicial && x.DataHoraEntrada <= dataFinal && x.FormaPagamento.Codigo != "MEN").ToListAsync();

        }
    }
}
