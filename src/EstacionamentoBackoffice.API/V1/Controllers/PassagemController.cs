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
    [Route("api/v{version:apiVersion}/Passagens")]
    public class PassagemController : MainController
    {
        private readonly IPassagemRepository _passagemRepository;
        private readonly IPassagemService _passagemService;
        private readonly IFormaPagamentoRepository _formaPagamentoRepository;
        private readonly IGaragemRepository _garagemRepository;
        private readonly ICarroRepository _carroRepository;

        private readonly IMapper _mapper;

        public PassagemController(IPassagemRepository passagemRepository,
            IPassagemService passagemService,
            IFormaPagamentoRepository formaPagamentoRepository,
            IGaragemRepository garagemRepository,
            ICarroRepository carroRepository,
                                      IMapper mapper,
                                      INotificador notificador,
                                      IUser user) : base(notificador, user)
        {
            _passagemRepository = passagemRepository;
            _passagemService = passagemService;
            _mapper = mapper;
            _carroRepository = carroRepository;
            _formaPagamentoRepository = formaPagamentoRepository;
            _garagemRepository = garagemRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<FormaPagamentoViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<FormaPagamentoViewModel>>(await _passagemRepository.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PassagemViewModel>> ObterPorId(Guid id)
        {
            var passagem = await ObterPorId(id);

            if (passagem == null) return NotFound();

            return passagem;
        }

        [HttpPost]
        public async Task<ActionResult<PassagemViewModel>> Adicionar(PassagemViewModel passagemViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            
            var carro = await _carroRepository.ObterPorPlaca(passagemViewModel.Placa);
            if (carro == null)
            {
                return NotFound();
            }
            
            var formaPagamento = await _formaPagamentoRepository.ObterPorCodigo(passagemViewModel.FormaPagamentoCodigo);

            if (formaPagamento == null)
            {
                NotificarErro("Forma de Pagamento não encontrada");
                return CustomResponse();
            }

            var garagem =  await _garagemRepository.ObterPorCodigo(passagemViewModel.CodigoGarragem);

            if (garagem == null)
            {
                NotificarErro("Garagem não encontrada");
                return CustomResponse();
            }

            Passagem passagem = _mapper.Map<Passagem>(passagemViewModel);

            passagem.GaragemId = garagem.Id;
            passagem.Garagem = garagem;
            passagem.CarroId = carro.Id;
            passagem.Carro = carro;
            passagem.FormaPagamentoId = formaPagamento.Id;
            passagem.FormaPagamento = formaPagamento;

            await _passagemService.Adicionar(passagem);

            return CustomResponse(passagemViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<FormaPagamentoViewModel>> Atualizar(Guid id, FormaPagamentoViewModel formaPagamentoViewModel)
        {
            if (id != formaPagamentoViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(formaPagamentoViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _passagemService.Atualizar(_mapper.Map<Passagem>(formaPagamentoViewModel));

            return CustomResponse(formaPagamentoViewModel);
        }

        [ClaimsAuthorize("Fornecedor", "Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<PassagemViewModel>> Excluir(Guid id)
        {
            var garagemViewModel = await ObterPorId(id);

            if (garagemViewModel == null) return NotFound();

            await _passagemService.Remover(id);

            return CustomResponse(garagemViewModel);
        }
    }
}
