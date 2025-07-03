using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Resolvers.Query
{
    public class BookReviewQuery(IBookReviewsService _service)
    {
        public IQueryable<BookReview> SiteGetBookReviews(Guid bookId)=>_service.SiteGetBookReviews(bookId);
		public IQueryable<BookReview> GetBookReviews(Guid bookId)=>_service.GetBookReviews(bookId); 
    }
}