using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioPfe.Infrastructure.Configurations
{
	public class CategoryConfiguration : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.HasKey(e => e.Id);
			builder.HasOne(e => e.SeoData).WithOne(e=>e.Category).HasForeignKey<Category>(e=>e.SeoDataId).OnDelete(DeleteBehavior.Cascade);
			
		}
	}
}