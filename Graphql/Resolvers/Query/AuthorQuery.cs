using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Business.Interfaces;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Resolvers.Query
{
	public class AuthorQuery(IAuthorService _service)
	{
		
		public IQueryable<Author> GetAuthors()=>_service.GetAuthors();
		public Task<Author?> GetAuthor(Guid id)=>_service.GetAuthor(id);
		public Task<Author?> GetAuthorBySlug(string slug) => _service.GetAuthorBySlug(slug);
		
	}
}