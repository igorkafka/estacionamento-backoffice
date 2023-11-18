using EstacionamentoBackoffice.Business.Models;

namespace EstacionamentoBackoffice.API.ViewModels
{
    public class CarroViewModel
    {
        public Guid Id { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public IEnumerable<Passagem> Passagens { get; set; }
    }
}
