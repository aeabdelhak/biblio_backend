using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioPfe.Infrastructure.Configurations
{
	public class BookCategoryConfiguration : IEntityTypeConfiguration<BookCategory>
	{
		public void Configure(EntityTypeBuilder<BookCategory> builder)
		{
			builder.HasKey(e => new { e.CategoryId, e.BookId });
			builder.HasOne(e => e.Category).WithMany(e => e.BookCategories).HasForeignKey(e => e.CategoryId).OnDelete(DeleteBehavior.Cascade);
			builder.HasOne(e => e.Book).WithMany(e => e.BookCategories).HasForeignKey(e => e.BookId).OnDelete(DeleteBehavior.Cascade);
		}
	}
}