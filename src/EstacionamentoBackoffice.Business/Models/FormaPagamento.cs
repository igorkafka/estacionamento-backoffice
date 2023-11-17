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
        public decimal Codigo { get; set; }
        public IEnumerable<Passagem> Passagens { get; set; }

    }
}
