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
    public class FormaPagamentoService : BaseService, IFormaPagamentoService
    {
        private readonly IFormaPagamentoRepository _formaPagamentoRepository;

        public FormaPagamentoService(IFormaPagamentoRepository formaPagamentoRepository,
                                 INotificador notificador) : base(notificador)
        {
            _formaPagamentoRepository = formaPagamentoRepository;
        }

        public async Task Adicionar(FormaPagamento formaPagamento)
        {
            if (!ExecutarValidacao(new FormaPagamentoValidation(), formaPagamento)) return;

            if (_formaPagamentoRepository.Buscar(g => g.Codigo == formaPagamento.Codigo).Result.Any())
            {
                Notificar("Já existe uma forma de pagamento com este código infomado.");
                return;
            }

            await _formaPagamentoRepository.Adicionar(formaPagamento);
        }

        public async Task Atualizar(FormaPagamento formaPagamento)
        {
            if (!ExecutarValidacao(new FormaPagamentoValidation(), formaPagamento)) return;

            if (_formaPagamentoRepository.Buscar(g => g.Codigo == formaPagamento.Codigo).Result.Any())
            {
                Notificar("Já existe uma garagem com este código infomado.");
                return;
            }

            await _formaPagamentoRepository.Atualizar(formaPagamento);
        }

        public async Task Remover(Guid id)
        {

            await _formaPagamentoRepository.Remover(id);
        }

        public void Dispose()
        {
            _formaPagamentoRepository?.Dispose();
        }
    }
}
