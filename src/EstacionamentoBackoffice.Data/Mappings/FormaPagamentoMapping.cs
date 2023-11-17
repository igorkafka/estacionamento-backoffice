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
    public class FormaPagamentoMapping : IEntityTypeConfiguration<FormaPagamento>
    {
        public void Configure(EntityTypeBuilder<FormaPagamento> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Codigo).IsUnique();

            builder.Property(c => c.Codigo)
                .IsRequired()
                .HasColumnType("varchar(3)");

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasColumnType("varchar(50)");


            builder.ToTable("FormasPagamento");
        }
    }
}
