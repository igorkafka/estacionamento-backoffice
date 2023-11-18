using AutoMapper;
using EstacionamentoBackoffice.API.Controllers;
using EstacionamentoBackoffice.API.Extensions;
using EstacionamentoBackoffice.API.ViewModels;
using EstacionamentoBackoffice.Business.Interfaces;
using EstacionamentoBackoffice.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EstacionamentoBackoffice.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/FormasPagamento")]
    [ApiController]
    public class FormaPagamentoController : MainController
    {
        private readonly IFormaPagamentoRepository _formaPagamentoRepository;
        private readonly IFormaPagamentoService _formaPagamentoService;
        private readonly IMapper _mapper;

        public FormaPagamentoController(IFormaPagamentoRepository formaPagamentoRepository,
            IFormaPagamentoService formaPagamentoService,
                                      IMapper mapper,
                                      INotificador notificador,
                                      IUser user) : base(notificador, user)
        {
            _formaPagamentoRepository = formaPagamentoRepository;
            _formaPagamentoService = formaPagamentoService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<FormaPagamentoViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<FormaPagamentoViewModel>>(await _formaPagamentoRepository.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FormaPagamentoViewModel>> ObterPorId(Guid id)
        {
            var garagem = await ObterPorId(id);

            if (garagem == null) return NotFound();

            return garagem;
        }

        [HttpPost]
        public async Task<ActionResult<FormaPagamentoViewModel>> Adicionar(FormaPagamentoViewModel formaPagamentoViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _formaPagamentoService.Adicionar(_mapper.Map<FormaPagamento>(formaPagamentoViewModel));

            return CustomResponse(formaPagamentoViewModel);
        }

        [ClaimsAuthorize("Fornecedor", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<FormaPagamentoViewModel>> Atualizar(Guid id, FormaPagamentoViewModel formaPagamentoViewModel)
        {
            if (id != formaPagamentoViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(formaPagamentoViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _formaPagamentoService.Atualizar(_mapper.Map<FormaPagamento>(formaPagamentoViewModel));

            return CustomResponse(formaPagamentoViewModel);
        }

        [ClaimsAuthorize("Fornecedor", "Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FormaPagamentoViewModel>> Excluir(Guid id)
        {
            var garagemViewModel = await ObterPorId(id);

            if (garagemViewModel == null) return NotFound();

            await _formaPagamentoService.Remover(id);

            return CustomResponse(garagemViewModel);
        }
    }
}
