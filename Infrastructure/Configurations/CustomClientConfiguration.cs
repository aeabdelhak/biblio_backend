using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioPfe.Infrastructure.Configurations
{
	public class CustomClientConfiguration : IEntityTypeConfiguration<CustomClient>
	{
		public void Configure(EntityTypeBuilder<CustomClient> builder)
		{
			builder.HasKey(e => e.Id);
			builder.HasOne(e => e.Order).WithOne(e => e.Client).HasForeignKey<CustomClient>(e => e.Id).OnDelete(DeleteBehavior.Cascade);
		}
	}
}