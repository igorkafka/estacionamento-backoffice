using EstacionamentoBackoffice.Business.Models;

namespace EstacionamentoBackoffice.API.ViewModels
{
    public class GaragemViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal PrecoUmaHora { get; set; }
        public decimal PrecoHorasExtra { get; set; }
        public decimal PrecoMensalista { get; set; }
    }
}
