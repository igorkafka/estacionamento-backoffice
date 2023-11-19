using AutoMapper;
using EstacionamentoBackoffice.API.Controllers;
using EstacionamentoBackoffice.API.ViewModels;
using EstacionamentoBackoffice.Business.Interfaces;
using EstacionamentoBackoffice.Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EstacionamentoBackoffice.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/tempo-medio")]
    public class TempoMedioController : MainController
    {
        private readonly ITempoService _tempoService;
        private readonly IMapper _mapper;
        public TempoMedioController(ITempoService tempoService, IMapper mapper, INotificador notificador,
                                      IUser user) : base(notificador, user)
        {
            _tempoService = tempoService;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<TempoMedioViewModel> ObterTempoMedio(DateTime dataInicial, DateTime dataFinal, string codigoPagamento)
        {
            return _mapper.Map<TempoMedioViewModel>(await _tempoService.ObterTempoMedio(dataInicial, dataFinal, codigoPagamento));
        }
        [AllowAnonymous]
        [HttpGet("obter-tempo-medio-mensalista")]
        public async Task<TempoMedioViewModel> ObterTempoMedioMensalista(DateTime dataInicial, DateTime dataFinal)
        {
            return _mapper.Map<TempoMedioViewModel>(await _tempoService.ObterTempoMedioMensalista(dataInicial, dataFinal));
        }
        [AllowAnonymous]
        [HttpGet("obter-tempo-nao-medio-mensalista")]
        public async Task<TempoMedioViewModel> ObterTempoMedioNaoMensalista(DateTime dataInicial, DateTime dataFinal)
        {
            return _mapper.Map<TempoMedioViewModel>(await _tempoService.ObterTempoMedioNaoMensalista(dataInicial, dataFinal));
        }
    }
}
