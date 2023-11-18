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
        public string Nome { get; set; }
        public decimal PrecoUmaHora { get; set; }
        public decimal PrecoHorasExtra { get; set; }
        public decimal PrecoMensalista { get; set; }
        public IEnumerable<Passagem> Passagens { get; set; }
        public static class GaragemFactory
        {
            public static Garagem NovaGaragem()
            {
                var garagem = new Garagem()
                {
                    Id = Guid.NewGuid(),
                    Nome = "Estamplaza Vila Olimpia",
                    PrecoUmaHora = 40,
                    PrecoHorasExtra = 10,
                    PrecoMensalista = 550
                };

                return garagem;
            }
        }
    }
}
