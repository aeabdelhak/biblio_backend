using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioPfe.Infrastructure.Entities
{
	public class DeliveryAddress : BaseEntity
	{
		public string City { get; set; }
		public string Address { get; set; }
		public string? ZipCode { get; set; }
		public string PhoneNumber { get; set; }
		public bool Default { get; set; } = true;
		public User User { get; set; }
		public Guid UserId { get; set; }
		public List<Order> Orders { get; set; }
	
	}
}