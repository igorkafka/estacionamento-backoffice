using EstacionamentoBackoffice.Business.DomainObjects;
using EstacionamentoBackoffice.Business.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoBackoffice.Business.Models
{
    public class Passagem : Entity
    {
        public Garagem Garagem { get; set; }
        public Guid GaragemId { get; set; }
        public Carro Carro { get; set; }
        public Guid CarroId { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public DateTime DataHoraSaida { get; set; }
        private decimal precoTotal;

        public decimal PrecoTotal
        {
            get { return CalcularPrecoTotal().ArrondaPrecoTotal(); }
            private set { precoTotal = value; }
        }

        public FormaPagamento FormaPagamento { get; set; }
        public Guid FormaPagamentoId { get; set; }
        private double CalcularEstadiaEmMinutos => this.DataHoraSaida.Subtract(this.DataHoraEntrada).TotalMinutes;
        private decimal CalcularCarencia(double estadiaEmMinutos)
        {
            Carencia carencia = new Carencia(this);
            if (estadiaEmMinutos <= 60)
                throw new DomainException("Não existe carência até a primeira hora");
            double quantidadeHorasPassadas = (estadiaEmMinutos / 60) - 1;
            decimal valorComCarencia = 0;
            if (quantidadeHorasPassadas >= 1)
                 valorComCarencia = Decimal.Multiply(this.Garagem.PrecoHorasExtra, carencia.ObterValorAbsoluto(quantidadeHorasPassadas));
            valorComCarencia = valorComCarencia + carencia.ObterValorApos30Minutos(quantidadeHorasPassadas);

            return valorComCarencia;

        }
        public decimal CalcularPrecoTotal()
        {
            if (this.FormaPagamento.Codigo == "MEN")
                return this.Garagem.PrecoMensalista;

            double estadiaEmMinutos = this.CalcularEstadiaEmMinutos;
            if (estadiaEmMinutos <= 60)
                return Garagem.PrecoUmaHora + 2;
            else
                return this.CalcularCarencia(estadiaEmMinutos) + (Garagem.PrecoUmaHora + 2);
        }

        public static class PassagemFactory
        {
            public static Passagem NovaPassagem(FormaPagamento formaPagamento, Carro carro, Garagem garagem)
            {
                var passagem = new Passagem
                {
                    FormaPagamento = formaPagamento,
                    FormaPagamentoId = formaPagamento.Id,
                    Carro = carro,
                    CarroId = carro.Id,
                    Garagem = garagem,
                    GaragemId = garagem.Id

                };
                return passagem;
            }
        }

    }
}
