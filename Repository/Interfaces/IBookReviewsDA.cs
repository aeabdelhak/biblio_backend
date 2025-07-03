using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Repository.Interfaces
{
    public interface IBookReviewsDA
    {
        public IQueryable<BookReview> GetBookReviews();
        
    }
}