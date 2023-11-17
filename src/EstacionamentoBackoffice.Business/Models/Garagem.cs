using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoBackoffice.Business.Models
{
    public class Garagem : Entity
    {
        public decimal PrecoUmaHora { get; set; }
        public decimal PrecoHorasExtra { get; set; }
        public decimal PrecoMensalista { get; set; }
        public IEnumerable<Passagem> Passagens { get; set; }
    }
}
