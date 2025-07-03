using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Common.Input;
using BiblioPfe.Common.Output;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Business.Interfaces
{
	public interface IAuthorService
	{
		public IQueryable<Author> GetAuthors();
		public Task<Author?> GetAuthor(Guid id);
		public Task<Author?> GetAuthorBySlug(string slug);
		public Task<Author> NewAuthor(AuthorInput input);
		public Task<Author> UpdateAuthor(Guid id,AuthorInput input);
		public Task<BaseReturnEnum> RemoveAuthor(Guid id);
		public Task<BaseReturnEnum> RemoveAuthors(List<Guid> ids);
		
	}
}