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
	public class AuthorMutation(IAuthorService _service)
	{
		public Task<Author> NewAuthor(AuthorInput input)=>_service.NewAuthor(input);
		public Task<BaseReturnEnum> RemoveAuthor(Guid id)=>_service.RemoveAuthor(id);
		public Task<BaseReturnEnum> RemoveAuthors(List<Guid> ids)=>_service.RemoveAuthors(ids);
		public Task<Author> UpdateAuthor(Guid id,AuthorInput input)=>_service.UpdateAuthor(id,input);
		
		
	}
}