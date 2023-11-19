using EstacionamentoBackoffice.Business.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoBackoffice.Business.Interfaces
{
    public interface ITempoService
    {
        Task<TempoMedio> ObterTempoMedio(DateTime dataInicial, DateTime dataFinal, string codigoPagamento);
        Task<TempoMedio> ObterTempoMedioNaoMensalista(DateTime dataInicial, DateTime dataFinal);
        Task<TempoMedio> ObterTempoMedioMensalista(DateTime dataInicial, DateTime dataFinal);

    }
}
