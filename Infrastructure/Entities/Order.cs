using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioPfe.Infrastructure.Entities
{
	public class Order : BaseEntity
	{
		public int? Number { get; set; } 
		public bool Paid { get; set; } = false;
		public PaymentMethode? PaymentMethode { get; set; } = Entities.PaymentMethode.Cash;
		public decimal DeliveryFees { get; set; } = 0;
		public decimal TotalPrice { get; set; } = 0;
		public decimal TotalPaid { get; set; } = 0;
		public decimal TotalToGetPaid { get; set; } = 0;
		public OrderDeliveringType DeliveringType { get; set; } = OrderDeliveringType.Delivery;
		public Guid? UserId { get; set; }
		public User? User { get; set; }
		public Guid? AddressId { get; set; }
		public DateTime? DueTime { get; set; }
		public DateTime? ReturnDate { get; set; }
		public OrderType Type { get; set; } = OrderType.Selling;
		public DeliveryAddress? Address { get; set; }
		public List<OrderStatus> Statuses { get;set;}
		public List<OrderDetail> Details { get;set;}
		public CustomClient? Client { get;set;}
		
	}
	
	public enum OrderDeliveringType 
	{
		Delivery,
		PickUp,
	}
	
	public enum OrderType
	{
		Selling,
		Borrowing
	}
	public enum PaymentMethode
	{
		Cash,
		Online
	}
	
	
}