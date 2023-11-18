using AutoMapper;
using EstacionamentoBackoffice.API.Controllers;
using EstacionamentoBackoffice.API.Extensions;
using EstacionamentoBackoffice.API.ViewModels;
using EstacionamentoBackoffice.Business.Interfaces;
using EstacionamentoBackoffice.Business.Models;
using EstacionamentoBackoffice.Business.Services;
using EstacionamentoBackoffice.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EstacionamentoBackoffice.API.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/carros")]
    public class CarrosController : MainController
    {
        private readonly ICarroRepository _carroRepository;
        private readonly ICarroService _carroService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CarrosController(INotificador notificador,
                                  ICarroRepository carroRepository,
                                  ICarroService carroService,
                                  IMapper mapper,
                                  IUser user,
                                  IHttpContextAccessor httpContextAccessor) : base(notificador, user)
        {
            _carroRepository = carroRepository;
            _carroService = carroService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<CarroViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<CarroViewModel>>(await _carroRepository.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CarroViewModel>> ObterPorId(Guid id)
        {
            var garagem = await ObterPorId(id);

            if (garagem == null) return NotFound();

            return garagem;
        }

        [HttpPost]
        public async Task<ActionResult<CarroViewModel>> Adicionar(CarroViewModel carroViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _carroService.Adicionar(_mapper.Map<Carro>(carroViewModel));

            return CustomResponse(carroViewModel);
        }

        [ClaimsAuthorize("Fornecedor", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<CarroViewModel>> Atualizar(Guid id, CarroViewModel carroViewModel)
        {
            if (id != carroViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(carroViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _carroService.Atualizar(_mapper.Map<Carro>(carroViewModel));

            return CustomResponse(carroViewModel);
        }

        [ClaimsAuthorize("Fornecedor", "Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<CarroViewModel>> Excluir(Guid id)
        {
            var garagemViewModel = await ObterPorId(id);

            if (garagemViewModel == null) return NotFound();

            await _carroRepository.Remover(id);

            return CustomResponse(garagemViewModel);
        }
    }
}
