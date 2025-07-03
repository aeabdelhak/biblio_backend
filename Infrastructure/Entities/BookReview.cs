using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiblioPfe.Infrastructure.Entities
{
    public class BookReview : BaseEntity
    {
        public string Body { get; set; }
        public int Rating { get; set; } = 0;
        public bool IsPublic { get; set; } = true;
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid BookId { get; set; }
        public Book Book { get; set; }
    }
}