using EstacionamentoBackoffice.Business.DomainObjects;
using EstacionamentoBackoffice.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoBackoffice.Business.Interfaces
{
    public interface IFechamentoService
    {
        Task<Fechamento> ObterFechamento(DateTime dataInicial, DateTime dataFinal, string codigoPagamento);
    }
}
