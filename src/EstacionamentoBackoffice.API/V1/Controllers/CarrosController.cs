using AutoMapper;
using EstacionamentoBackoffice.API.Controllers;
using EstacionamentoBackoffice.API.Extensions;
using EstacionamentoBackoffice.API.ViewModels;
using EstacionamentoBackoffice.Business.Interfaces;
using EstacionamentoBackoffice.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EstacionamentoBackoffice.API.V1.Controllers
{
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
            var carro = await ObterCarroPorId(id);

            if (carro == null) return NotFound();

            return carro;
        }

        [HttpGet("{placa}")]
        public async Task<ActionResult<CarroViewModel>> ObterPorPlaca(string placa)
        {
            var garagem = await ObterPorPlaca(placa);

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
        private async Task<CarroViewModel> ObterCarroPorId(Guid id)
        {
            return _mapper.Map<CarroViewModel>(await _carroRepository.ObterPorId(id));
        }
        private async Task<CarroViewModel> ObterCarroPorPlaca(string placa)
        {
            return _mapper.Map<CarroViewModel>(await _carroRepository.ObterPorPlaca(placa));
        }
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<CarroViewModel>> Excluir(Guid id, string? placa)
        {
            var garagemViewModel = await ObterCarroPorId(id);

            if (garagemViewModel == null) {
                garagemViewModel =  await ObterCarroPorPlaca(placa);
               if(garagemViewModel == null) return NotFound(); 
               else 
                    id = garagemViewModel.Id;
            }

            await _carroRepository.Remover(id);

            return CustomResponse(garagemViewModel);
        }
        [HttpDelete("{placa}")]
        public async Task<ActionResult<CarroViewModel>> Excluir(string placa)
        {
            var carroViewModel = await ObterCarroPorPlaca(placa);

            if (carroViewModel == null) return NotFound();

            await _carroRepository.Remover(carroViewModel.Id);

            return CustomResponse(carroViewModel);
        }
    }
}
