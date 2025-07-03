using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Graphql.Resolvers.Query;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Types.Query
{
	public class AuthorQueryTypes:ObjectTypeExtension<AuthorQuery>
	{
		protected override void Configure(IObjectTypeDescriptor<AuthorQuery> descriptor)
		{
			descriptor.ExtendsType<AuthQuery>();
            descriptor.Field(e => e.GetAuthorBySlug(default!));
            descriptor.Field(e => e.GetAuthors());
            descriptor.Field(e => e.GetAuthor(default)).Authorize(UserRole.Admin.ToString());
			
		
		}
	}
}