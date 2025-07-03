using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioPfe.Infrastructure.Entities
{
	public class SeoData : BaseEntity
	{
		public string Slug { get; set; }
		public string MetaTitle { get; set; }
		public string? MetaDescription { get; set; }
		public Book? Book { get; set; }
		public Category? Category { get; set; }
		public Author? Author { get; set; }
	}
}