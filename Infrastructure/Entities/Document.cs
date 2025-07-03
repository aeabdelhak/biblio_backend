using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioPfe.Infrastructure.Entities
{
	public class Document:BaseEntity
	{
		public string Name { get; set; }
		public string Url { get; set; }
		public string Path { get; set; }
		public string Type { get; set; }
		public Author? Author { get; set; }
		public Book? Book { get; set; }
		public User? User { get; set; }
		public CarouselSlide? CarouselSlide { get; set; }
	}
}