using EstacionamentoBackoffice.Business.Models;
using System.ComponentModel.DataAnnotations;

namespace EstacionamentoBackoffice.API.ViewModels
{
    public class PassagemViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Garagem Garagem { get; set; }
        public Guid GaragemId { get; set; }
        public string CodigoGarragem { get; set; }
        public string FormaPagamentoCodigo { get; set; }
        public Carro Carro { get; set; }
        public string Placa { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public DateTime DataHoraSaida { get; set; }

        private decimal PrecoTotal { get; set; }
    }
}
