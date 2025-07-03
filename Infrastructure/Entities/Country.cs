using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioPfe.Infrastructure.Entities
{
	public class Country:BaseEntity
	{
		public string AlphaCode1 { get; set; }
		public string AlphaCode2 { get; set; }
		public string Name { get; set; }
		public string Nationality { get; set; }
		public List<AuthorNationality> AuthorsNationalities { get; set; }
	}
}