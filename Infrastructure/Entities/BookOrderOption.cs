using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioPfe.Infrastructure.Entities
{
	public class BookOrderOption : BaseEntity
	{
		public Book Book { get; set; }
		public Guid BookId { get; set; }
		public BookOrderOptionEnum Option { get; set; }
		public decimal Price { get; set; }
		public bool IsActive { get; set; } = true;
		public List<OrderDetail> OrderDetails { get; set; }
		
	}
	
	public enum BookOrderOptionEnum
	{
		Selling,
		Borrowing
	}

}