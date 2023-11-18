using EstacionamentoBackoffice.Business.Interfaces;
using EstacionamentoBackoffice.Business.Models;
using EstacionamentoBackoffice.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoBackoffice.Business.Services
{
    public class GaragemService : BaseService, IGaragemService
    {
        private readonly IGaragemRepository _garagemRepository;

        public GaragemService(IGaragemRepository garagemRepository,
                                 INotificador notificador) : base(notificador)
        {
             _garagemRepository = garagemRepository;
        }

        public async Task Adicionar(Garagem garagem)
        {
            if (!ExecutarValidacao(new GaragemValidation(), garagem)) return;

            if (_garagemRepository.Buscar(g => g.Codigo == garagem.Codigo).Result.Any())
            {
                Notificar("Já existe uma garagem com este código infomado.");
                return;
            }

            await _garagemRepository.Adicionar(garagem);
        }

        public async Task Atualizar(Garagem garagem)
        {
            if (!ExecutarValidacao(new GaragemValidation(), garagem)) return;

                       if (_garagemRepository.Buscar(g => g.Codigo == garagem.Codigo).Result.Any())
            {
                Notificar("Já existe uma garagem com este código infomado.");
                return;
            }

            await _garagemRepository.Atualizar(garagem);
        }

        public async Task Remover(Guid id)
        {

            await _garagemRepository.Remover(id);
        }

        public void Dispose()
        {
            _garagemRepository?.Dispose();
        }
    }
}
