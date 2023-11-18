using EstacionamentoBackoffice.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoBackoffice.Business.Interfaces
{
    public interface ICarroService
    {
        Task Adicionar(Carro carro);
        Task Atualizar(Carro carro);
        Task Remover(Guid id);

    }
}
