using EstacionamentoBackoffice.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoBackoffice.Domain.Tests.Fixtures
{
    [CollectionDefinition(nameof(PassagemCollection))]
    public class PassagemCollection : ICollectionFixture<PassagemValidoTestsFixture>
    { }
    public class PassagemValidoTestsFixture : IDisposable
    {
        public Passagem GerarPassagemValida()
        {
            var garagem = Garagem.GaragemFactory.NovaGaragem();
            var formaPagamento = FormaPagamento.FormaPagamentoFactory.NovoFormaPagamentoPix();
            var carro = Carro.CarroFactory.NovoCarro();
            var passagem = Passagem.PassagemFactory.NovaPassagem(formaPagamento, carro, garagem);

            return passagem;
        }

        public Passagem GerarPassagemMensalistaValida()
        {
            var garagem = Garagem.GaragemFactory.NovaGaragem();
            var formaPagamento = FormaPagamento.FormaPagamentoFactory.NovoFormaPagamentoMensalista();
            var carro = Carro.CarroFactory.NovoCarro();
            var passagem = Passagem.PassagemFactory.NovaPassagem(formaPagamento, carro, garagem);

            return passagem;
        }

        public void Dispose()
        {
        }
    }
}
