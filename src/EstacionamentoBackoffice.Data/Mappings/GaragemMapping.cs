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
    public class GaragemMapping : IEntityTypeConfiguration<Garagem>
    {
        public void Configure(EntityTypeBuilder<Garagem> builder)
        {
            builder.HasKey(p => p.Id);


            builder.ToTable("Garagens");
        }
    }
}
