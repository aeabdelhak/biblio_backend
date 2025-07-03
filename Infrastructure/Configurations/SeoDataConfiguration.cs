using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioPfe.Infrastructure.Configurations
{
    public class SeoDataConfiguration : IEntityTypeConfiguration<SeoData>
    {
        public void Configure(EntityTypeBuilder<SeoData> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}