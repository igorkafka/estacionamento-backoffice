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
    public class CarroService : BaseService, ICarroService
    {
        private readonly ICarroRepository _carroRepository;

        public CarroService(ICarroRepository carroRepository,
                                 INotificador notificador) : base(notificador)
        {
            _carroRepository = carroRepository;
        }

        public async Task Adicionar(Carro carro)
        {
            if (!ExecutarValidacao(new CarroValidation(), carro)) return;

            if (_carroRepository.Buscar(g => g.Placa == carro.Placa).Result.Any())
            {
                Notificar("Já existe um carro com esta placo.");
                return;
            }

            await _carroRepository.Adicionar(carro);
        }

        public async Task Atualizar(Carro carro)
        {
            if (!ExecutarValidacao(new CarroValidation(), carro)) return;

            if (_carroRepository.Buscar(g => g.Placa == carro.Placa).Result.Any())
            {
                Notificar("Já existe um carro com esta placo.");
                return;
            }

            await _carroRepository.Atualizar(carro);
        }
        public async Task Remover(Guid id)
        {

            await _carroRepository.Remover(id);
        }
        public void Dispose()
        {
            _carroRepository?.Dispose();
        }

        public Task<Garagem> ObterPorCodigo(string codigo)
        {
            throw new NotImplementedException();
        }
    }
}
