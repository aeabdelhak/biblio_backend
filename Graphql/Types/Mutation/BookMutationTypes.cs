using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Graphql.Resolvers.Mutation;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Types.Mutation
{
	public class BookMutationTypes : ObjectTypeExtension<BookMutation>
	{
		protected override void Configure(IObjectTypeDescriptor<BookMutation> descriptor)
		{
			descriptor.ExtendsType<AuthMutation>();
			descriptor.Field(e => e.NewBook(default!)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e => e.UpdateBook(default!,default!)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e => e.DeleteBook(default!)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e => e.ToggleBook(default!,default!)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e => e.UpdateBookOptions(default!,default!,default!)).Authorize(UserRole.Admin.ToString());
			
		}
	}
}