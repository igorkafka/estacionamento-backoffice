using EstacionamentoBackoffice.Business.Models;
using System.ComponentModel.DataAnnotations;

namespace EstacionamentoBackoffice.API.ViewModels
{
    [Serializable]
    public class PassagemViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public GaragemViewModel? Garagem { get; set; }
        public Guid? GaragemId { get; set; }
        public string CodigoGarragem { get; set; }
        public string FormaPagamentoCodigo { get; set; }
        public CarroViewModel? Carro { get; set; }
        public string Placa { get; set; }
        public DateTime DataHoraEntrada { get; set; }
        public DateTime DataHoraSaida { get; set; }

        private decimal PrecoTotal { get; set; }
    }
}
