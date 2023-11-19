using AutoMapper;
using EstacionamentoBackoffice.API.ViewModels;
using EstacionamentoBackoffice.Business.DomainObjects;
using EstacionamentoBackoffice.Business.Models;

namespace EstacionamentoBackoffice.API.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Carro, CarroViewModel>().ReverseMap();
            CreateMap<Garagem, GaragemViewModel>().ReverseMap();
            CreateMap<FormaPagamento, FormaPagamentoViewModel>().ReverseMap();
            CreateMap<Fechamento, FechamentoViewModel>().ReverseMap();
            CreateMap<TempoMedio, TempoMedioViewModel>().ReverseMap();

            CreateMap<Passagem, PassagemViewModel>().ReverseMap();

        }
    }
}
