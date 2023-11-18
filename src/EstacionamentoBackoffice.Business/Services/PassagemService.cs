using EstacionamentoBackoffice.Business.Interfaces;
using EstacionamentoBackoffice.Business.Models.Validations;
using EstacionamentoBackoffice.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoBackoffice.Business.Services
{
    public class PassagemService : BaseService, IPassagemService
    {
        private readonly IPassagemRepository _passagemRepository;

        public PassagemService(IPassagemRepository passagemRepository,
                                 INotificador notificador) : base(notificador)
        {
            _passagemRepository = passagemRepository;
        }

        public async Task Adicionar(Passagem passagem)
        {
            if (!ExecutarValidacao(new PassagemValidation(), passagem)) return;


            passagem.CalcularPrecoTotal();
            await _passagemRepository.AdicionarPassagem(passagem);
        }

        public async Task Atualizar(Passagem passagem)
        {
            if (!ExecutarValidacao(new PassagemValidation(), passagem)) return;

            await _passagemRepository.Atualizar(passagem);
        }

        public async Task Remover(Guid id)
        {

            await _passagemRepository.Remover(id);
        }

        public void Dispose()
        {
            _passagemRepository?.Dispose();
        }
    }
}
