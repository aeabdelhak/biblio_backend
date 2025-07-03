using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioPfe.Infrastructure.Configurations
{
	public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
	{
		public void Configure(EntityTypeBuilder<OrderStatus> builder)
		{
			builder.HasKey(e => e.Id);
			builder.HasOne(e => e.Order).WithMany(e => e.Statuses).HasForeignKey(e => e.OrderId).OnDelete(DeleteBehavior.Cascade);
			builder.HasOne(e => e.MadeBy).WithMany(e => e.MadeStatusChanges).HasForeignKey(e => e.MadeById).OnDelete(DeleteBehavior.Cascade);
		}
	}
}