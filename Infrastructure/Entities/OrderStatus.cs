using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioPfe.Infrastructure.Entities
{
	public class OrderStatus:BaseEntity
	{
		public Order Order { get; set; }
		public Guid OrderId { get; set; }
		public bool IsCurrent { get; set; }
		public OrderStatusEnum Status { get; set; }
		public User? MadeBy { get; set; }
		public Guid? MadeById { get; set; }
	}
	public enum OrderStatusEnum
	{
		Draft,
		New,
		Confirmed,
		PickedUp,
		Delivered,
		Returned,
		Completed,
		Canceled,
		Shipping,
		Borrowing,
	}
}