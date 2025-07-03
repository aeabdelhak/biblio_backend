using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Graphql.Resolvers.Mutation;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Types.Mutation
{
    public class AuthorMutationTypes : ObjectTypeExtension<AuthorMutation>
    {
        	protected override void Configure(IObjectTypeDescriptor<AuthorMutation> descriptor)
		{
			descriptor.ExtendsType<AuthMutation>();
			descriptor.Field(e => e.NewAuthor(default!)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e => e.RemoveAuthor(default!)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e => e.RemoveAuthors(default!)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e => e.UpdateAuthor(default!,default!)).Authorize(UserRole.Admin.ToString());
		}
    }
}