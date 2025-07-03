using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioPfe.Infrastructure.Entities
{
	public class Category :BaseEntity
	{
		public string Label { get; set; }
		public string? Description { get; set; }
		public List<BookCategory> BookCategories { get; set; }
		public SeoData SeoData { get; set; }
		public Guid SeoDataId { get; set; }
	}
}