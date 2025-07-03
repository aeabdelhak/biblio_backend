using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioPfe.Infrastructure.Configurations
{
	public class OrderDetailConfiguration : IEntityTypeConfiguration<OrderDetail>
	{
		public void Configure(EntityTypeBuilder<OrderDetail> builder)
		{
			builder.HasKey(e => e.Id);
			builder.HasOne(e => e.Order).WithMany(e => e.Details).HasForeignKey(e => e.OrderId).OnDelete(DeleteBehavior.Cascade);
			builder.HasOne(e => e.Book).WithMany(e=>e.OrderDetails).HasForeignKey(e=>e.BookId).OnDelete(DeleteBehavior.Restrict);
			builder.HasOne(e => e.Option).WithMany(e=>e.OrderDetails).HasForeignKey(e=>e.OptionId).OnDelete(DeleteBehavior.Restrict);
		}
	}
}