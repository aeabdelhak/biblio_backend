using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioPfe.Infrastructure.Configurations
{
	public class BookAuthorConfiguration : IEntityTypeConfiguration<BookAuthor>
	{
		public void Configure(EntityTypeBuilder<BookAuthor> builder)
		{
			builder.HasKey(e => new { e.AuthorId, e.BookId });
			builder.HasOne(e => e.Author).WithMany(e => e.BookAuthors).HasForeignKey(e => e.AuthorId).OnDelete(DeleteBehavior.Cascade);
			builder.HasOne(e => e.Book).WithMany(e => e.BookAuthors).HasForeignKey(e => e.BookId).OnDelete(DeleteBehavior.Cascade);
		}
	}
}