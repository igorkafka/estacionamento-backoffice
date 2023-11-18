
namespace EstacionamentoBackoffice.API.ViewModels
{
    public class FormaPagamentoViewModel
    {
        public string Descricao { get; set; }
        public string Codigo { get; set; }
        public IEnumerable<PassagemViewModel> Passagens { get; set; }

    }
}
