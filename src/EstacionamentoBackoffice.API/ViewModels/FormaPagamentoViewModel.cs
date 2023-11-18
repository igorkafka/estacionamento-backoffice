
using System.ComponentModel.DataAnnotations;

namespace EstacionamentoBackoffice.API.ViewModels
{
    public class FormaPagamentoViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public string Codigo { get; set; }

    }
}
