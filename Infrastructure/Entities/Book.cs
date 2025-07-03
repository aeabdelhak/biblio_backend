using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioPfe.Infrastructure.Entities
{
	public class Book :BaseEntity
	{
		public string Name { get; set; }
		public string Resume { get; set; }
		public int InStock { get; set; }
		public bool IsActive { get; set; }
		public Document? Cover { get; set; }
		public Guid? CoverId { get; set; }
		public SeoData SeoData { get; set; }
		public Guid SeoDataId { get; set; }
		public List<BookAuthor> BookAuthors { get; set; }
		public List<BookCategory> BookCategories { get; set; }
		public List<OrderDetail> OrderDetails { get; set; }
		public List<BookOrderOption> Options { get; set; }
		public List<BookReview> BookReviews { get; set; } 
		
		
	}
}