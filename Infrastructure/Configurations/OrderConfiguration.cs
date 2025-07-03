using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace BiblioPfe.Infrastructure.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<Order>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Order> builder)
		{
			builder.HasKey(e => e.Id);
			builder.HasOne(e => e.Address).WithMany(e=>e.Orders).HasForeignKey(e=>e.AddressId).OnDelete(DeleteBehavior.SetNull);
			builder.HasOne(e => e.User).WithMany(e=>e.Orders).HasForeignKey(e=>e.UserId).OnDelete(DeleteBehavior.Cascade);

		}
	}
}