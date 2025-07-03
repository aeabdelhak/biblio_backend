using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioPfe.Infrastructure.Configurations
{
	public class BookOrderOptionConfiguration : IEntityTypeConfiguration<BookOrderOption>
	{
		public void Configure(EntityTypeBuilder<BookOrderOption> builder)
		{
			builder.HasKey(e => e.Id);
			builder.HasOne(e => e.Book).WithMany(e=>e.Options).HasForeignKey(e=>e.BookId).OnDelete(DeleteBehavior.Cascade);
			
		}
	}
}