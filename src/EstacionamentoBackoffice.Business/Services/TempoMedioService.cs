using EstacionamentoBackoffice.Business.DomainObjects;
using EstacionamentoBackoffice.Business.Extensions;
using EstacionamentoBackoffice.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoBackoffice.Business.Services
{
    public class TempoMedioService : ITempoService
    {
        private readonly IPassagemRepository _passagemRepository;
        public TempoMedioService(IPassagemRepository passagemRepository)
        {

            _passagemRepository = passagemRepository;

        }
        public async Task<TempoMedio> ObterTempoMedio(DateTime dataInicial, DateTime dataFinal, string codigoPagamento)
        {
            var passsagens = await _passagemRepository.ObterFechamento(dataInicial, dataFinal, codigoPagamento);
            var timespan = passsagens.Select(x => x.DataHoraSaida.Value.Subtract(x.DataHoraEntrada));
            var tempoMedioExtraido = timespan.Extrair();
            var tempoMedio = new TempoMedio() { Media = tempoMedioExtraido };
            return tempoMedio;
        }
        public async Task<TempoMedio> ObterTempoMedioMensalista(DateTime dataInicial, DateTime dataFinal)
        {
            var passsagens = await _passagemRepository.ObterFechamento(dataInicial, dataFinal, "MEN");
            var timespan = passsagens.Select(x => x.DataHoraSaida.Value.Subtract(x.DataHoraEntrada));
            var tempoMedioExtraido = timespan.Extrair();
            var tempoMedio = new TempoMedio() { Media = tempoMedioExtraido };
            return tempoMedio;
        }

        public async Task<TempoMedio> ObterTempoMedioNaoMensalista(DateTime dataInicial, DateTime dataFinal)
        {
            var passsagens = await _passagemRepository.ObterFechamentoNaoMensalista(dataInicial, dataFinal);
            var timespan = passsagens.Select(x => x.DataHoraSaida.Value.Subtract(x.DataHoraEntrada));
            var tempoMedioExtraido = timespan.Extrair();
            var tempoMedio = new TempoMedio() { Media = tempoMedioExtraido };
            return tempoMedio;
        }
    }
}
