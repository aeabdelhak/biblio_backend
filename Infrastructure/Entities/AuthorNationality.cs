using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioPfe.Infrastructure.Entities
{
    public class AuthorNationality 
    {
        public Guid CountryId { get; set; }
        public Guid AuthorId { get; set; }
        public Author Author { get; set; }
        public Country Country { get; set; }
    }
}