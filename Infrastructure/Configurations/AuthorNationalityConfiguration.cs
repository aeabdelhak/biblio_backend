using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioPfe.Infrastructure.Configurations
{
	public class AuthorNationalityConfiguration : IEntityTypeConfiguration<AuthorNationality>
	{
		public void Configure(EntityTypeBuilder<AuthorNationality> builder)
		{
			builder.HasKey(e => new { e.AuthorId, e.CountryId });
			builder.HasOne(e => e.Author).WithMany(e => e.Nationalities).HasForeignKey(e => e.AuthorId).OnDelete(DeleteBehavior.Cascade);
			builder.HasOne(e => e.Country).WithMany(e => e.AuthorsNationalities).HasForeignKey(e => e.CountryId).OnDelete(DeleteBehavior.Cascade);
		}
	}
}