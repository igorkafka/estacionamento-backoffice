using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoBackoffice.Business.Models
{
    public class FormaPagamento : Entity
    {
        public string Descricao { get; set; }
        public string Codigo { get; set; }
        public IEnumerable<Passagem> Passagens { get; set; }
        public static class FormaPagamentoFactory
        {
            public static FormaPagamento NovoFormaPagamentoPix()
            {
                var formaPagamento = new FormaPagamento
                {
                    Codigo = "PIX",
                    Descricao = "PIX"

                };

                return formaPagamento;
            }
            public static FormaPagamento NovoFormaPagamentoMensalista()
            {
                var formaPagamento = new FormaPagamento
                {
                    Codigo = "MEN",
                    Descricao = "Mensalista"

                };

                return formaPagamento;
            }
        }

    }
}
