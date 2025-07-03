using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioPfe.Infrastructure.Configurations
{
	public class CarouselSlideConfiguration : IEntityTypeConfiguration<CarouselSlide>
	{
		public void Configure(EntityTypeBuilder<CarouselSlide> builder)
		{
			builder.HasKey(e => e.Id);
			builder.HasOne(e => e.Image).WithOne(e => e.CarouselSlide).HasForeignKey<CarouselSlide>(e => e.ImageId).OnDelete(DeleteBehavior.Cascade);
		}
	}
}