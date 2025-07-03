using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Common.Input;
using BiblioPfe.Common.Output;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Business.Interfaces
{
	public interface IBookServices 
	{
		public Task<Book?> GetBookAsAdmin(Guid id);
		public Task<Book?> GetBookBySlug(string slug);
		public Task<List<Book>> GetSimilarToBook(string slug);
		public Task<List<Book>> GetSameAuthorBooks(string slug);
		public IQueryable<Book> GetBooks();
		public Task<Book> UpdateBook(Guid id,BookInput input);
		public Task<Book> NewBook(BookInput input);
		public Task<Book> ToggleBook(Guid id,bool active);
		public Task<BaseReturnEnum> DeleteBook(Guid id);
		public Task<Book?> GetBookInernly(Guid id);
		
		public Task<List<BookOrderOption>> UpdateBookOptions(Guid id,BookOptionInput selling,BookOptionInput borrowing);
	}
}