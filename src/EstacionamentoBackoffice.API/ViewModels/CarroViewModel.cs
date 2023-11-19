
namespace EstacionamentoBackoffice.API.ViewModels
{
    [Serializable]
    public class CarroViewModel
    {
        public Guid Id { get; set; }
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
    }
}
