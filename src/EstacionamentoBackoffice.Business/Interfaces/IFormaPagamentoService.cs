using EstacionamentoBackoffice.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoBackoffice.Business.Interfaces
{
    public interface IFormaPagamentoService 
    {
        Task Adicionar(FormaPagamento formaPagamento);
        Task Atualizar(FormaPagamento formaPagamento);
        Task Remover(Guid id);
    }
}
