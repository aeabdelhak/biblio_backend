using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Resolvers.Query
{
	public class BookQuery(IBookServices _service)
	{
		public Task<Book?> GetBookAsAdmin(Guid id) => _service.GetBookAsAdmin(id);

		public Task<Book?> GetBookBySlug(string slug) => _service.GetBookBySlug(slug);

		public IQueryable<Book> GetBooks() => _service.GetBooks();
		public Task<List<Book>> GetSimilarToBook(string slug)=>_service.GetSimilarToBook(slug);
		public Task<List<Book>> GetSameAuthorBooks(string slug)=>_service.GetSameAuthorBooks(slug);
	}
}
