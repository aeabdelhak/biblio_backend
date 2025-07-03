using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BiblioPfe.Infrastructure.Configurations
{
	public class DeliveryAddressConfiguration : IEntityTypeConfiguration<DeliveryAddress>
	{
		public void Configure(EntityTypeBuilder<DeliveryAddress> builder)
		{
			builder.HasKey(e => e.Id);
			builder.HasOne(e => e.User).WithMany(w=>w.Addresses).HasForeignKey(e=>e.UserId).OnDelete(DeleteBehavior.Restrict);
			
		}
	}
}