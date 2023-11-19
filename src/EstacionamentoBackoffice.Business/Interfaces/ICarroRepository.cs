using EstacionamentoBackoffice.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoBackoffice.Business.Interfaces
{
    public interface ICarroRepository : IRepository<Carro>
    {
        Task<Carro> ObterPorPlaca(string placa);
        Task<IQueryable<Carro>> ObterCarroPorPeriodo(DateTime dataHoraInicial, DateTime dataHoraFinal);
        Task<IQueryable<Carro>> ObterCarrosAindaNaGaragem(DateTime dataHoraInicial);
        Task<IQueryable<Carro>> ObterCarrosForaGaragem();
    }
}
