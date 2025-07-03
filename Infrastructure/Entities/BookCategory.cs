using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioPfe.Infrastructure.Entities
{
	public class BookCategory
	{
		public Guid CategoryId { get; set; }
		public Guid BookId { get; set; }
		public Category Category { get; set; }
		public Book Book { get; set; }
	}
}