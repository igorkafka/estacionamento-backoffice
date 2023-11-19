using EstacionamentoBackoffice.Business.DomainObjects;
using EstacionamentoBackoffice.Business.Interfaces;
using EstacionamentoBackoffice.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoBackoffice.Business.Services
{
    public class FechamentoService : IFechamentoService
    {
        private readonly IPassagemRepository _passagemRepository;    
        public FechamentoService(IPassagemRepository passagemRepository)
        {

            _passagemRepository = passagemRepository;

        }
        public async Task<Fechamento> ObterFechamento(DateTime dataInicial, DateTime dataFinal, string codigoPagamento)
        {
            var passsagens = await _passagemRepository.ObterFechamento(dataInicial, dataFinal, codigoPagamento);
            var fechamento = new Fechamento() { Quantidade = passsagens.Count, ValorTotal = passsagens.Sum(x => x.PrecoTotal) };
            return fechamento;
        }
    }
}
