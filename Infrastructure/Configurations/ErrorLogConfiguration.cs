using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioPfe.Infrastructure.Configurations
{
	public class ErrorLogConfiguration : IEntityTypeConfiguration<ErrorLog>
	{
		public void Configure(EntityTypeBuilder<ErrorLog> builder)
		{
			builder.HasKey(e => e.Id);
		}
	}
}