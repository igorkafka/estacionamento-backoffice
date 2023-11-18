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
    }
}
