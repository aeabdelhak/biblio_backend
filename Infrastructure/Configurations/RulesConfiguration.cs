using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioPfe.Infrastructure.Configurations
{
	public class RulesConfiguration : IEntityTypeConfiguration<Rules>
	{
		public void Configure(EntityTypeBuilder<Rules> builder)
		{
			builder.HasKey(e => e.Id);
		}
	}
}