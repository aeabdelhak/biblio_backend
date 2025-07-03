using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioPfe.Infrastructure.Entities
{
	public class OrderDetail:BaseEntity
	{
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public Guid OrderId { get; set; }
		public Guid OptionId { get; set; }
		public Guid BookId { get; set; }
		public BookOrderOption Option { get; set; }
		public Book Book { get; set; }
		public Order Order { get; set; }

	}
}