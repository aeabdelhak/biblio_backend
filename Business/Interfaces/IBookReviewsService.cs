using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Common.Input;
using BiblioPfe.Common.Output;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Business.Interfaces
{
	public interface IBookReviewsService
	{
		public IQueryable<BookReview> GetBookReviews(Guid bookId);
		
		public IQueryable<BookReview> SiteGetBookReviews(Guid bookId);
		public Task<BaseReturnEnum> RemoveBookReview(Guid Id);
		public Task<BaseReturnEnum> SiteRemoveBookReview(Guid Id);
		public Task<BookReview> SiteUpdateBookReview(Guid Id,ReviewInput input);
		public Task<BookReview> UpdateBookReview(Guid Id,ReviewInput input);
		public Task<BookReview> AddBookReview(Guid BookId,ReviewInput input);
		public Task<BookReview> ToggleReview(Guid Id,bool active);
	}
}