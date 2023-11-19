using EstacionamentoBackoffice.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoBackoffice.Business.Interfaces
{
    public interface IPassagemRepository : IRepository<Passagem>
    {
        Task AdicionarPassagem(Passagem passagem);
        Task<List<Passagem>> ObterFechamento(DateTime dataInicial, DateTime dataFinal, string codigoPagamento);
        Task<List<Passagem>> ObterFechamentoNaoMensalista(DateTime dataInicial, DateTime dataFinal);
    }
}
