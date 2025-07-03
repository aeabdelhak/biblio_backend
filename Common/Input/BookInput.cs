using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Common.Input
{
    public class BookInput
    {
        public string Name { get; set; }
		public string Resume { get; set; }
		public int InStock { get; set; }
		public IFile? Cover { get; set; }
		public List<Guid> BookAuthors { get; set; }
		public List<Guid> BookCategories { get; set; }
    }
}