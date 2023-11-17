using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoBackoffice.Business.Models
{
    public class Carro : Entity
    {
        public string Placa { get; set; }
        public string Modelo { get; set; }
        public IEnumerable<Passagem> Passagens { get; set; }

    }
}
