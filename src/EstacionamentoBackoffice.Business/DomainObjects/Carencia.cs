using EstacionamentoBackoffice.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoBackoffice.Business.DomainObjects
{
    public class Carencia
    {
        public Passagem Passagem { get; set; }
        public Carencia(Passagem passagem) 
        {
            this.Passagem = passagem;
        }
        public decimal ObterValorAbsoluto(double numero)
        {
            return Convert.ToDecimal(Math.Truncate(numero));
        }
        public decimal ObterValorApos30Minutos(double quantidadesHorasPassadas)
        {

            var quantidadeMinutosPassados = this.ObterParteDecimal(quantidadesHorasPassadas);
            if (quantidadeMinutosPassados == 0)
                return 0;
            if (quantidadeMinutosPassados <= 0.75)
                return this.Passagem.Garagem.PrecoHorasExtra / 2;
            else if (quantidadeMinutosPassados >= 0.75)
                return this.Passagem.Garagem.PrecoHorasExtra;
            return 0;
        }
        public double ObterParteDecimal(double numero)
        {
            return numero % 1;
        }

    }
}
