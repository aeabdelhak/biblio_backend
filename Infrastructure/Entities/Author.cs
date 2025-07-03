using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioPfe.Infrastructure.Entities
{
    public class Author : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string? Resume { get; set; }
        public List<AuthorNationality> Nationalities { get; set; }
        public Document? Image { get; set; }
        public Guid? ImageId { get; set; }
        public Guid SeoDataId { get; set; }
        public SeoData SeoData { get; set; }
        public List<BookAuthor> BookAuthors { get; set; }
    }
}
