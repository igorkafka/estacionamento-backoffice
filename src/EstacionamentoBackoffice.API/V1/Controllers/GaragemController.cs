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
    [Route("api/v{version:apiVersion}/Garagens")]
    public class GaragemController : MainController
    {
        private readonly IGaragemRepository _garagemRepository;
        private readonly IGaragemService _garagemService;
        private readonly IMapper _mapper;

        public GaragemController(IGaragemRepository garagemRepository,
            IGaragemService garagemService,
                                      IMapper mapper,
                                      INotificador notificador,
                                      IUser user) : base(notificador, user)
        {
          _garagemRepository = garagemRepository;
            _garagemService = garagemService;
           _mapper = mapper;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<GaragemViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<GaragemViewModel>>(await _garagemRepository.ObterTodos());
        }
        private async Task<GaragemViewModel> ObterGaragemPorId(Guid id)
        {
            return _mapper.Map<GaragemViewModel>(await _garagemRepository.ObterPorId(id));
        }
        private async Task<GaragemViewModel> ObterGaragemPorCodigo(string codigo)
        {
            return _mapper.Map<GaragemViewModel>(await _garagemRepository.ObterPorCodigo(codigo));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GaragemViewModel>> ObterPorId(Guid id)
        {
            var garagem = await ObterGaragemPorId(id);

            if (garagem == null) return NotFound();

            return garagem;
        }
        [HttpGet("{codigo}")]
        public async Task<ActionResult<GaragemViewModel>> ObterPorCodigo(string codigo)
        {
            var garagem = await ObterGaragemPorCodigo(codigo);

            if (garagem == null) return NotFound();

            return garagem;
        }

        [HttpPost]
        public async Task<ActionResult<GaragemViewModel>> Adicionar(GaragemViewModel garagemViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _garagemService.Adicionar(_mapper.Map<Garagem>(garagemViewModel));

            return CustomResponse(garagemViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<GaragemViewModel>> Atualizar(Guid id, GaragemViewModel garagemViewModel)
        {
            if (id != garagemViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(garagemViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _garagemService.Atualizar(_mapper.Map<Garagem>(garagemViewModel));

            return CustomResponse(garagemViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<GaragemViewModel>> Excluir(Guid id)
        {
            var garagemViewModel = await ObterPorId(id);

            if (garagemViewModel == null) return NotFound();

            await _garagemService.Remover(id);

            return CustomResponse(garagemViewModel);
        }
        [HttpDelete("{codigo}")]
        public async Task<ActionResult<GaragemViewModel>> Excluir(string codigo)
        {
            var garagemViewModel = await ObterGaragemPorCodigo(codigo);

            if (garagemViewModel == null) return NotFound();

            await _garagemService.Remover(garagemViewModel.Id);

            return CustomResponse(garagemViewModel);
        }
    }
}
