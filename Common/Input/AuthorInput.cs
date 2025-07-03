using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Common.Input
{
    public class AuthorInput
    {
        public string FirstName { get; set; }
		public string LastName { get; set; }
		public string DisplayName { get; set; }
		public string? Resume { get; set; }
		public IFile? Image { get; set; }
		public List<Guid> Nationalities { get; set; }
    }
}