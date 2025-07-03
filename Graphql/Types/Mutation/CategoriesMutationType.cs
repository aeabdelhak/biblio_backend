using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiblioPfe.Graphql.Resolvers.Mutation;
using BiblioPfe.Infrastructure.Entities;

namespace BiblioPfe.Graphql.Types.Mutation
{
	public class CategoriesMutationType :  ObjectTypeExtension<CategoriesMutation>
	{

		protected override void Configure(IObjectTypeDescriptor<CategoriesMutation> descriptor)
		{
			descriptor.ExtendsType<AuthMutation>();
			descriptor.Field(e => e.CreateCategory(default!)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e => e.RemoveCategory(default!)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e => e.RemoveCategories(default!)).Authorize(UserRole.Admin.ToString());
			descriptor.Field(e => e.UpdateCategory(default!,default!)).Authorize(UserRole.Admin.ToString());
		}
	}
}