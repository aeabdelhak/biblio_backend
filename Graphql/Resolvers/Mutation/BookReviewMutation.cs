using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Common.Input;
using BiblioPfe.Common.Output;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Resolvers.Mutation
{
    public class BookReviewMutation(IBookReviewsService _service)
    {
		public Task<BaseReturnEnum> RemoveBookReview(Guid Id)=>_service.RemoveBookReview(Id);
		public Task<BaseReturnEnum> SiteRemoveBookReview(Guid Id)=>_service.SiteRemoveBookReview(Id);
		public Task<BookReview> SiteUpdateBookReview(Guid Id,ReviewInput input)=>_service.SiteUpdateBookReview(Id,input);
		public Task<BookReview> UpdateBookReview(Guid Id,ReviewInput input)=>_service.UpdateBookReview(Id,input);
		public Task<BookReview> AddBookReview(Guid BookId,ReviewInput input)=>_service.AddBookReview(BookId,input);
		public Task<BookReview> ToggleReview(Guid Id,bool active)=>_service.ToggleReview(Id,active);
    }
}