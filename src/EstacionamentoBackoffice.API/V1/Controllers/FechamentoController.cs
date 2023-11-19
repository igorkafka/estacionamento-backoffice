using AutoMapper;
using EstacionamentoBackoffice.API.Controllers;
using EstacionamentoBackoffice.API.ViewModels;
using EstacionamentoBackoffice.Business.Interfaces;
using EstacionamentoBackoffice.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EstacionamentoBackoffice.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/fechamentos")]
    public class FechamentoController : MainController
    {
        private readonly IFechamentoService _fechamentoService;
        private readonly IMapper _mapper;
        public FechamentoController(IFechamentoService fechamentoService, IMapper mapper, INotificador notificador,
                                      IUser user) : base(notificador, user)
        {
             _fechamentoService = fechamentoService;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpGet("fechamentos")]
        public async Task<FechamentoViewModel> ObterTodosObterCarrosAindaNaGaragem(DateTime dataInicial, DateTime dataFinal, string codigoPagamento)
        {
            return _mapper.Map<FechamentoViewModel>(await _fechamentoService.ObterFechamento(dataInicial, dataFinal, codigoPagamento));
        }
    }
}
