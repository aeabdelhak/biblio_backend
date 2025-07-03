using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioPfe.Infrastructure.Configurations
{
	public class AuthorConfiguration : IEntityTypeConfiguration<Author>
	{
		public void Configure(EntityTypeBuilder<Author> builder)
		{
			builder.HasKey(e => e.Id);
			builder.HasOne(e => e.SeoData).WithOne(e => e.Author).HasForeignKey<Author>(e=>e.SeoDataId).OnDelete(DeleteBehavior.Cascade);
			
		}
	}
}