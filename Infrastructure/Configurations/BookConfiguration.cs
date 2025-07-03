using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioPfe.Infrastructure.Configurations
{
	public class BookConfiguration : IEntityTypeConfiguration<Book>
	{
		public void Configure(EntityTypeBuilder<Book> builder)
		{
			builder.HasKey(e => e.Id);
			builder.HasOne(e => e.Cover).WithOne(e=>e.Book).HasForeignKey<Book>(e=>e.CoverId).OnDelete(DeleteBehavior.Restrict);
			builder.HasOne(e => e.SeoData).WithOne(e=>e.Book).HasForeignKey<Book>(e=>e.SeoDataId).OnDelete(DeleteBehavior.Cascade);
		}
	}
}