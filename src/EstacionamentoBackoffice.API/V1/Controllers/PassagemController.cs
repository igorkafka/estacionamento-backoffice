using AutoMapper;
using EstacionamentoBackoffice.API.Controllers;
using EstacionamentoBackoffice.API.Interfaces;
using EstacionamentoBackoffice.API.ViewModels;
using EstacionamentoBackoffice.Business.Interfaces;
using EstacionamentoBackoffice.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization.Formatters.Binary;

namespace EstacionamentoBackoffice.API.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/Passagens")]
    public class PassagemController : MainController
    {
        private readonly IPassagemRepository _passagemRepository;
        private readonly IPassagemService _passagemService;
        private readonly IFormaPagamentoRepository _formaPagamentoRepository;
        private readonly IGaragemRepository _garagemRepository;
        private readonly ICarroRepository _carroRepository;
        private readonly IBlobService _blogAzure;
        private readonly IMapper _mapper;

        public PassagemController(IPassagemRepository passagemRepository,
            IPassagemService passagemService,
            IFormaPagamentoRepository formaPagamentoRepository,
            IGaragemRepository garagemRepository,
            ICarroRepository carroRepository,
            IBlobService blogAzure,
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
            _blogAzure = blogAzure;
        }

        [HttpGet]
        public async Task<IEnumerable<PassagemViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<PassagemViewModel>>(await _passagemRepository.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PassagemViewModel>> ObterPorId(Guid id)
        {
            var streamBlob = await _blogAzure.GetBlobFileAsync(id.ToString());
           

            if (streamBlob != null)
            {
                PassagemViewModel passagemViewModel = (PassagemViewModel)DeserializeFromStream(streamBlob);
                return passagemViewModel;
            }

            return _mapper.Map<PassagemViewModel>(await _passagemRepository.ObterPorId(id));

        }

        [HttpPost]
        public async Task<ActionResult<PassagemViewModel>> Adicionar(PassagemViewModel passagemViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            
            var carro = await _carroRepository.ObterPorPlaca(passagemViewModel.Placa);
            if (carro == null)
            {
                NotificarErro("Carro não encontrada");
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

            var result = Task.Run(async () => await _passagemService.Adicionar(passagem));

            result.Wait();
            passagemViewModel.Id = passagem.Id;
            passagemViewModel.PrecoTotal = passagem.PrecoTotal;
            passagemViewModel.Carro  = _mapper.Map<CarroViewModel>(carro);
            passagemViewModel.Garagem = _mapper.Map<GaragemViewModel>(garagem);
            passagemViewModel.FormaPagamento = _mapper.Map<FormaPagamentoViewModel>(formaPagamento);

            await _blogAzure.CreateBlobFileAsync(passagem.Id.ToString(), ObjectToByteArray(passagemViewModel));

            return CustomResponse(passagemViewModel);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<PassagemViewModel>> Atualizar(Guid id, PassagemViewModel formaPagamentoViewModel)
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

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<PassagemViewModel>> Excluir(Guid id)
        {
            var garagemViewModel = await ObterPassagemPorId(id);

            if (garagemViewModel == null) return NotFound();

            await _passagemService.Remover(id);


            var streamBlob = await _blogAzure.GetBlobFileAsync(id.ToString());


            if (streamBlob != null)
            {
                await _blogAzure.DeleteBlobFileAsync(id.ToString());

            }


            return CustomResponse(garagemViewModel);
        }
        private async Task<PassagemViewModel> ObterPassagemPorId(Guid id)
        {
            return _mapper.Map<PassagemViewModel>(await _passagemRepository.ObterPorId(id));
        }
        private byte[] ObjectToByteArray(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(ms, obj);
                    byte[] byteArray = ms.ToArray();
                    return byteArray;
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            }
        }
        private object DeserializeFromStream(Stream stream)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            object o = formatter.Deserialize(stream);
            return o;
        }
    }
}
