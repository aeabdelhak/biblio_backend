using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Common.Input;
using BiblioPfe.Common.Output;
using BiblioPfe.helpers;
using BiblioPfe.Infrastructure.Entities;
using BiblioPfe.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BiblioPfe.Business.Services
{
    public class BookReviewsService(
        IBookReviewsDA _reviewsDA,
        ICommonDA _commonDA,
        AuthHelper _auth
    ) : IBookReviewsService
    {
        public async Task<BookReview> AddBookReview(Guid BookId, ReviewInput input)
        {
            var review = new BookReview
            {
                Body = input.Body,
                BookId = BookId,
                Rating = input.Rating > 5 ? 5 : input.Rating
            };
            await _commonDA.DbInsert(review);
            return review;
        }

        public IQueryable<BookReview> GetBookReviews(Guid bookId)
        {
            return _reviewsDA.GetBookReviews().OrderByDescending(e => e.CreatedAt);
        }

        public async Task<BaseReturnEnum> RemoveBookReview(Guid Id)
        {
            var item = await _reviewsDA.GetBookReviews().SingleOrDefaultAsync(e => e.Id == Id);
            if (item is null)
                return BaseReturnEnum.NotFound;
            await _commonDA.DbDelete(item);
            return BaseReturnEnum.Success;
        }

        public IQueryable<BookReview> SiteGetBookReviews(Guid bookId)
        {
            return _reviewsDA.GetBookReviews().Where(e => e.BookId == bookId && e.IsPublic);
        }

        public async Task<BaseReturnEnum> SiteRemoveBookReview(Guid Id)
        {
            var auth = _auth.GetAuthClaims();

            var item = await _reviewsDA
                .GetBookReviews()
                .SingleOrDefaultAsync(e => e.Id == Id && e.UserId == auth.UserId);
            if (item is null)
                return BaseReturnEnum.NotFound;

            await _commonDA.DbDelete(item);
            return BaseReturnEnum.Success;
        }

        public async Task<BookReview> SiteUpdateBookReview(Guid Id, ReviewInput input)
        {
            var auth = _auth.GetAuthClaims();
            var item = await _reviewsDA
                .GetBookReviews()
                .SingleAsync(e => e.Id == Id && e.UserId == auth.UserId);
            item.Body = input.Body;
            item.Rating = input.Rating > 5 ? 5 : input.Rating;
            await _commonDA.DbDelete(item);
            return item;
        }

        public async Task<BookReview> ToggleReview(Guid Id, bool active)
        {
            var item = await _reviewsDA.GetBookReviews().SingleAsync(e => e.Id == Id);
            item.IsPublic = active;
            await _commonDA.DbUpdate(item);
            return item;
        }

        public async Task<BookReview> UpdateBookReview(Guid Id, ReviewInput input)
        {
            var item = await _reviewsDA.GetBookReviews().SingleAsync(e => e.Id == Id);
            item.Body = input.Body;
            item.Rating = input.Rating > 5 ? 5 : input.Rating;
            await _commonDA.DbDelete(item);
            return item;
        }
    }
}
