using EstacionamentoBackoffice.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoBackoffice.Business.Interfaces
{
    public interface IGaragemService
    {
        Task Adicionar(Garagem garagem);
        Task Atualizar(Garagem garagem);
        Task Remover(Guid id);

    }
}
