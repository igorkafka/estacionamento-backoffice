using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoBackoffice.Business.Models
{
    public class Passagem : Entity
    {
        public int Id { get; set; }
        public Garagem Garagem { get; set; }
        public Guid GaragemId { get; set; }
        public Carro Carro { get; set; }
        public Guid CarroId { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public DateTime DataHoraSaida { get; set; }
        public decimal PrecoTotal { get; set; }
        public FormaPagamento FormaPagamento { get; set; }
        public Guid FormaPagamentoId { get; set; }

    }
}
