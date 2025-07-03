using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Infrastructure;
using BiblioPfe.Infrastructure.Entities;
using BiblioPfe.Repository.Interfaces;

namespace BiblioPfe.Repository.DataAcces
{
    public class BookReviewsDA(AppDbContext _dbContext) : IBookReviewsDA
    {
        public IQueryable<BookReview> GetBookReviews()
        {
			return _dbContext.BookReviews.AsQueryable();
        }
    }
}