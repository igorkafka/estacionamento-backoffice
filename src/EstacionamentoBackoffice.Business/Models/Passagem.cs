﻿using EstacionamentoBackoffice.Business.DomainObjects;
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
        public DateTime? DataHoraSaida { get; set; }

        public decimal PrecoTotal{ get; private set; }
        

        public FormaPagamento FormaPagamento { get; set; }
        public Guid FormaPagamentoId { get; set; }
        private double CalcularEstadiaEmMinutos => DataHoraSaida.Value.Subtract(DataHoraEntrada).TotalMinutes;
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
        public void CalcularPrecoTotal()
        {
    

            if (DataHoraSaida == null && FormaPagamento.Codigo != "MEN")
                this.PrecoTotal =  (Garagem.PrecoUmaHora + 2).ArrondaPrecoTotal();
            else if (this.FormaPagamento.Codigo == "MEN")
                this.PrecoTotal =  this.Garagem.PrecoMensalista.ArrondaPrecoTotal();
            else
            {
                double estadiaEmMinutos = this.CalcularEstadiaEmMinutos;
                if (estadiaEmMinutos <= 60)
                    this.PrecoTotal = (Garagem.PrecoUmaHora + 2).ArrondaPrecoTotal();
                else
                    this.PrecoTotal = (this.CalcularCarencia(estadiaEmMinutos) + (Garagem.PrecoUmaHora + 2)).ArrondaPrecoTotal();
            }
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
