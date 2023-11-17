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
    public class CarroMapping : IEntityTypeConfiguration<Carro>
    {
        public void Configure(EntityTypeBuilder<Carro> builder)
        {
            builder.HasKey(p => p.Id);

            builder.HasIndex(p => p.Placa).IsUnique();

            builder.Property(c => c.Modelo)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(c => c.Placa)
                .IsRequired()
                .HasColumnType("varchar(12)");


            builder.ToTable("Carros");
        }
    }
}
