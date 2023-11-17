using EstacionamentoBackoffice.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstacionamentoBackoffice.Data.Mappings
{
    public class PassagemMapping : IEntityTypeConfiguration<Passagem>
    {
        public void Configure(EntityTypeBuilder<Passagem> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasOne(c => c.Garagem)
                     .WithMany(c => c.Passagens)
                     .HasForeignKey(p => p.GaragemId);

            builder.HasOne(c => c.FormaPagamento)
                     .WithMany(c => c.Passagens)
                     .HasForeignKey(p => p.FormaPagamentoId);

            builder.HasOne(c => c.Carro)
                     .WithMany(c => c.Passagens)
                     .HasForeignKey(p => p.CarroId);

            builder.ToTable("Passagens");
        }
    }
}
